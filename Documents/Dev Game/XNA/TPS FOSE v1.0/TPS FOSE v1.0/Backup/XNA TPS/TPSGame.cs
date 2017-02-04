using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using XNA_TPS.Helpers;
using XNA_TPS.GameBase;
using XNA_TPS.GameLogic.Levels;

using System.IO;
using System.Xml.Serialization;
namespace XNA_TPS
{
    [Serializable]
    public struct GameData
    {
        public float playerLoad;
        public int Str, Int, Ski, Hea, Agi, Exp, Hp, Mp,LV;
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TPSGame : Microsoft.Xna.Framework.Game
    {
        bool IsLoad;
        static String GAME_TITLE = "XNA TPS v1.0";
        GraphicsDeviceManager graphics;
        //stogre
        bool operationPending = false;
        GameData gameData;
        // Game stuff
        helpScreen help;
        InputHelper inputHelper;
        GameScreen gameScreen;
        StartScreen startScreen;
        public TPSGame()
        {
            Window.Title = GAME_TITLE;
            Content.RootDirectory = "Content";
            Components.Add(new GamerServicesComponent(this));
            // Creating and configuring graphics device
            graphics = new GraphicsDeviceManager(this);
            GameSettings gameSettings = SettingsManager.Read(Content.RootDirectory + "/" +
                GameAssetsPath.SETTINGS_PATH + "GameSettings.xml");
            ConfigureGraphicsManager(gameSettings);
           
            // Input helper
            inputHelper = new InputHelper(PlayerIndex.One,
                SettingsManager.GetKeyboardDictionary(gameSettings.KeyboardSettings[0]));
            Services.AddService(typeof(InputHelper), inputHelper);
            //graphics service
            Services.AddService(typeof(GraphicsDeviceManager), graphics);
            // Game Screen
            startScreen = new StartScreen(this);
            gameScreen = new GameScreen(this, LevelCreator.Levels.AlienPlanet);
            help = new helpScreen(this);
            Components.Add(help);
            Components.Add(gameScreen);
            Components.Add(startScreen);
            startScreen.Show();
        }
        void ChangeScreen(Screen oldS, Screen newS)
        {
            oldS.Hide();
            newS.Show();
        }
        /// <summary>
        /// Configure the graphics device manager and checks for shader compatibility
        /// </summary>
        private void ConfigureGraphicsManager(GameSettings gameSettings)
        {
#if XBOX360
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
#else
            graphics.PreferredBackBufferWidth = gameSettings.PreferredWindowWidth;
            graphics.PreferredBackBufferHeight = gameSettings.PreferredWindowHeight;
            graphics.IsFullScreen = gameSettings.PreferredFullScreen;
            
#endif

            // Multi sampling
            graphics.PreferMultiSampling = true;

            // Minimum shader profile required
            graphics.MinimumVertexShaderProfile = ShaderProfile.VS_2_0;
            graphics.MinimumPixelShaderProfile = ShaderProfile.PS_2_0;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()

        {
            gameData = new GameData();
            base.Initialize();

            gameData.playerLoad = gameScreen.gameLevel.Player.Weapon.BulletsCount;
            
            
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update input
            inputHelper.Update();
            if(gameScreen.Enabled)
                if(inputHelper.IsKeyJustPressed(Keys.Escape))
                    ChangeScreen(gameScreen, startScreen);

            if (startScreen.Enabled)
            {
                if (startScreen.New && inputHelper.IsKeyJustPressed(Keys.Enter))
                    ChangeScreen(startScreen, gameScreen);
                if (startScreen.Load && inputHelper.IsKeyJustPressed(Keys.Enter))
                {
                    ChangeScreen(startScreen, gameScreen);
                    gameScreen.Initialize();
                    gameScreen.Update(gameTime);
                    gameScreen.Draw(gameTime);
                    Guide.BeginShowStorageDeviceSelector(FindStorageDevice, "loadRequest");
                }
                if (startScreen.Option && inputHelper.IsKeyJustPressed(Keys.Enter))
                    ChangeScreen(startScreen, help);
                if (startScreen.Exit && inputHelper.IsKeyJustPressed(Keys.Enter))
                    Exit();
            }
            if (help.Enabled)
                if(inputHelper.IsKeyJustPressed(Keys.Space))
                ChangeScreen(help, gameScreen);
            if (!Guide.IsVisible && !operationPending)
            {
                if (inputHelper.IsKeyJustPressed(Keys.C))
                {
                    operationPending = true;
                    Guide.BeginShowStorageDeviceSelector(FindStorageDevice, "saveRequest");
                }
                if (inputHelper.IsKeyJustPressed(Keys.V))
                {
                    operationPending = true;
                    Guide.BeginShowStorageDeviceSelector(FindStorageDevice, "loadRequest");
                }
            }
            
            base.Update(gameTime);
        }
        private void FindStorageDevice(IAsyncResult result)
        {
            StorageDevice storageDevice = Guide.EndShowStorageDeviceSelector(result);
            if (storageDevice != null)
            {
                if (result.AsyncState == "saveRequest")
                    SaveGame(storageDevice);
                else if (result.AsyncState == "loadRequest")
                    LoadGame(storageDevice);
            }
        }

        private void SaveGame(StorageDevice storageDevice)
        {
            gameData.playerLoad = gameScreen.gameLevel.Player.Weapon.BulletsCount;
            gameData.LV = gameScreen.gameLevel.Player.Level;
            gameData.Agi = gameScreen.gameLevel.Player.Agi;
            gameData.Str = gameScreen.gameLevel.Player.Str;
            gameData.Exp = gameScreen.gameLevel.Player.Exp;
            gameData.Int = gameScreen.gameLevel.Player.Int;
            
            gameData.Ski = gameScreen.gameLevel.Player.Ski;
            gameData.Hea = gameScreen.gameLevel.Player.Hea;
            gameData.Hp = gameScreen.gameLevel.Player.Life;
            gameData.Mp = gameScreen.gameLevel.Player.Mana;
            StorageContainer container = storageDevice.OpenContainer("BookCodeWin");
            string fileName = Path.Combine(container.Path, "save0001.sav");

            FileStream saveFile = File.Open(fileName, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameData));

            xmlSerializer.Serialize(saveFile, gameData);
            saveFile.Close();
            container.Dispose();


            operationPending = false;
            
        }

        private void LoadGame(StorageDevice storageDevice)
        {
            StorageContainer container = storageDevice.OpenContainer("BookCodeWin");
            string fileName = Path.Combine(container.Path, "save0001.sav");
            if (File.Exists(fileName))
            {
                FileStream saveFile = File.Open(fileName, FileMode.Open);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameData));

                gameData = (GameData)xmlSerializer.Deserialize(saveFile);
                gameScreen.gameLevel.Player.Level = gameData.LV;
                gameScreen.gameLevel.Player.Weapon.BulletsCount = (int)gameData.playerLoad;
                gameScreen.gameLevel.Player.Agi = gameData.Agi;
                gameScreen.gameLevel.Player.Str = gameData.Str;
                gameScreen.gameLevel.Player.Exp = gameData.Exp;
                gameScreen.gameLevel.Player.Int = gameData.Int;

                gameScreen.gameLevel.Player.Ski = gameData.Ski;
                gameScreen.gameLevel.Player.Hea = gameData.Hea;
                gameScreen.gameLevel.Player.Life = gameData.Hp;
                gameScreen.gameLevel.Player.Mana = gameData.Mp;
                saveFile.Close();


                operationPending = false;
            }
            container.Dispose();
           
        }

       
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
