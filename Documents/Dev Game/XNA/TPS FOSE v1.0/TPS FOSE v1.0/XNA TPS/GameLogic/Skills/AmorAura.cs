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
    public class Amor : Skills
    {

        public Amor(Game game)
            : base(game)
        {

            Ski = 70;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            //tang def cho player
            Shooter.Def *= 3;

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

    }
}