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
    class NhayDa: Di
    {
        public NhayDa(Texture2D texture, int Width, int Height, int num,int H)
            : base(texture, Width, Height, num, H)
        {
        }
        public override void Update(GameTime gameTime)
        {
            if ( (cur.IsKeyDown(Keys.F) && old.IsKeyUp(Keys.F)) && (cur.IsKeyDown(Keys.S) && old.IsKeyUp(Keys.S)) )
            {
                z = 6;
                float a = 5 * (float)gameTime.ElapsedGameTime.TotalSeconds; //hàm tạo 1 khoảng thời gian 
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= a)
                {
                    timer -= 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    MyRect.X += MyTexture.Width / Hinh;
                    FrameNum += 1;
                    if (FrameNum >= Hinh)
                    {
                        FrameNum = 0;
                        MyRect.X = 0;
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
