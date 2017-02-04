using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using XNA_TPS.GameBase;
using XNA_TPS.GameBase.Materials;
using XNA_TPS.GameBase.Cameras;
using XNA_TPS.GameBase.Lights;
using XNA_TPS.GameBase.Shapes;
using XNA_TPS.Helpers;


namespace XNA_TPS.GameLogic.Levels
{
    public static class LevelCreator
    {
        public enum Levels
        {
            AlienPlanet
        }

        public static GameLevel CreateLevel(Game game, Levels level)
        {
            // Remove all services from the last level
            game.Services.RemoveService(typeof(CameraManager));
            game.Services.RemoveService(typeof(LightManager));
            game.Services.RemoveService(typeof(Terrain));
            
            switch (level)
            {
                case Levels.AlienPlanet:
                    return CreateAlienPlanetLevel(game);
                    break;

                default:
                    throw new ArgumentException("Invalid game level");
                    break;
            }
        }

        private static GameLevel CreateAlienPlanetLevel(Game game)
        {
            ContentManager Content = game.Content;
            GameLevel gameLevel = new GameLevel();

            // Cameras and lights
            AddCameras(game, ref gameLevel);
            gameLevel.LightManager = new LightManager();
            gameLevel.LightManager.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
            gameLevel.LightManager.Add("MainLight", 
                new PointLight(new Vector3(10000, 10000, 10000), new Vector3(0.2f, 0.2f, 0.2f)));
            gameLevel.LightManager.Add("CameraLight",
                new PointLight(Vector3.Zero, Vector3.One));
            
            game.Services.AddService(typeof(CameraManager), gameLevel.CameraManager);
            game.Services.AddService(typeof(LightManager), gameLevel.LightManager);

            // Terrain
            gameLevel.Terrain = new Terrain(game);
            gameLevel.Terrain.Initialize();
            gameLevel.Terrain.Load(game.Content, "Terrain1", 8.0f, 1.0f);

            // Terrain material
            TerrainMaterial terrainMaterial = new TerrainMaterial();
            terrainMaterial.LightMaterial = new LightMaterial(
                new Vector3(0.8f), new Vector3(0.3f), 32.0f);
            terrainMaterial.DiffuseTexture1 = GetTextureMaterial(game, "Terrain1", new Vector2(40, 40));
            terrainMaterial.DiffuseTexture2 = GetTextureMaterial(game, "Terrain2", new Vector2(25, 25));
            terrainMaterial.DiffuseTexture3 = GetTextureMaterial(game, "Terrain3", new Vector2(15, 15));
            terrainMaterial.DiffuseTexture4 = GetTextureMaterial(game, "Terrain4", Vector2.One);
            terrainMaterial.AlphaMapTexture = GetTextureMaterial(game, "AlphaMap", Vector2.One);
            terrainMaterial.NormalMapTexture = GetTextureMaterial(game, "Rockbump", new Vector2(128, 128));
            gameLevel.Terrain.Material = terrainMaterial;
            game.Services.AddService(typeof(Terrain), gameLevel.Terrain);

            // Sky
            gameLevel.SkyDome = new SkyDome(game);
            gameLevel.SkyDome.Initialize();
            gameLevel.SkyDome.Load("SkyDome");
            gameLevel.SkyDome.TextureMaterial = GetTextureMaterial(game, "SkyDome", Vector2.One);

            // Player
            gameLevel.Player = new Player(game, UnitTypes.PlayerType.Marine);
            gameLevel.Player.Initialize();
            gameLevel.Player.Transformation = new Transformation(new Vector3(-210, 0, 10),
                new Vector3(0, 70, 0), Vector3.One);
            gameLevel.Player.AnimatedModel.AnimationSpeed = 1.3f;
            gameLevel.Player.AttachWeapon(UnitTypes.PlayerWeaponType.MachineGun);

            // Player chase camera offsets
            gameLevel.Player.ChaseOffsetPosition = new Vector3[2];
            gameLevel.Player.ChaseOffsetPosition[0] = new Vector3(3.0f, 5.0f, 0.0f);
            gameLevel.Player.ChaseOffsetPosition[1] = new Vector3(3.0f, 4.0f, 0.0f);

            // Enemies
            gameLevel.EnemyList = ScatterEnemies(game, 20, 150, 800, gameLevel.Player);
            //ali
            gameLevel.AcherList = ScatterAchers(game, 20, 150, 800);
            //attach weapon for ali
            for (int i = 0; i < gameLevel.AcherList.Count; i++)
                gameLevel.AcherList[i].AttachWeapon(UnitTypes.PlayerWeaponType.MachineGun);
            //skill
            gameLevel.fire = new IceBlast(game);
            gameLevel.fireBurning = new FireBurning(game);
            gameLevel.fire.Shooter = gameLevel.Player;
            gameLevel.amor = new Amor(game);
            gameLevel.amor.Shooter = gameLevel.Player;
            gameLevel.fireBurning.Shooter = gameLevel.Player;
            //bullet List
            gameLevel.BulletList = new List<Bullet>();
            //static model
            gameLevel.staticModel = CreateStaticModel(game, 20, 200, 500, gameLevel.Player);
            foreach (StaticModel model in gameLevel.staticModel)
                model.UpdateHeight(gameLevel.Terrain);
            return gameLevel;
            
            
            
        }
        //method de create static model
        //minh thiet ke de? no load dc nhieu loai model khac nhau, tuy nhienko co model nen minh chi load 1 cai
        //cac ban co the mod lai de (phan switch) de load dc nhieu loai model mot cach ngau nhien
        static List<StaticModel> CreateStaticModel(Game game, int numTrees, float min, int distance, Player player)
        {
            List<StaticModel> TreeList = new List<StaticModel>();
            for (int i = 0; i < numTrees; i++)
            {
                int Index = 0;
                string ModelName;
                Index = RandomHelper.GetRandomInt(5);
                switch (Index)
                {
                    case 0:
                        ModelName = "model";
                        break;
                    case 1:
                        ModelName = "model2";
                        break;
                    case 2:
                        ModelName = "model4";
                        break;
                    default:
                        ModelName = "model3";
                        break;
                }
                ModelName = "StaticModel/soilder";
                StaticModel tree = new StaticModel(game);
                tree.Initialize();

                tree.Load(game.Content.Load<Model>(ModelName));
                tree.Position = new Vector3(1500 - RandomHelper.GetRandomInt(3000), 0, 1500 - RandomHelper.GetRandomInt(3000));
                TreeList.Add(tree);
            }
            return TreeList;
        }
        private static TextureMaterial GetTextureMaterial(Game game, string textureFilename, Vector2 tile)
        {
            Texture2D texture = game.Content.Load<Texture2D>(GameAssetsPath.TEXTURES_PATH + textureFilename);
            return new TextureMaterial(texture, tile);
        }

        private static void AddCameras(Game game, ref GameLevel gameLevel)
        {
            float aspectRate = (float)game.GraphicsDevice.Viewport.Width /
                game.GraphicsDevice.Viewport.Height;

            ThirdPersonCamera followCamera = new ThirdPersonCamera();
            followCamera.SetPerspectiveFov(60.0f, aspectRate, 0.1f, 2000);
            followCamera.SetChaseParameters(20.0f, 25.0f, 30.0f, 14.0f);

            ThirdPersonCamera fpsCamera = new ThirdPersonCamera();
            fpsCamera.SetPerspectiveFov(45.0f, aspectRate, 0.1f, 2000);
            fpsCamera.SetChaseParameters(5.0f, 6.0f, 6.0f, 6.0f);
            
            gameLevel.CameraManager = new CameraManager();
            gameLevel.CameraManager.Add("FollowCamera", followCamera);
            gameLevel.CameraManager.Add("FPSCamera", fpsCamera);
        }
        //load enemy voi' cac level va` thuoc tinh khac nhau
        private static List<Enemy> ScatterEnemies(Game game, int numEnemies,
            float minDistance, int distance, Player player)
        {
            List<Enemy> enemyList = new List<Enemy>();

            for (int i = 0; i < 12; i++)
            {
                Enemy enemy = new Enemy(game, UnitTypes.EnemyType.LV1);
                enemy.Initialize();

                // Generate a random position
                Vector3 offset = RandomHelper.GeneratePositionXZ(distance);
                while (Math.Abs(offset.X) < minDistance && Math.Abs(offset.Z) < minDistance)
                    offset = RandomHelper.GeneratePositionXZ(distance);

                enemy.Transformation = new Transformation(player.Transformation.Translate +
                    offset, Vector3.Zero, Vector3.One);

                enemy.Player = player;
                enemyList.Add(enemy);
            }
            for (int i = 0; i < 10; i++)
            {
                Enemy enemy = new Enemy(game, UnitTypes.EnemyType.LV3);
                enemy.Initialize();

                // Generate a random position
                Vector3 offset = RandomHelper.GeneratePositionXZ(distance);
                while (Math.Abs(offset.X) < minDistance && Math.Abs(offset.Z) < minDistance)
                    offset = RandomHelper.GeneratePositionXZ(distance);

                enemy.Transformation = new Transformation(player.Transformation.Translate +
                    offset, Vector3.Zero, Vector3.One*1.2f);

                enemy.Player = player;
                enemyList.Add(enemy);
            }
            for (int i = 0; i < 7; i++)
            {
                Enemy enemy = new Enemy(game, UnitTypes.EnemyType.LV5);
                enemy.Initialize();

                // Generate a random position
                Vector3 offset = RandomHelper.GeneratePositionXZ(distance);
                while (Math.Abs(offset.X) < minDistance && Math.Abs(offset.Z) < minDistance)
                    offset = RandomHelper.GeneratePositionXZ(distance);

                enemy.Transformation = new Transformation(player.Transformation.Translate +
                    offset, Vector3.Zero, Vector3.One*1.4f);

                enemy.Player = player;
                enemyList.Add(enemy);
            }
            for (int i = 0; i < 5; i++)
            {
                Enemy enemy = new Enemy(game, UnitTypes.EnemyType.LV8);
                enemy.Initialize();

                // Generate a random position
                Vector3 offset = RandomHelper.GeneratePositionXZ(distance);
                while (Math.Abs(offset.X) < minDistance && Math.Abs(offset.Z) < minDistance)
                    offset = RandomHelper.GeneratePositionXZ(distance);

                enemy.Transformation = new Transformation(player.Transformation.Translate +
                    offset, Vector3.Zero, Vector3.One * 1.8f);

                enemy.Player = player;
                enemyList.Add(enemy);
            }
            for (int i = 0; i < 1; i++)
            {
                Enemy enemy = new Enemy(game, UnitTypes.EnemyType.Boss);
                enemy.Initialize();

                // Generate a random position
                Vector3 offset = RandomHelper.GeneratePositionXZ(distance);
                while (Math.Abs(offset.X) < minDistance && Math.Abs(offset.Z) < minDistance)
                    offset = RandomHelper.GeneratePositionXZ(distance);

                enemy.Transformation = new Transformation(player.Transformation.Translate +
                    offset, Vector3.Zero, Vector3.One * 3f);

                enemy.Player = player;
                enemyList.Add(enemy);
            }
            for (int i = 0; i < 3; i++)
            {
                Enemy enemy = new Enemy(game, UnitTypes.EnemyType.LV10);
                enemy.Initialize();

                // Generate a random position
                Vector3 offset = RandomHelper.GeneratePositionXZ(distance);
                while (Math.Abs(offset.X) < minDistance && Math.Abs(offset.Z) < minDistance)
                    offset = RandomHelper.GeneratePositionXZ(distance);

                enemy.Transformation = new Transformation(player.Transformation.Translate +
                    offset, Vector3.Zero, Vector3.One * 2.1f);

                enemy.Player = player;
                enemyList.Add(enemy);
            }
            return enemyList;
        }
        private static List<Acher> ScatterAchers(Game game, int numEnemies,
            float minDistance, int distance)
        {
            List<Acher> enemyList = new List<Acher>();

            for (int i = 0; i < 12; i++)
            {
                Acher enemy = new Acher(game, UnitTypes.AcherType.LV2);
                enemy.Initialize();

                enemy.Transformation = new Transformation(new Vector3 (1000- RandomHelper.GetRandomInt(2000), 0,1000- RandomHelper.GetRandomInt(2000)),Vector3.Zero, Vector3.One);

                enemyList.Add(enemy);
            }
            for (int i = 0; i < 10; i++)
            {
                Acher enemy = new Acher(game, UnitTypes.AcherType.LV4);
                enemy.Initialize();

                enemy.Transformation = new Transformation(new Vector3(1000 - RandomHelper.GetRandomInt(2000), 0, 1000 - RandomHelper.GetRandomInt(2000)), Vector3.Zero, Vector3.One);

                enemyList.Add(enemy);
            }
            for (int i = 0; i < 8; i++)
            {
                Acher enemy = new Acher(game, UnitTypes.AcherType.LV6);
                enemy.Initialize();

                // Generate a random position

                enemy.Transformation = new Transformation(new Vector3(1000 - RandomHelper.GetRandomInt(2000), 0, 1000 - RandomHelper.GetRandomInt(2000)), Vector3.Zero, Vector3.One);

                enemyList.Add(enemy);
            }
            for (int i = 0; i < 6; i++)
            {
                Acher enemy = new Acher(game, UnitTypes.AcherType.LV8);
                enemy.Initialize();

                enemy.Transformation = new Transformation(new Vector3(1000 - RandomHelper.GetRandomInt(2000), 0, 1000 - RandomHelper.GetRandomInt(2000)), Vector3.Zero, Vector3.One);
                
                enemyList.Add(enemy);
            }
            return enemyList;
        }
    }
}
