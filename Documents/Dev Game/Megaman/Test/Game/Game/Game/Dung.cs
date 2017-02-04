using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game
{
    class Dung: Di
    {
        public Dung(Texture2D texture, int Width, int Height, int num, int H)
            : base(texture, Width, Height, num, H)
        {
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
