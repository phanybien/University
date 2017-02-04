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
    class Di
    {
        protected Texture2D MyTexture;
        //protected SpriteBatch spriteBatch;
        protected Rectangle MyRect;
        protected Vector2 Myposition;
        protected SpriteEffects spriteEffect;
        protected int Width;
        protected int Height;
        protected int FrameNum;
        protected int Hinh,z=0;
        protected float timer;
        const float frameDelay = 80;
        protected KeyboardState mykeyboard;
        protected KeyboardState cur, old;

        #region.Properties

        public Texture2D texture
        {
            get
            {
                return MyTexture;
            }
        }

        #endregion
        public Di(Texture2D Mytexture, int Width, int Height, int num,int H)
        {
            this.MyTexture = Mytexture;
            this.Height = Height;
            this.Width = Width;
            FrameNum = num;
            Hinh = H;
            MyRect = new Rectangle(0, 0, MyTexture.Width / Hinh, MyTexture.Height);
            Myposition = new Vector2(0, 200);
        }
        public virtual void Update(GameTime gameTime)
        {
            mykeyboard = Keyboard.GetState();//nhap trang thai
            if (mykeyboard.IsKeyDown(Keys.C))//di chuyen sang trái
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
                Myposition.X -= 5;
                if (Myposition.X <= 0)
                    Myposition.X = 0;
            }
            if (mykeyboard.IsKeyDown(Keys.B))//di chuyen sang phai
            {
                spriteEffect = SpriteEffects.None;
                Myposition.X += 5;
                if (Myposition.X + MyTexture.Width/Hinh >= 1350)
                    Myposition.X = 1350 - MyTexture.Width/Hinh;
            }
            cur = Keyboard.GetState();
            if (cur.IsKeyDown(Keys.B) && old.IsKeyUp(Keys.B))
            {
                z = 1;
                float a = 100 * (float)gameTime.ElapsedGameTime.TotalSeconds; //hàm tạo 1 khoảng thời gian 
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= a)
                {
                    timer -= 5 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    MyRect.X += MyTexture.Width / Hinh;
                    FrameNum += 1;
                    if (FrameNum >= Hinh)
                    {
                        FrameNum = 0;
                        MyRect.X = 0;
                    }
                }
            }
            if (cur.IsKeyDown(Keys.C) && old.IsKeyUp(Keys.C))
            {
                z = 1;
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
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Myposition, MyRect, Color.White, 0f, new Vector2(0, 0), 1f, spriteEffect, 0);
        }
    }
}
