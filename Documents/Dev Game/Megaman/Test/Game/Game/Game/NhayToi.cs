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
    class NhayToi: Di
    {
        public NhayToi(Texture2D texture, int Width, int Height, int num,int H)
            : base(texture, Width, Height, num, H)
        {
        }
        public override void Update(GameTime gameTime)
        {
            if (cur.IsKeyDown(Keys.D) && old.IsKeyUp(Keys.C))
            {
                //spriteEffect = SpriteEffects.None;
                Myposition.X -= 10;
                if (Myposition.X <= 0)
                    Myposition.X = 0;
            }
            if (cur.IsKeyDown(Keys.D) && old.IsKeyUp(Keys.B))
            {
                //spriteEffect = SpriteEffects.None;
                Myposition.X += 10;
                if (Myposition.X + MyTexture.Width / Hinh >= 1350)
                    Myposition.X = 1350 - MyTexture.Width / Hinh;
            }
            if (cur.IsKeyDown(Keys.D) && old.IsKeyUp(Keys.D))
            {
                float a = 100 * (float)gameTime.ElapsedGameTime.TotalSeconds; //hàm tạo 1 khoảng thời gian 
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
