using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using XNA_TPS.GameBase.ParticleSystems;
using XNA_TPS.GameBase.Cameras;

namespace XNA_TPS.GameLogic
{
    public class Player : TerrainUnit
    {
        public bool IsHit;
        static float MAX_WAIST_BONE_ROTATE = 0.50f;
        static int WAIST_BONE_ID = 2;
        static int RIGHT_HAND_BONE_ID = 15;
        ExplosionSmokeParticleSystem smoke;
        public enum PlayerAnimations
        {
            Idle = 0,
            Run,
            Aim,
            Shoot
        }

        // Player type
        UnitTypes.PlayerType playerType;
        // Player weapon
        PlayerWeapon playerWeapon;
        // Camera chase position
        Vector3[] chaseOffsetPosition;
        // Rotate torso bone
        float rotateWaistBone;
        float rotateWaistBoneVelocity;
        //attribute
        TimeSpan regen;
        public int Level = 1;
        public int MaxMana,Exp,Str,Int,Ski,Agi,Hea,Mana,Dam,Def;
        public float MaxExp;
        #region Properties
        public PlayerAnimations CurrentAnimation
        {
            get
            {
                return (PlayerAnimations)CurrentAnimationId;
            }
        }

        public Vector3[] ChaseOffsetPosition
        {
            get
            {
                return chaseOffsetPosition;
            }
            set
            {
                chaseOffsetPosition = value;
            }
        }

        public PlayerWeapon Weapon
        {
            get
            {
                return playerWeapon;
            }
        }

        public float RotateWaist
        {
            get
            {
                return rotateWaistBone;
            }
            set
            {
                rotateWaistBone = value;

                // Rotate torso bone
                Matrix rotate = Matrix.CreateRotationZ(rotateWaistBone);
                AnimatedModel.BonesTransform[WAIST_BONE_ID] = rotate;
            }
        }

        public float RotateWaistVelocity
        {
            get
            {
                return rotateWaistBoneVelocity;
            }
            set
            {
                rotateWaistBoneVelocity = value;
            }
        }
        #endregion
        
        public Player(Game game, UnitTypes.PlayerType playerType)
            : base(game)
        {
            this.playerType = playerType;
            smoke = new ExplosionSmokeParticleSystem(game, game.Content);
            smoke.DrawOrder = 50;
            game.Components.Add(smoke);
        }

        protected override void LoadContent()
        {
            Load(UnitTypes.PlayerModelFileName[(int)playerType]);

            Speed = UnitTypes.PlayerSpeed[(int)playerType];

            MaxExp = UnitTypes.MAXExp[(int)playerType];

            SetAnimation(Player.PlayerAnimations.Idle, false, true, false);
            Int = 20;
            Str = 20;
            Agi = 10;
            Hea = 25;
            Ski = 100;
            Life = Str * 3 + Hea * 5;
            Mana = Int * 3 + Hea * 5;
            base.LoadContent();
        }

        public void AttachWeapon(UnitTypes.PlayerWeaponType weaponType)
        {
            playerWeapon = new PlayerWeapon(Game, weaponType);
            playerWeapon.Initialize();
        }

        public void SetAnimation(PlayerAnimations animation, bool reset, bool enableLoop, bool waitFinish)
        {
            SetAnimation((int)animation, reset, enableLoop, waitFinish);
        }

        private void UpdateChasePosition()
        {
            ThirdPersonCamera camera = cameraManager.ActiveCamera as ThirdPersonCamera;
            if (camera != null)
            {
                // Get camera offset position for the active camera
                Vector3 cameraOffset = Vector3.Zero;
                if (chaseOffsetPosition != null)
                    cameraOffset = chaseOffsetPosition[cameraManager.ActiveCameraIndex];

                // Get the model center
                Vector3 center = BoundingSphere.Center;

                // Calculate chase position and direction
                camera.ChasePosition = center +
                    cameraOffset.X * StrafeVector +
                    cameraOffset.Y * UpVector +
                    cameraOffset.Z * HeadingVector;
                camera.ChaseDirection = HeadingVector;
            }
        }

        private void UpdateWaistBone(float elapsedTimeSeconds)
        {
            if (rotateWaistBoneVelocity != 0.0f)
            {
                rotateWaistBone += rotateWaistBoneVelocity * elapsedTimeSeconds;
                rotateWaistBone = MathHelper.Clamp(rotateWaistBone, -MAX_WAIST_BONE_ROTATE, MAX_WAIST_BONE_ROTATE);

                // Rotate torso bone
                Matrix rotate = Matrix.CreateRotationZ(rotateWaistBone);
                AnimatedModel.BonesTransform[WAIST_BONE_ID] = rotate;
            }
        }

        public override void Update(GameTime time)
        {
            float elapsedTimeSeconds = (float)time.ElapsedGameTime.TotalSeconds;
            UpdateWaistBone(elapsedTimeSeconds);
            //check for nextlevel
            NextLevel(time);
            //spawn smoke
            if(LinearVelocity != Vector3.Zero)
            smoke.AddParticle(Transformation.Translate, Vector3.One);
            base.Update(time);
            UpdateChasePosition();
            IsHit = false;
            // Update player weapon
            Matrix transformedHand = AnimatedModel.BonesAnimation[RIGHT_HAND_BONE_ID] * Transformation.Matrix;
            playerWeapon.TargetDirection = HeadingVector + UpVector * rotateWaistBone;
            playerWeapon.Update(time, transformedHand);
        }
        public void NextLevel(GameTime time)
        {
            regen += time.ElapsedGameTime;
            if (Exp > MaxExp)
            {
                Exp -= (int)MaxExp;
                MaxExp = 1.2f*Level*1000;
                Str += 3; Int += 3; Agi += 3; Hea += 3; Ski += 5;
                Level++;
                playerWeapon.BulletsCount = playerWeapon.MaxBullets;
                Life = MaxLife;
                Mana = MaxMana;
            }
            else
            {
                Dam = Str;
                Def = Agi / 200;
                MaxLife = Str * 3 + Hea * 5;
                MaxMana = Int * 3 + Hea * 5;
            }
            //Regen mana va` Hp
            if(regen > TimeSpan.FromMilliseconds(1000))
            {
                Life += Str / 20;
                Mana += Int / 20;
                if (Life > MaxLife)
                    Life = MaxLife;
                if (Mana > MaxMana)
                    Mana = MaxMana;
                regen -= TimeSpan.FromMilliseconds(1000);
            }
            
        }
        public override void ReceiveDamage(int damageValue)
        {
            //reduce dam
            IsHit = true;
            damageValue -= damageValue * Def;
            base.ReceiveDamage(damageValue);
        }
        public override void Draw(GameTime time)
        {
            smoke.SetCamera(cameraManager.ActiveCamera.View, cameraManager.ActiveCamera.Projection);
            playerWeapon.Draw(time);

            base.Draw(time);
        }
    }
}
