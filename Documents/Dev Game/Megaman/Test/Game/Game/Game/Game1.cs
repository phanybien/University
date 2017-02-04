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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture,texture1,texture2,texture3,texture4,texture5,texture6,texture7,texture8,texture9,texture10,texture11,texture12;
        Di di; Nhay nhay; Da da; Dam dam; Dung dung; Ngoi ngoi; NhayToi nhaytoi; NgoiDa ngoida; NgoiDam ngoidam; NhayDa nhayda; NhayDam nhaydam; Nga nga; DungDay dungday;
        KeyboardState cur,old;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1350;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Run");
            di = new Di(texture, texture.Width, texture.Height, 0, 10);
            texture1 = Content.Load<Texture2D>("Run2");
            nhay = new Nhay(texture1, texture1.Width, texture1.Height, 0, 1);
            //texture2 = Content.Load<Texture2D>("Da");
            //da = new Da(texture2, texture2.Width, texture2.Height, 0, 6);
            texture3 = Content.Load<Texture2D>("Shoot");
            dam = new Dam(texture3, texture3.Width, texture3.Height, 0, 1);
            texture4 = Content.Load<Texture2D>("Stand");
            dung = new Dung(texture4, texture4.Width, texture4.Height, 0, 3);
            texture5 = Content.Load<Texture2D>("Sit");
            ngoi = new Ngoi(texture5, texture5.Width, texture5.Height, 0, 4);
            //texture6 = Content.Load<Texture2D>("NgoiDa");
            //ngoida = new NgoiDa(texture6, texture6.Width, texture6.Height, 0, 3);
            //texture7 = Content.Load<Texture2D>("NhayToi");
            //nhaytoi = new NhayToi(texture7, texture7.Width, texture7.Height, 0, 6);
            texture8 = Content.Load<Texture2D>("Run n Shoot");
            ngoidam = new NgoiDam(texture8, texture8.Width, texture8.Height, 0, 10);
            //texture9 = Content.Load<Texture2D>("NhayDa");
            //nhayda = new NhayDa(texture9, texture9.Width, texture9.Height, 0, 3);
            //texture10 = Content.Load<Texture2D>("NhayDam");
            //nhaydam = new NhayDam(texture10, texture10.Width, texture10.Height, 0, 3);
            //texture11 = Content.Load<Texture2D>("Nga");
            //nga = new Nga(texture11, texture11.Width, texture11.Height, 0, 5);
            //texture12 = Content.Load<Texture2D>("DungDay");
            //dungday = new DungDay(texture12, texture12.Width, texture12.Height, 0, 3);

        }
        protected override void UnloadContent()
        {
            
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            cur = Keyboard.GetState();
            di.Update(gameTime);
            nhay.Update(gameTime);
            //da.Update(gameTime);
            dam.Update(gameTime);
            dung.Update(gameTime);
            ngoi.Update(gameTime);
            //ngoida.Update(gameTime);
            //nhaytoi.Update(gameTime);
            ngoidam.Update(gameTime);
            //nhayda.Update(gameTime);
            //nhaydam.Update(gameTime);
            //nga.Update(gameTime);
            //dungday.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (cur.IsKeyDown(Keys.C) || cur.IsKeyDown(Keys.B) ||cur.IsKeyDown(Keys.F) 
                || cur.IsKeyDown(Keys.S) || cur.IsKeyDown(Keys.A) || cur.IsKeyDown(Keys.V)
                || (cur.IsKeyDown(Keys.V) && cur.IsKeyDown(Keys.S)) || (cur.IsKeyDown(Keys.V) && cur.IsKeyDown(Keys.A))
                || (cur.IsKeyDown(Keys.F) && cur.IsKeyDown(Keys.S)) || (cur.IsKeyDown(Keys.F) && cur.IsKeyDown(Keys.A))
                || cur.IsKeyDown(Keys.D) || cur.IsKeyDown(Keys.E) || cur.IsKeyDown(Keys.R))
            {
                //if (cur.IsKeyDown(Keys.D))
                //    nhaytoi.Draw(spriteBatch);
                //else
                    if (cur.IsKeyDown(Keys.C) || cur.IsKeyDown(Keys.B))
                        di.Draw(spriteBatch);
                    else
                        //if (cur.IsKeyDown(Keys.V) && cur.IsKeyDown(Keys.S))
                        //    ngoida.Draw(spriteBatch);
                        //else
                            if (cur.IsKeyDown(Keys.B) && cur.IsKeyDown(Keys.A))
                                ngoidam.Draw(spriteBatch);
                            else
                                //if (cur.IsKeyDown(Keys.F) && cur.IsKeyDown(Keys.S))
                                //    nhayda.Draw(spriteBatch);
                                //else
                                    //if (cur.IsKeyDown(Keys.F) && cur.IsKeyDown(Keys.A))
                                    //    nhaydam.Draw(spriteBatch);
                                    //else
                                        if (cur.IsKeyDown(Keys.V))
                                            ngoi.Draw(spriteBatch);
                                        else
                                            //if (cur.IsKeyDown(Keys.S))
                                            //    da.Draw(spriteBatch);
                                            //else
                                                if (cur.IsKeyDown(Keys.A))
                                                    dam.Draw(spriteBatch);
                                                else
                                                    if (cur.IsKeyDown(Keys.F))
                                                        nhay.Draw(spriteBatch);
                                                //    else
                                                        //if (cur.IsKeyDown(Keys.E))
                                                        //    nga.Draw(spriteBatch);
                                                        //else
                                                        //    if (cur.IsKeyDown(Keys.R))
                                                        //        dungday.Draw(spriteBatch);
            }
            else
                dung.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
