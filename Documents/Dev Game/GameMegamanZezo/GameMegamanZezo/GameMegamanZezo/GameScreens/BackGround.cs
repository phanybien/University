using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary.Controls;
using XRpgLibrary;


namespace GameMegamanZezo.GameScreens
{
    public class BackGround:BaseGameState
    {
        //field
        Texture2D backGround;
        LinkLabel startLabel;

        //constructor
        public BackGround(Game game, GameStateManager manager)
            : base(game, manager)
        {

        }

        //Method 
        protected override void LoadContent()
        {
            ContentManager content = gameRef.Content;
            backGround = content.Load<Texture2D>(@"Backgrounds\Background");

            base.LoadContent();

            startLabel = new LinkLabel();
            startLabel.Position = new Vector2(250, 350);
            startLabel.Text = "Press ENTER to Begin";
            startLabel.Color = Color.White;
            startLabel.TabStop = true;
            startLabel.HasFocus = true;
            startLabel.Selected += new EventHandler(StartLabel_Selected);

            controlManager.Add(startLabel);
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update(gameTime, PlayerIndex.One);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            gameRef.spriteBatch.Begin();
            base.Draw(gameTime);

            gameRef.spriteBatch.Draw(backGround, gameRef.ScreenRectangle, Color.White);
            controlManager.Draw(gameRef.spriteBatch);
            gameRef.spriteBatch.End();
        }

        private void StartLabel_Selected(Object sender, EventArgs e)
        {
            StateManager.PushState(gameRef.startMenuScreen);
        }
    }
}
