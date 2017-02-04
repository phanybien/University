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
    public class IceBlast : Skills
    {
        SmokePlumeParticleSystem Smoke;
        IceParticleSystem fireSkill;
      

        public IceBlast(Game game)
            : base(game)
        {
            Smoke = new SmokePlumeParticleSystem(game, game.Content);
            fireSkill = new IceParticleSystem(game, game.Content);
            fireSkill.DrawOrder = 100;
            Smoke.DrawOrder = 200;
            game.Components.Add(fireSkill);
            game.Components.Add(Smoke);
            CoolDown = 10000;
            ManaCost = 30;
            Ski = 30;
           
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
        public void Update(GameTime gameTime,Enemy aim)
        {

            if (Active && skill && IsKeyJustPressed(Keys.D1))
            {
                //gay dam cho monster (tac dung tuc thoi`)
                ManaLost(ManaCost);
                aim.ReceiveDamage(3*Shooter.Dam);
                skill = false;
                Time = 0;

            }


            base.Update(gameTime);
        }
        public override void AddEffect()
        {
                    fireSkill.AddParticle(Shooter.Transformation.Translate + new Vector3(RandomHelper.GetRandomInt(100) - 50, RandomHelper.GetRandomInt(10) - 5, RandomHelper.GetRandomInt(100) - 10), Vector3.Zero);
                    fireSkill.AddParticle(Shooter.Transformation.Translate + new Vector3(RandomHelper.GetRandomInt(100) - 50, RandomHelper.GetRandomInt(10) - 5, RandomHelper.GetRandomInt(100) - 10), Vector3.Zero);
                    Smoke.AddParticle(Shooter.Transformation.Translate + new Vector3(RandomHelper.GetRandomInt(100) - 50, RandomHelper.GetRandomInt(10) - 5, RandomHelper.GetRandomInt(100) - 10), Vector3.Zero);
                    Smoke.AddParticle(Shooter.Transformation.Translate + new Vector3(RandomHelper.GetRandomInt(100) - 50, RandomHelper.GetRandomInt(10) - 5, RandomHelper.GetRandomInt(100) - 10), Vector3.Zero);
                
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