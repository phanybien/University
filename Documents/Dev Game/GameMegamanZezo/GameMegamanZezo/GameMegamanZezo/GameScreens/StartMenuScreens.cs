using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using XRpgLibrary;
using Microsoft.Xna.Framework.Input;

namespace GameMegamanZezo.GameScreens
{
    public class StartMenuScreens:BaseGameState
    {
        //field
        //property
        //Constructor
        public StartMenuScreens(Game game, GameStateManager manager)
            : base(game, manager)
        {

        }

        //Method
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (InputHandler.KeyReleased(Keys.Escape))
            {
                Game.Exit();
            }
            base.Draw(gameTime);
        }
    }
}
