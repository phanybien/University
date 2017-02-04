#region File Description
//-----------------------------------------------------------------------------
// ExplosionParticleSystem.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XNA_TPS.GameBase;
#endregion

namespace XNA_TPS.GameBase.ParticleSystems
{
    /// <summary>
    /// Custom particle system for creating the fiery part of the explosions.
    /// </summary>
    class IceParticleSystem : ParticleSystem
    {
        public IceParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "Ice";

            settings.MaxParticles = 1000;

            settings.Duration = TimeSpan.FromSeconds(5);
            settings.DurationRandomness = 2;

            settings.MinHorizontalVelocity = 10;
            settings.MaxHorizontalVelocity = -10;

            settings.MinVerticalVelocity = 10;
            settings.MaxVerticalVelocity = 40;

            settings.EndVelocity = 0;

            settings.MinColor = Color.Black;
            settings.MaxColor = Color.White;

            settings.MinRotateSpeed = 0;
            settings.MaxRotateSpeed = 0;

            settings.MinStartSize = 20;
            settings.MaxStartSize = 20;

            settings.MinEndSize = 20;
            settings.MaxEndSize = 20;

            // Use additive blending.
            settings.SourceBlend = Blend.SourceAlpha;
            settings.DestinationBlend = Blend.One;
        }
    }
}
