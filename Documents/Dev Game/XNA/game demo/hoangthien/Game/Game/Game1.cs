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

namespace Game
{
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture; //sd ham nay nhieu dung de load mot hinh len
        Vector2 position = new Vector2(0, 0);
        Vector2 myVecl;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //mac dinh ngang 800 doc 600

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }


        protected override void Initialize()
        {
            myVecl = new Vector2(5,5);
            position = new Vector2(0, 0);
            base.Initialize();
        }

   
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("bong");   //load hinh co ten la "bong" vao texture

        }

 
        protected override void UnloadContent()
        {
            
        }

 
        protected override void Update(GameTime gameTime)
        {
         
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            position.Y += myVecl.Y;

            if (position.Y + texture.Height >= 600 || position.Y <= 0)
            {
                myVecl.Y = -myVecl.Y;
            }

            //position += new Vector2(1, 0);// lam chuyen dong sang ngang
            //position += new Vector2(0, 1);// lam chuyen dong di xuong
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);//dung White thi se de nguyen mau goc cua tam hinh
           

            spriteBatch.End();
     

            base.Draw(gameTime);
        }
    }
}
