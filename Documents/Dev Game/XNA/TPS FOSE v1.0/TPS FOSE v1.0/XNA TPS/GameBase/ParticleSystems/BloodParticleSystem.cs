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
    class BloodParticleSystem : ParticleSystem
    {
        public BloodParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "blood";

            settings.MaxParticles = 300;

            settings.Duration = TimeSpan.FromSeconds(5);
            settings.DurationRandomness = 3;

            settings.MinHorizontalVelocity = -20;
            settings.MaxHorizontalVelocity = 20;

            settings.MinVerticalVelocity = -30;
            settings.MaxVerticalVelocity = 0;
            settings.Gravity = new Vector3(0, -3, 0);
            settings.EndVelocity = 0;

            settings.MinColor = Color.Red;
            settings.MaxColor = Color.White;

            settings.MinRotateSpeed = 0;
            settings.MaxRotateSpeed =0;

            settings.MinStartSize = 10;
            settings.MaxStartSize = 10;

            settings.MinEndSize = 20;
            settings.MaxEndSize = 30;

            // Use additive blending.
            settings.SourceBlend = Blend.SourceAlpha;
            settings.DestinationBlend = Blend.One;
        }
    }
}
