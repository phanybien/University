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
using XNA_TPS.GameBase.Shapes;
using XNA_TPS.GameBase;
using XNA_TPS.GameLogic;
using XNA_TPS.GameBase.Cameras;
using XNA_TPS.GameBase.ParticleSystems;
namespace XNA_TPS.GameLogic

{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Bullet : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public Boolean IsUsed = true;
        Model bulletModel;
        BaseCamera TPS;
        public BaseCamera TPScam
        {
            set { TPS = value; }
        }
        Vector3 position;
        Vector3 velocity;
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector3 Velocity
        {
            set { velocity = value; }
        }
        Matrix world;
        public Model BulletModel
        {
            set
            {
                bulletModel = value;
            }
        }
        public BoundingSphere BulletBounding
        {
            get { return new BoundingSphere(position, 3f); }
        }
        public Bullet(Game game)
            : base(game)
        {
            bulletModel = game.Content.Load<Model>(GameAssetsPath.MODELS_PATH + "ammo");

            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime time)
        {

            position += velocity;
            base.Update(time);
        }

        public override void Draw(GameTime time)
        {

            world = Matrix.CreateScale(0.5f*Vector3.One)*Matrix.CreateTranslation(position);
            foreach (ModelMesh mesh in bulletModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = TPS.View;
                    effect.Projection = TPS.Projection;
                }
                mesh.Draw();
            }
        }
    }
}