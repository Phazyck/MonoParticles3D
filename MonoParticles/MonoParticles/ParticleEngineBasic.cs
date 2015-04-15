using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoParticles
{
    public class ParticleEngineBasic : IParticleEngine
    {
        //A random number generator to add realism
        private readonly Random _random;
        //The emitter of the particles
        public Vector2 EmitterLocation { get; set; }
        //The pools of particles handled by the system
        private readonly List<ParticleBasic> _particles;
        //The texture used for the particles
        public Texture2D Texture { get; set; }

        public ParticleEngineBasic(Texture2D texture, Vector2 location)
        {
            EmitterLocation = location;
            Texture = texture;
            _particles = new List<ParticleBasic>();
            _random = new Random();
        }

        //Method that handles the generation of particles and defines
        //its behaviour
        private ParticleBasic GenerateNewParticle()
        {
            //Create the particles at the emitter
            var position = EmitterLocation;
            //Just a random direction
            var direction = new Vector2(
                (float)(_random.NextDouble() * 2 - 1),
                (float)(_random.NextDouble() * 2 - 1));
            // A random life time constrained to a maximum value
            var lifetime = 1 + _random.Next(400);
            return new ParticleBasic(Texture, position, direction, lifetime);
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

                if (_particles[particle].LifeTime > 0) 
                    continue;

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