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
    class ExplosionParticleSystem : ParticleSystem
    {
        public ExplosionParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "explosion";

            settings.MaxParticles = 300;

            settings.Duration = TimeSpan.FromSeconds(5);
            settings.DurationRandomness = 2;

            settings.MinHorizontalVelocity = -50;
            settings.MaxHorizontalVelocity = 50;

            settings.MinVerticalVelocity = -50;
            settings.MaxVerticalVelocity = 50;

            settings.EndVelocity = 0;

            settings.MinColor = Color.DarkGray;
            settings.MaxColor = Color.Gray;

            settings.MinRotateSpeed = -1;
            settings.MaxRotateSpeed = 1;

            settings.MinStartSize = 10;
            settings.MaxStartSize = 10;

            settings.MinEndSize = 100;
            settings.MaxEndSize = 100;

            // Use additive blending.
            settings.SourceBlend = Blend.BlendFactor;
            settings.DestinationBlend = Blend.BlendFactor;
        }
    }
}
