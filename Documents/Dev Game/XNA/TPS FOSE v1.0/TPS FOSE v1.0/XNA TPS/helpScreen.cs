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
using XNA_TPS.Helpers;

namespace XNA_TPS
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class helpScreen : Screen
    {
        SpriteBatch spriteBatch;
        InputHelper inputHelper;
        #region property
        #endregion
        public helpScreen(Game game)
            : base(game)
        {

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            inputHelper = Game.Services.GetService(typeof(InputHelper)) as InputHelper;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/help"), new Rectangle(0, 0, 1280, 768), Color.White);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}