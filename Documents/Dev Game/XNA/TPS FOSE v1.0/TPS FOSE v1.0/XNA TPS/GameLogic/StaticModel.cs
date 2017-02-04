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
using XNA_TPS.GameBase;
using XNA_TPS.GameBase.Cameras;
using XNA_TPS.GameBase.Shapes;

namespace XNA_TPS.GameLogic
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StaticModel : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //random
        int ran = RandomHelper.GetRandomInt(10);
        //model for non animate
        Vector3 position;
        Matrix[] transform;
        Model model;
        Matrix world;
 
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }
        public BoundingSphere ModelSphere
        {
            get
            {
                BoundingSphere origSphere = (BoundingSphere)model.Tag;
                return XNAUtils.TransformBoundingSphere(origSphere, world);
            }
        }
        public StaticModel(Game game)
            : base(game)
        {
            world = Matrix.CreateScale(Vector3.One) * Matrix.CreateRotationY(ran / 5) * Matrix.CreateTranslation(position);
        }
        public void Load(Model mo)
        {
            model = XNAUtils.LoadModelWithBoundingSphere(ref transform, ref mo);
        }
        
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void UpdateHeight(Terrain terrain)
        {
            // Get terrain height
            float terrainHeight = terrain.GetHeight(new Vector2(position.X, position.Z));
            Vector3 newPosition = position;

            if (position.Y < (terrainHeight) )
            {
                position.Y = terrainHeight;
            }
            world = Matrix.CreateScale(Vector3.One) * Matrix.CreateRotationY(ran / 5) * Matrix.CreateTranslation(position);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        
        private void DrawModel(Model model, Matrix worldMatrix, Matrix[] modelTransforms,BaseCamera camera)
        {
            model.CopyAbsoluteBoneTransformsTo(modelTransforms);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = modelTransforms[mesh.ParentBone.Index] * worldMatrix;
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                }
                mesh.Draw();
            }
        }
        public void DrawNoAnimate(BaseCamera camera)
        {
            DrawModel(model, world, transform, camera);
        }
    }
}