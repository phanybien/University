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

namespace XNA_TPS.GameLogic
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Skills : Microsoft.Xna.Framework.GameComponent
    {
        //skill cho biet khi nao` cooldown xong
        protected bool skill = true;
        //dem' thoi gian dang chay thoi` gian hoi (tu` 0 den CoolDown)
        public int Time;
        //phim de su dung skill
        protected Keys key;
        protected int ManaCost;
        //thoi gian hoi` chieu
        public int CoolDown;
        //So diem skill de player co' the dung` skill
        public int Ski ;
        Player player;
        //DK de user co the dung` skill va` toa? eff
        protected bool Active,effect;
        bool pressed;
        //player trong gameScene
        public Player Shooter
        {
            get { return player; }
            set { player = value; }
        }
        protected KeyboardState keyBoard, LastKeyboard;
        public Skills(Game game)
            : base(game)
        {
            
        }
        public override void Initialize()
        {
            skill = true;
            Time = CoolDown;
            base.Initialize();
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            
            LastKeyboard = keyBoard;
            keyBoard = Keyboard.GetState();
            //Skill player!
            if(!skill)
            {
                if (Time < CoolDown)
                {
                    if (Time < CoolDown / 3)
                        //trong khoang thoi gian o -> CoolDown / 3 se~ chay effect cua skill (toa lua hay dong' bang enemy...)
                        effect = true;
                    else
                        effect = false;
                    //chay thoi gian hoi`
                    Time += gameTime.ElapsedGameTime.Milliseconds;
                    skill = false;
                }
                else
                {
                    effect = false;
                    skill = true;
                }
            }
            
            Active = (player.Ski >= Ski&&Shooter.Mana>=ManaCost);
            if (effect) AddEffect();
            base.Update(gameTime);
        }
        //Neu skill co effect, Class mo rong se override method duoi' day
        public virtual void AddEffect()
        {
        }
        //Tru` mana cho player
        public void ManaLost(int mana)
        {
            player.Mana -= mana;
        }
        //Ky thuat release phim'
        public bool IsKeyJustPressed(Keys key)
        {
            pressed = (keyBoard.IsKeyDown(key) && LastKeyboard.IsKeyUp(key));

            return pressed;
        }
    }
}