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
    public class StartScreen : Screen
    {
        int time = 0;
        SpriteFont spriteFont;
        SpriteBatch spriteBatch;
        enum Menu
        {
            New = 0,
            Load = 1,
            Option=2,
            Exit=3,
        }
        Menu menu = 0;
        InputHelper inputHelper;
        #region property
        public bool New
        {
            get { return (menu == Menu.New); }
        }
        public bool Exit
        {
            get { return (menu == Menu.Exit); }
        }
        public bool Load
        {
            get { return (menu == Menu.Load); }
        }
        public bool Option
        {
            get { return (menu == Menu.Option); }
        }
        #endregion
        public StartScreen(Game game)
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
            spriteFont = Game.Content.Load<SpriteFont>(GameAssetsPath.FONTS_PATH+"BerlinSans");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            
            if (inputHelper.IsKeyJustPressed(Keys.Up))
            {
                if (menu == 0)
                    menu = Menu.Exit;
                else
                    menu--;
            }
            if (inputHelper.IsKeyJustPressed(Keys.Down))
            {
                if (menu == Menu.Exit)
                    menu = Menu.New;

            else

                menu++;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            
            time += gameTime.TotalGameTime.Milliseconds;
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/BG"), new Rectangle(0, 0, 1280, 960), Color.White);
            spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/info"), new Rectangle(100, 200, 700, 400), Color.White);
            if(time > 2000)
                if (menu == Menu.New)
                {
                    spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/Selected"), new Vector2(800, 100), new Rectangle(0, 0, 300, 100), Color.White);
                    spriteBatch.DrawString(spriteFont, "New", new Vector2(880, 120), Color.Violet);
                    
                }
                else
                {
                   
                    spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/NoSelect"), new Vector2(800, 100), new Rectangle(0, 0, 300, 100), Color.White);
                    spriteBatch.DrawString(spriteFont, "New", new Vector2(880, 120), Color.Yellow);
                }
            if (time > 4000)
                if (menu == Menu.Load)
                {
                    
                    spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/Selected"), new Vector2(800, 200), new Rectangle(0, 0, 300, 100), Color.White);
                spriteBatch.DrawString(spriteFont, "Load", new Vector2(880, 220), Color.Violet);}
                else
                {
                    
                    spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/NoSelect"), new Vector2(800, 200), new Rectangle(0, 0, 300, 100), Color.White);
              spriteBatch.DrawString(spriteFont, "Load", new Vector2(880, 220), Color.Yellow);  }
            if (time > 16000)
                if (menu == Menu.Option)
                {
                    

                    spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/Selected"), new Vector2(800, 300), new Rectangle(0, 0, 300, 100), Color.White);
              spriteBatch.DrawString(spriteFont, "Help", new Vector2(880, 320), Color.Violet);  }
                else
                {
                    
                    spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/NoSelect"), new Vector2(800, 300), new Rectangle(0, 0, 300, 100), Color.White);
                    spriteBatch.DrawString(spriteFont, "Help", new Vector2(880, 320), Color.Yellow);
                }
            if (time > 32000)
                        if (menu == Menu.Exit)
                        {
                            
                            spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/Selected"), new Vector2(800, 400), new Rectangle(0, 0, 300, 100), Color.White);
                        spriteBatch.DrawString(spriteFont, "Exit", new Vector2(880, 420), Color.Violet);}
                        else
                        {
                            
                            spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/NoSelect"), new Vector2(800, 400), new Rectangle(0, 0, 300, 100), Color.White);
                        spriteBatch.DrawString(spriteFont, "Exit", new Vector2(880, 420), Color.Yellow);}
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}