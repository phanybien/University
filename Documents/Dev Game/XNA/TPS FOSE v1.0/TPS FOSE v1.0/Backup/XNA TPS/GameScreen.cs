using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XNA_TPS.GameBase.Cameras;
using XNA_TPS.GameBase.Lights;
using XNA_TPS.GameBase.Shapes;
using XNA_TPS.GameLogic;
using XNA_TPS.GameLogic.Levels;
using XNA_TPS.Helpers;
using XNA_TPS.GameBase.ParticleSystems;
using XNA_TPS.GameBase;


namespace XNA_TPS
{
    
    public class GameScreen : Screen
    {
        Song music;
        //fire
        BloodParticleSystem fire;
        // Modified level that we are playing
        LevelCreator.Levels currentLevel;
        public GameLevel gameLevel;
        //Mouse
        MouseState currentMouseState;
        MouseState mouseOrigin;
        // Text
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        // Weapon target sprite
        Texture2D weaponTargetTexture;
        Vector3 weaponTargetPosition;

        // Aimed enemy
        Enemy aimEnemy;
        int numEnemiesAlive;
        //time
        TimeSpan timeS;
        // Necessary services
        InputHelper inputHelper;
        GraphicsDeviceManager graphics;
        public GameScreen(Game game, LevelCreator.Levels currentLevel)
            : base(game)
        {
            this.currentLevel = currentLevel;
            fire = new BloodParticleSystem(game, game.Content);
            fire.DrawOrder = 700;
            game.Components.Add(fire);
        }

        public override void Initialize()
        {
            // Mouse
            graphics = Game.Services.GetService(typeof(GraphicsDeviceManager)) as GraphicsDeviceManager;
            Mouse.SetPosition(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            mouseOrigin = Mouse.GetState();
            
            // Get services
            inputHelper = Game.Services.GetService(typeof(InputHelper)) as InputHelper;
            if (inputHelper == null)
                throw new InvalidOperationException("Cannot find an input service");
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create SpriteBatch and add services
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //audio
            music = Game.Content.Load<Song>("audio/backmusic");
            MediaPlayer.Play(music);
            // Font 2D
            spriteFont = Game.Content.Load<SpriteFont>(GameAssetsPath.FONTS_PATH +
                "BerlinSans");

            // Weapon target
            weaponTargetTexture = Game.Content.Load<Texture2D>(GameAssetsPath.TEXTURES_PATH +
                "weaponTarget");

            // Load game level
            gameLevel = LevelCreator.CreateLevel(Game, currentLevel);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        float UD;
        float LR;
        private void UpdateInput()
        {
            float rotationSpeed;
            //Toc do xoay camera theo chuot
            if (inputHelper.IsKeyPressed(Keys.Space))
                //toc do cham lai 1 chut :) khi dang aim mode!
                rotationSpeed = 0.02f;
            else
                rotationSpeed = 0.05f;
            ThirdPersonCamera fpsCamera = gameLevel.CameraManager["FPSCamera"] as ThirdPersonCamera;
            ThirdPersonCamera followCamera = gameLevel.CameraManager["FollowCamera"] as ThirdPersonCamera;
            //Dam bao? followCamera ko o duoi' mat dat'
            followCamera.Position = new Vector3(followCamera.Position.X, Math.Max(followCamera.Position.Y, (gameLevel.Terrain.GetHeight(new Vector2(followCamera.Position.X, followCamera.Position.Z)) + 30)), followCamera.Position.Z);
            //Lay vi tri mouse hien tai
            currentMouseState = Mouse.GetState();
            //Do lech camera khi xoay chuot
            if (currentMouseState != mouseOrigin)
            {
                float xDifference = currentMouseState.X - mouseOrigin.X;
                float yDifference = currentMouseState.Y - mouseOrigin.Y;
                LR -= rotationSpeed * xDifference;
                UD -= rotationSpeed * yDifference;

                Mouse.SetPosition(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            }
            else
            {
                LR = UD = 0f;
            }
            Player player = gameLevel.Player;
            Vector2 leftThumb = inputHelper.GetLeftThumbStick();
            
            
            // Aim Mode
            if (inputHelper.IsKeyPressed(Keys.Space))
            {
                
                // Reset follow camera 
                if (gameLevel.CameraManager.ActiveCamera != fpsCamera)
                {
                    gameLevel.CameraManager.SetActiveCamera("FPSCamera");
                    fpsCamera.IsFirstTimeChase = true;
                    player.SetAnimation(Player.PlayerAnimations.Aim, false, false, false);
                }
                
                // Rotate the camera and move the player's aim
                fpsCamera.EyeRotateVelocity = new Vector3(UD * 50.0f, 0.0f, 0.0f);
                player.LinearVelocity = Vector3.Zero;
                player.AngularVelocity = new Vector3(0.0f, LR * 70.0f, 0.0f);
                player.RotateWaistVelocity = UD * 0.8f;

                // Shoot
                if (aimEnemy != null && currentMouseState.LeftButton == ButtonState.Pressed && player.Weapon.BulletsCount > 0 && Vector3.Distance(player.Transformation.Translate, aimEnemy.Transformation.Translate) < 700)
                {
                    
                    // Wait the last shoot animation finish
                    if (player.AnimatedModel.IsAnimationFinished )
                    {
                        player.SetAnimation(Player.PlayerAnimations.Shoot, true, false, false);

                        // Damage the enemy
                        player.Weapon.BulletsCount--;
                        if (aimEnemy != null)
                        {
                            fire.AddParticle(aimEnemy.Transformation.Translate + Vector3.One * 10, Vector3.Down);
                            fire.AddParticle(aimEnemy.Transformation.Translate + Vector3.One * 10, Vector3.Down);
                            fire.AddParticle(aimEnemy.Transformation.Translate + Vector3.One * 10, Vector3.Down);
                            fire.AddParticle(aimEnemy.Transformation.Translate + Vector3.One * 10, Vector3.Down);
                            aimEnemy.isHited = true;
                            aimEnemy.aimEnemy = player;
                            aimEnemy.ReceiveDamage(player.Dam);
                        }
                    }
                }
            }
            // Normal Mode
            else
            {
                bool isPlayerIdle = true;

                if (gameLevel.CameraManager.ActiveCamera != followCamera)
                {
                    // Reset fps camera 
                    gameLevel.CameraManager.SetActiveCamera("FollowCamera");
                    followCamera.IsFirstTimeChase = true;
                    player.RotateWaist = 0.0f;
                    player.RotateWaistVelocity = 0.0f;
                }

                followCamera.EyeRotateVelocity = new Vector3(UD * 50.0f, LR * 30f, 0.0f);
               
                player.AngularVelocity = new Vector3(0.0f, -leftThumb.X * 70.0f, 0.0f);

                // Run foward 
                if (inputHelper.IsKeyPressed(Keys.W))
                {
                    player.SetAnimation(Player.PlayerAnimations.Run, false, true, false);
                    player.LinearVelocity = player.HeadingVector * 30.0f;
                    isPlayerIdle = false;
                }
                // Run backward
                else if (inputHelper.IsKeyPressed(Keys.S))
                {
                    player.SetAnimation(Player.PlayerAnimations.Run, false, true, false);
                    player.LinearVelocity = -player.HeadingVector * 20.0f;
                    isPlayerIdle = false;
                }
                else
                    player.LinearVelocity = Vector3.Zero;
                if (inputHelper.IsKeyPressed(Keys.A))
                    player.AngularVelocity += new Vector3(0, 50, 0);
                else if (inputHelper.IsKeyPressed(Keys.D))
                    player.AngularVelocity -= new Vector3(0, 50, 0);
                // Jump
                if (inputHelper.IsKeyJustPressed(Keys.Q))
                {
                    player.Jump(2.5f);
                    isPlayerIdle = false;
                }

                if (isPlayerIdle)
                    player.SetAnimation(Player.PlayerAnimations.Idle, false, true, false);
            }
        }

        private void UpdateWeaponTarget()
        {
            aimEnemy = null;
            numEnemiesAlive = 0;
            
            // Shoot ray
            Ray ray = new Ray(gameLevel.Player.Weapon.FirePosition, gameLevel.Player.Weapon.TargetDirection);
            
            // Distance from the ray start position to the terrain
            float? distance = gameLevel.Terrain.Intersects(ray);

            // Test intersection with enemies
            foreach (Enemy enemy in gameLevel.EnemyList)
            {
                if (!enemy.IsDead)
                {
                    numEnemiesAlive++;

                    float? enemyDistance = enemy.BoxIntersects(ray);
                    if (enemyDistance != null)
                    {
                        if (distance == null || enemyDistance <= distance)
                        {
                            distance = enemyDistance;
                            aimEnemy = enemy;
                        }
                    }
                }
            }

            // Weapon target position
            weaponTargetPosition = gameLevel.Player.Weapon.FirePosition +
                gameLevel.Player.Weapon.TargetDirection * 300;
        }

        public override void Update(GameTime gameTime)
        {
            timeS += gameTime.ElapsedGameTime;
            if (timeS > TimeSpan.FromMinutes(1))
            {
                MediaPlayer.Play(music);
                timeS -= TimeSpan.FromMinutes(1);
            }
            // Restart game
            if (gameLevel.Player.IsDead || numEnemiesAlive == 0)
                gameLevel = LevelCreator.CreateLevel(Game, currentLevel);

            UpdateInput();
            if (gameLevel.Player.IsHit)
            {
                fire.AddParticle(gameLevel.Player.Transformation.Translate + Vector3.One * 5, Vector3.Down);
                fire.AddParticle(gameLevel.Player.Transformation.Translate + Vector3.One * 5, Vector3.Down);
            }
            // Update player
            gameLevel.Player.Update(gameTime);
            UpdateWeaponTarget();
            
            
            // Update camera
            BaseCamera activeCamera = gameLevel.CameraManager.ActiveCamera;
            activeCamera.Update(gameTime);
            //Update and Draw staticmodel
            foreach (StaticModel tree in gameLevel.staticModel)
            {
                if (tree.ModelSphere.Intersects(activeCamera.Frustum))
                {
                    tree.UpdateHeight(gameLevel.Terrain);
                    //tree.DrawNoAnimate(activeCamera);
                }
            }
            // Update light position
            PointLight cameraLight = gameLevel.LightManager["CameraLight"] as PointLight;
            cameraLight.Position = activeCamera.Position;
            //update skill
            if (aimEnemy != null)
            {
                if (Vector3.Distance(gameLevel.Player.Transformation.Translate, aimEnemy.Transformation.Translate) < 150)
                {
                    gameLevel.fireBurning.Update(gameTime, aimEnemy);
                    gameLevel.fire.Update(gameTime, aimEnemy);
                }
            }
            else
            {
                gameLevel.fire.Update(gameTime);
                gameLevel.fireBurning.Update(gameTime);
            }

            foreach (Enemy enemy in gameLevel.EnemyList)
            {
                if (enemy.BoundingSphere.Intersects(activeCamera.Frustum) ||
                    enemy.State == Enemy.EnemyState.ChasePlayer ||
                    enemy.State == Enemy.EnemyState.AttackPlayer)
                {
                    foreach (Acher acher in gameLevel.AcherList)
                    {
                        
                         
                        if (!acher.IsDead&&Vector3.Distance(acher.Transformation.Translate, enemy.Transformation.Translate) < 150)
                        {
                            enemy.aimEnemy = acher;
                            break;
                        }
                    }
                    if (Vector3.Distance(gameLevel.Player.Transformation.Translate, enemy.Transformation.Translate) < 130)
                    {
                        enemy.aimEnemy = gameLevel.Player;
                    }
                    enemy.Update(gameTime);
                }

            }
            //Update ali
            foreach (Acher acher in gameLevel.AcherList)
            {
                if (acher.BoundingSphere.Intersects(activeCamera.Frustum))
                {
                    foreach (Enemy enemy in gameLevel.EnemyList)
                    {
                        if (!enemy.IsDead&&Vector3.Distance(acher.Transformation.Translate, enemy.Transformation.Translate) < 100)
                        {
                            acher.aimEnemy = enemy;
                            break;
                        }
                        
                    }
                    if (acher.Fire)
                    {
                        Bullet bullet = new Bullet(Game);
                        acher.Fire = false;
                        if (aimEnemy != null)
                        
                        bullet.TPScam = gameLevel.CameraManager.ActiveCamera;
                        bullet.Position = acher.Weapon.FirePosition;
                        bullet.Velocity = acher.Chase;
                        gameLevel.BulletList.Add(bullet);
                        
                    }
                    
                    acher.Update(gameTime);
                }

            }
            
            if (gameLevel.BulletList != null)
                foreach (Bullet bullet in gameLevel.BulletList)
                    if (bullet.BulletBounding.Intersects(activeCamera.Frustum)||bullet.Position.Y < gameLevel.Terrain.GetHeight(new Vector2(bullet.Position.X, bullet.Position.Z)))
                    {
                        
                        bullet.TPScam = gameLevel.CameraManager.ActiveCamera;
                        if (bullet.BulletBounding.Intersects(gameLevel.Player.BoundingBox))
                        {
                            
                            bullet.Position = new Vector3(0, 1000, 0);
                            bullet.Velocity = Vector3.Zero;

                        }
                        
                        bullet.Update(gameTime);

                    }
            // Update scene objects
            gameLevel.SkyDome.Update(gameTime);
            gameLevel.Terrain.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.DepthBuffer, Color.White, 1.0f, 255);

            BaseCamera activeCamera = gameLevel.CameraManager.ActiveCamera;

            gameLevel.SkyDome.Draw(gameTime);
            gameLevel.Terrain.Draw(gameTime);
            gameLevel.Player.Draw(gameTime);
            //Draw effect
            if (aimEnemy != null)
            {
                gameLevel.fireBurning.Draw(gameLevel.CameraManager);
                gameLevel.fire.Draw(gameLevel.CameraManager);
            }
            fire.SetCamera(activeCamera.View, activeCamera.Projection);
            // Draw enemies
            foreach (Enemy enemy in gameLevel.EnemyList)
            {
                if (enemy.BoundingSphere.Intersects(activeCamera.Frustum))
                    enemy.Draw(gameTime);
            }
            //Draw ali
            foreach (Acher enemy in gameLevel.AcherList)
            {
                if (enemy.BoundingSphere.Intersects(activeCamera.Frustum))
                    enemy.Draw(gameTime);
            }
            //Draw static
            foreach (StaticModel tree in gameLevel.staticModel)
            {
                if (tree.ModelSphere.Intersects(activeCamera.Frustum))
                    tree.DrawNoAnimate(activeCamera);
            }
            if(gameLevel.BulletList != null)
            foreach (Bullet enemy in gameLevel.BulletList)
            {
               
                    
                    if (enemy.BulletBounding.Intersects(activeCamera.Frustum))
                     
                        enemy.Draw(gameTime);
                    
                
            }
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.SaveState);

            // Project weapon target
            weaponTargetPosition = GraphicsDevice.Viewport.Project(weaponTargetPosition,
                activeCamera.Projection, activeCamera.View, Matrix.Identity);
            
            // Draw weapon target
            int weaponRectangleSize = GraphicsDevice.Viewport.Width / 40;
            if (activeCamera == gameLevel.CameraManager["FPSCamera"])
            {
                if (aimEnemy == null)
                    spriteBatch.Draw(weaponTargetTexture, new Rectangle(
                        (int)(weaponTargetPosition.X - weaponRectangleSize * 0.5f),
                        (int)(weaponTargetPosition.Y - weaponRectangleSize * 0.5f),
                        weaponRectangleSize, weaponRectangleSize),
                      Color.White);
                else {
                    spriteBatch.Draw(weaponTargetTexture, new Rectangle(
                        (int)(weaponTargetPosition.X - weaponRectangleSize * 0.5f),
                        (int)(weaponTargetPosition.Y - weaponRectangleSize * 0.5f),
                        weaponRectangleSize, weaponRectangleSize),
                      Color.Red);
           
                }
            }
            // Draw GUI text
            //Draw GUI image
            spriteBatch.Draw(Game.Content.Load<Texture2D>(GameAssetsPath.GRAPHICS2D_PATH + "statusBG"), new Rectangle(Game.Window.ClientBounds.Width / 4, Game.Window.ClientBounds.Height - 50, 700, 50), Color.White);
            spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/LiveHp"), new Rectangle(Game.Window.ClientBounds.Width / 4 + 50, Game.Window.ClientBounds.Height - 25, 300 * gameLevel.Player.Life / gameLevel.Player.MaxLife, 25), Color.White);
            spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/LiveMp"), new Rectangle(Game.Window.ClientBounds.Width / 4 + 400, Game.Window.ClientBounds.Height - 25, 300 * gameLevel.Player.Mana / gameLevel.Player.MaxMana, 25), Color.White);

            spriteBatch.Draw(Game.Content.Load<Texture2D>("2Dgraphics/Exp"), new Rectangle(Game.Window.ClientBounds.Width / 4 + 65, Game.Window.ClientBounds.Height - 50, 300 * (int)gameLevel.Player.Exp / (int)gameLevel.Player.MaxExp, 25), Color.White);


            if (aimEnemy != null)
            {
                spriteBatch.Draw(Game.Content.Load<Texture2D>(GameAssetsPath.GRAPHICS2D_PATH + "EnemyStatusBG"), new Rectangle(Game.Window.ClientBounds.Width / 4, 0, 500, 40), Color.White);
                spriteBatch.Draw(Game.Content.Load<Texture2D>(GameAssetsPath.GRAPHICS2D_PATH + "LiveHP"), new Rectangle(Game.Window.ClientBounds.Width / 4 + 200, 7, 300 * aimEnemy.Life / aimEnemy.MaxLife, 25), Color.White);
                if (aimEnemy is Enemy)
                    spriteBatch.DrawString(spriteFont, aimEnemy.name + " " + ((Enemy)aimEnemy).EnemyType.ToString(), new Vector2(Game.Window.ClientBounds.Width / 4, 0), Color.Green);
               
            }
            spriteBatch.DrawString(spriteFont, "BULLETS: " + gameLevel.Player.Weapon.BulletsCount.ToString(), new Vector2(10, 75),
                Color.Red);
            spriteBatch.DrawString(spriteFont, "LV: " + gameLevel.Player.Level.ToString(), new Vector2(10, 105),
                Color.Red);
            //Draw icon skill
            if (gameLevel.Player.Ski > 30)
            {
                spriteBatch.Draw(Game.Content.Load<Texture2D>(GameAssetsPath.GRAPHICS2D_PATH + "fire"), new Rectangle(40, Game.Window.ClientBounds.Height - 40, 40, 40), Color.White);
                spriteBatch.Draw(Game.Content.Load<Texture2D>(GameAssetsPath.GRAPHICS2D_PATH + "fireDown"), new Rectangle(40, Game.Window.ClientBounds.Height - 40, 40, 40 * (int)gameLevel.fire.Time/ gameLevel.fire.CoolDown), Color.White);
                if (gameLevel.Player.Ski > 50)
                {
                    spriteBatch.Draw(Game.Content.Load<Texture2D>(GameAssetsPath.GRAPHICS2D_PATH + "Evil"), new Rectangle(80, Game.Window.ClientBounds.Height - 40, 40, 40), Color.White);
                    spriteBatch.Draw(Game.Content.Load<Texture2D>(GameAssetsPath.GRAPHICS2D_PATH + "EvilDown"), new Rectangle(80, Game.Window.ClientBounds.Height - 40, 40, 40 * (int)gameLevel.fireBurning.Time / (int)gameLevel.fireBurning.CoolDown), Color.White);
                    if (gameLevel.Player.Ski > 70)
                    {
                        spriteBatch.Draw(Game.Content.Load<Texture2D>(GameAssetsPath.GRAPHICS2D_PATH + "Amor"), new Rectangle(120, Game.Window.ClientBounds.Height - 40, 40, 40), Color.White);
                        //spriteBatch.Draw(Game.Content.Load<Texture2D>(GameAssetsPath.GRAPHICS2D_PATH + "frostDown"), new Rectangle(120, Game.Window.ClientBounds.Height - 40, 40, 40 * (int)Count3 / ((int)gameLevel.Player.delayNovaFrost * 100)), Color.White);
                    }
                }
            }
            spriteBatch.End();
            
            base.Draw(gameTime);

          
        }
        public void Addparticle(ParticleSystem part, Vector3 Pos)
        {
            part.AddParticle(Pos, Vector3.Up * 3);

        }
    }
}
