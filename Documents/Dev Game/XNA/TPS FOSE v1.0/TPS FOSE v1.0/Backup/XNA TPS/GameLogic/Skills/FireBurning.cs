using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using XNA_TPS.GameLogic;
using XNA_TPS.GameLogic.Levels;
using XNA_TPS.Helpers;
using XNA_TPS.GameBase.ParticleSystems;
using XNA_TPS.GameBase.Cameras;
namespace XNA_TPS.GameLogic
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class FireBurning : Skills
    {
        //particles effect
        SmokePlumeParticleSystem Smoke;
        FireParticleSystem fireSkill;
        //input
        KeyboardState input;
        

        public FireBurning(Game game)
            : base(game)
        {
            //thiet lap cac tham so cho skill
            //xem class Skills.cs se ro~
            Smoke = new SmokePlumeParticleSystem(game, game.Content);
            fireSkill = new FireParticleSystem(game, game.Content);
            fireSkill.DrawOrder = 100;
            Smoke.DrawOrder = 200;
            game.Components.Add(fireSkill);
            game.Components.Add(Smoke);
            CoolDown = 20000;
            key = Keys.D2;
            Ski = 50;
            ManaCost = 50;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            Time = CoolDown;
            skill = true;

            base.Initialize();
            
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, Enemy aim)
        {

            //dung` keyboard input de choi skill
            if (Active && skill && IsKeyJustPressed(Keys.D2))
            {
                ManaLost(ManaCost);
                skill = false;
                Time = 0;
                //Gay dam cho Enemy (tac dung tuc thoi`)
                aim.ReceiveDamage(5 * Shooter.Dam);

            }
            
            
            base.Update(gameTime);
        }
        public override void AddEffect()
        {
            fireSkill.AddParticle(Shooter.Weapon.FirePosition + Shooter.Weapon.TargetDirection, Vector3.Zero);
            fireSkill.AddParticle(Shooter.Weapon.FirePosition + Shooter.Weapon.TargetDirection * 2, Vector3.Zero);
            fireSkill.AddParticle(Shooter.Weapon.FirePosition + Shooter.Weapon.TargetDirection * 3, Vector3.Zero);
            fireSkill.AddParticle(Shooter.Weapon.FirePosition + Shooter.Weapon.TargetDirection * 4, Vector3.Zero);
            fireSkill.AddParticle(Shooter.Weapon.FirePosition + Shooter.Weapon.TargetDirection * 5, Vector3.Zero);

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public void Draw(CameraManager cameraManager)
        {
            fireSkill.SetCamera(cameraManager.ActiveCamera.View, cameraManager.ActiveCamera.Projection);
            Smoke.SetCamera(cameraManager.ActiveCamera.View, cameraManager.ActiveCamera.Projection);
        }

    }
}