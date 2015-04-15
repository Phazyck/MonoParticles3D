using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoParticles
{
    public class ParticleEngineAdvanced : IParticleEngine
    {
        //A random number generator to add realism
        private readonly Random _random;
        //The emitter of the particles
        public Vector2 EmitterLocation { get; set; }
        //The pools of particles handled by the system
        private readonly List<ParticleAdvanced> _particles;
        //The texture used for the particles
        public Texture2D Texture { get; set; }

        public ParticleEngineAdvanced(Texture2D texture, Vector2 location)
        {
            EmitterLocation = location;
            Texture = texture;
            _particles = new List<ParticleAdvanced>();
            _random = new Random();
        }

        private ParticleAdvanced GenerateNewParticle()
        {
            //Create the particles at the emitter and
            //move it around randomly just a bit
            var position = EmitterLocation + new Vector2(
                (float)(_random.NextDouble() * 10 - 10),
                (float)(_random.NextDouble() * 10 - 10)
            );
            
            //Just a random direction. Effectively, they
            //go in every direction
            var direction = new Vector2(
                (float)(_random.NextDouble() * 2 - 1),
                (float)(_random.NextDouble() * 2 - 1)
            );
            
            // A random life time constrained to a minimum and maximum value
            var lifeTime = 1000 + _random.Next(400);

            // The particles are rotated so that they seem to come
            // from a point in the infinite. No need to rotate them over time
            var rotation = -1 * (float)Math.Atan2(direction.X, direction.Y);
            const float rotationRate = 0;
            // A color in the blueish tones
            var color = new Color(0f, (float)_random.NextDouble(), 1f);
            // An initial random size and decreasing over time
            var size = (float)_random.NextDouble() / 4;
            const float sizeRate = -0.01f;
            // A 0 fade value to start (so they don't pop in)
            // and an increase over time
            const float fadeValue = 0f;
            var fadeRate = 0.001f * (float)_random.NextDouble();

            return new ParticleAdvanced(Texture, position, direction,
                rotation, rotationRate, color, fadeValue,
                fadeRate, size, sizeRate, lifeTime);
        }

        public void Update()
        {
            //Number of particles added to the system every update
            const int total = 10;
            for (var i = 0; i < total; i++)
            {
                _particles.Add(GenerateNewParticle());
            }

            //We remove the particles that reach their life time
            for (var particle = 0; particle < _particles.Count; particle++)
            {
                _particles[particle].Update();
                if (_particles[particle].LifeTime > 0) continue;

                _particles.RemoveAt(particle);
                particle--;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Drawing all the particles of the system
            foreach (var particle in _particles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}