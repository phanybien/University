using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using XNA_TPS.GameBase;
using XNA_TPS.Helpers;

namespace XNA_TPS.GameLogic
{
    public class Acher : TerrainUnit
    {
        public int seconds = 0;
        public Enemy aimEnemy;
        TimeSpan elapsedTime;
        PlayerWeapon BeastWeapon;
        public PlayerWeapon Weapon
        {
            get
            {
                return BeastWeapon;
            }
        }
        

        public enum AcherAnimations
        {
            Idle = 0,
            Run,
            aim,
            Shoot
        }

        public enum AcherState
        {
            Wander = 0,
            ChaseBeast,
            AttackBeast,
            Dead,
            Evade
        }

        static float DISTANCE_EPSILON = 1.0f;
        static float LINEAR_VELOCITY_CONSTANT = 35.0f;
        static float ANGULAR_VELOCITY_CONSTANT = 100.0f;

        static int WANDER_MAX_MOVES = 3;
        static int WANDER_DISTANCE = 100;
        static float WANDER_DELAY_SECONDS = 3.0f;
        static float ATTACK_DELAY_SECONDS = 2.0f;

        AcherState state;
        float nextActionTime;

        // Wander
        int wanderMovesCount;
        Vector3 wanderPosition;
        Vector3 wanderStartPosition;

        // Chase
        float perceptionDistance;
        Vector3 chaseVector;

        // Attack
        bool isHited;
        float attackDistance;
        int attackDamage;
        Boolean fire = false;
        //Type
        public UnitTypes.AcherType AcherType;
        public int Exp;
        #region Properties 
        public Boolean Fire
        {
            get
            {
                return fire;
            }
            set
            {
                fire = value;
            }
        }
        public Vector3 Chase
        {
            get { return chaseVector; }
        }
        public AcherAnimations CurrentAnimation
        {
            get
            {
                return (AcherAnimations)CurrentAnimationId;
            }
        }

        public AcherState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public override Transformation Transformation
        {
            get
            {
                return AnimatedModel.Transformation;
            }
            set
            {
                base.Transformation = value;
                wanderPosition = value.Translate;
                wanderStartPosition = value.Translate;
            }
        }
        #endregion

        public Acher(Game game, UnitTypes.AcherType AcherType)
            : base(game)
        {
            this.AcherType = AcherType;
            this.name = "Acher";
            isHited = false;
            wanderMovesCount = 0;
            
        }

        protected override void LoadContent()
        {
            Load(UnitTypes.AcherModelFileName[(int)AcherType]);

            // Unit configurations
            Life = UnitTypes.AcherLife[(int)AcherType];
            MaxLife = Life;
            Speed = UnitTypes.AcherSpeed[(int)AcherType];
            perceptionDistance = UnitTypes.AcherPerceptionDistance[(int)AcherType];
            attackDamage = UnitTypes.AcherAttackDamage[(int)AcherType];
            attackDistance = UnitTypes.AcherAttackDistance[(int)AcherType];
            wanderPosition = Transformation.Translate;
            SetAnimation(AcherAnimations.Idle, false, true, false);

            base.LoadContent();
        }

        public override void ReceiveDamage(int damageValue)
        {
            base.ReceiveDamage(damageValue);

            if (Life <= 0)
            {
                state = AcherState.Dead;
            }
        }

        public void SetAnimation(AcherAnimations animation, bool reset, bool enableLoop, bool waitFinish)
        {
            SetAnimation((int)animation, reset, enableLoop, waitFinish);
        }

        private void Move(Vector3 direction)
        {
            SetAnimation(AcherAnimations.Run, false, true, false);
            LinearVelocity = direction * LINEAR_VELOCITY_CONSTANT;

            // Angle between heading and move direction
            float radianAngle = (float)Math.Acos(Vector3.Dot(HeadingVector, direction));
            if (radianAngle >= 0.1f)
            {
                // Find short side to rodade CW or CCW
                float sideToRotate = Vector3.Dot(StrafeVector, direction);

                Vector3 rotationVector = new Vector3(0, ANGULAR_VELOCITY_CONSTANT * radianAngle, 0);
                if (sideToRotate > 0)
                    AngularVelocity = -rotationVector;
                else
                    AngularVelocity = rotationVector;
            }
        }

        private void Wander(GameTime time)
        {
            // Calculate wander vector on X, Z axis
            Vector3 wanderVector = wanderPosition - Transformation.Translate;
            wanderVector.Y = 0;
            float wanderVectorLength = wanderVector.Length();

            // Reached the destination position
            if (wanderVectorLength < DISTANCE_EPSILON)
            {
                SetAnimation(AcherAnimations.Idle, false, true, false);

                // Generate new random position
                if (wanderMovesCount < WANDER_MAX_MOVES)
                {
                    wanderPosition = Transformation.Translate +
                        RandomHelper.GeneratePositionXZ(WANDER_DISTANCE);

                    wanderMovesCount++;
                }
                // Go back to the start position
                else
                {
                    wanderPosition = wanderStartPosition;
                    wanderMovesCount = 0;
                }

                // Next time wander
                nextActionTime = (float)time.TotalGameTime.TotalSeconds + WANDER_DELAY_SECONDS +
                    WANDER_DELAY_SECONDS * (float)RandomHelper.RandomGenerator.NextDouble();
            }

            // Wait for the next action time
            if (time.TotalGameTime.TotalSeconds > nextActionTime)
            {
                wanderVector *= (1.0f / wanderVectorLength);
                Move(wanderVector);
            }
        }

        private void ChaseBeast(GameTime time)
        {
            Move(chaseVector);
        }
        public void AttachWeapon(UnitTypes.PlayerWeaponType weaponType)
        {
            BeastWeapon = new PlayerWeapon(Game, weaponType);
            BeastWeapon.Initialize();
        }
        private void AttackBeast(GameTime time)
        {
            float elapsedTimeSeconds = (float)time.TotalGameTime.TotalSeconds;
            if (elapsedTimeSeconds > nextActionTime)
            {
                fire = true;
                aimEnemy.ReceiveDamage(attackDamage);
                SetAnimation(AcherAnimations.Shoot, true, false, false);
                nextActionTime = elapsedTimeSeconds + ATTACK_DELAY_SECONDS;
            }
        }
        private void EvadeBeast(GameTime time)
        {
            Move(-chaseVector);
        }
        public override void Update(GameTime time)
        {
            LinearVelocity = Vector3.Zero;
            AngularVelocity = Vector3.Zero;
            float distanceToBeast = 0f;
            if (aimEnemy != null)
            {
                chaseVector = aimEnemy.Transformation.Translate - Transformation.Translate;
                distanceToBeast = chaseVector.Length();

                // Normalize chase vector
                chaseVector *= (1.0f / distanceToBeast);
                if (aimEnemy.IsDead)
                {
                    aimEnemy = null;
                    state = AcherState.Wander;
                }
            }
            else distanceToBeast = 1000;
            
            switch (state)
            {
                case AcherState.Wander:
                    if (isHited || distanceToBeast < perceptionDistance && aimEnemy != null)
                        // Change state
                        state = AcherState.ChaseBeast;
                    else
                        Wander(time);
                    break;

                case AcherState.ChaseBeast:
                    if (distanceToBeast <= attackDistance && aimEnemy != null)
                    {
                        // Change state
                        state = AcherState.AttackBeast;
                        nextActionTime = 0;
                    }
                    else
                        ChaseBeast(time);
                    break;
                case AcherState.Evade:
                    if ((distanceToBeast <= attackDistance) && (distanceToBeast > attackDistance / 2) && aimEnemy != null)
                    {
                        state = AcherState.AttackBeast;
                        nextActionTime = 0;
                    }
                    else 
                        EvadeBeast(time);
                    break;

                case AcherState.AttackBeast:
                    {

                        Vector3 direction = chaseVector;

                        // Angle between heading and move direction
                        float radianAngle = (float)Math.Acos(Vector3.Dot(HeadingVector, direction));
                        if (radianAngle >= 0.1f)
                        {
                            // Find short side to rodade CW or CCW
                            float sideToRotate = Vector3.Dot(StrafeVector, direction);

                            Vector3 rotationVector = new Vector3(0, ANGULAR_VELOCITY_CONSTANT * radianAngle, 0);
                            if (sideToRotate > 0)
                                AngularVelocity = -rotationVector;
                            else
                                AngularVelocity = rotationVector;
                        }

                        if (distanceToBeast > attackDistance * 1.5f && aimEnemy != null)
                            // Change state
                            state = AcherState.ChaseBeast;
                        else
                            if (distanceToBeast < attackDistance * 0.5f && aimEnemy != null)
                            // Change state
                            state = AcherState.Evade;
                        else
                            AttackBeast(time);
                        break;
                    }

                default:
                    {
                        
                        if (Transformation.Rotate.X < 80)
                            Transformation.Rotate = new Vector3(Transformation.Rotate.X + 10, Transformation.Rotate.Y, Transformation.Rotate.Z);
                        elapsedTime += time.ElapsedGameTime;
                        if (elapsedTime > TimeSpan.FromSeconds(3))
                        {
                            Transformation.Translate = RandomHelper.GeneratePositionXZ(500);
                            Transformation.Rotate = new Vector3(Transformation.Rotate.X - 80, Transformation.Rotate.Y, Transformation.Rotate.Z);
                            //Acher song lai 
                            Life = MaxLife;
                            IsDead = false;
                            isHited = false;
                            aimEnemy = null;
                            State = Acher.AcherState.Wander;
                            elapsedTime -= TimeSpan.FromSeconds(3);
                        }
                    }
                    break;
                   
                    
            }
            Matrix transformedHand = AnimatedModel.BonesAnimation[15] * Transformation.Matrix;
            BeastWeapon.Update(time, transformedHand);
            base.Update(time);
        }

        public override void Draw(GameTime time)
        {
            BeastWeapon.Draw(time);
            base.Draw(time);
        }
    }
}
