using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;

using XRpgLibrary;
using XRpgLibrary.Controls;

namespace GameMegamanZezo.GameScreens
{
    public abstract partial class BaseGameState:GameState
    {
        //field
        protected Game1 gameRef;
        protected ControlManager controlManager;
        protected PlayerIndex playerIndexInControl;

        //Constructor
        public BaseGameState(Game game, GameStateManager manager)
            : base(game, manager)
        {
            gameRef = (Game1)game;
            playerIndexInControl = PlayerIndex.One;
        }

        //Method
        protected override void LoadContent()
        {
            ContentManager content = Game.Content;

            SpriteFont menuFont = content.Load<SpriteFont>(@"Fonts\ControlFont");
            controlManager = new ControlManager(menuFont);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
           
            base.Draw(gameTime);
        }
    }
}
