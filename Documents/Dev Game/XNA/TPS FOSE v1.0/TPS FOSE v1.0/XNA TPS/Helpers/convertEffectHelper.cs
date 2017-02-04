using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace XNA_TPS.Helpers
{
    class convertEffectHelper
    {
        //Thay doi effect tu` basic effect sang 1 effect moi'
        public static Texture2D[] ChangeBasicEffect(GraphicsDevice device, ref Model model, Effect newEffect)
        {
            List<Texture2D> textureList = new List<Texture2D>();

            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart modmeshpart in mesh.MeshParts)
                {
                    BasicEffect oldEffect = (BasicEffect)modmeshpart.Effect;
                    textureList.Add(oldEffect.Texture); ;
                    modmeshpart.Effect = newEffect.Clone(device);
                }

            Texture2D[] textures = textureList.ToArray();
            return textures;
        }
        public static BasicEffect[] StoreBasicEffect(GraphicsDevice device, ref Model model, Effect newEffect)
        {
            List<BasicEffect> effectList = new List<BasicEffect>();

            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart modmeshpart in mesh.MeshParts)
                {
                    BasicEffect oldEffect = (BasicEffect)modmeshpart.Effect;
                    effectList.Add(oldEffect);
                    modmeshpart.Effect = newEffect.Clone(device);
                }

            BasicEffect[] effects = effectList.ToArray();
            return effects;
        }
        public static void ChangeEffect(GraphicsDevice device, ref Model model, Effect newEffect)
        {
            List<Effect> effectList = new List<Effect>();

            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart modmeshpart in mesh.MeshParts)
                {
                    Effect oldEffect = (Effect)modmeshpart.Effect;
                    effectList.Add(oldEffect);
                    modmeshpart.Effect = newEffect.Clone(device);
                }
        }
    }
}
