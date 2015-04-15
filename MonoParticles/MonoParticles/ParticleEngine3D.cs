using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoParticles
{
    public class ParticleEngine3D
    {
        public Model Model { get; set; }
        public Vector3 Position { get; set; }
        public Texture2D Texture { get; set; }
        public int Limit { get; set; }
        public int ParticleCount { get; private set; }
        private readonly Particle3D[] _particles;

        public ParticleEngine3D(Model model, Vector3 position = new Vector3(), Texture2D texture = null, int limit = 10000)
        {
            Model = model;
            Position = position;
            Texture = texture;
            Limit = limit;

            ParticleCount = 0;
            
            _particles = new Particle3D[limit];

            for (var i = 0; i < limit; ++i)
            {
                _particles[i] = new Particle3D();
            }
        }

        //Method that handles the generation of particles and defines its behaviour
        private void InitParticle()
        {
            if (ParticleCount >= Limit)
            {
                return;
            }

            var particle = _particles[ParticleCount];

            var pos = Position;
            var rnd = RandomF.InsideCircle(100f);
            pos.X += rnd.X;
            pos.Z += rnd.Y;

            particle.Model = Model;
            particle.Texture = Texture;
            particle.Transform =  new Transform
            {
                LocalPosition = 
                    pos,
                    //Vector3.Zero,
                LocalScale = Vector3.Zero,
            };
            particle.Change = new Transform
            {
                LocalPosition = 
                    new Vector3(0f,-RandomF.NextFloat(.1f,.25f),0f),
                    //RandomF.OnUnitSphere() * 0.1f,
                    //Vector3.Zero,
                //LocalScale = Vector3.One * -0.01f,
                LocalScale = 
                    Vector3.One * .01f,
                    //Vector3.Zero,
                Yaw = RandomF.NextFloat(0.02f,0.04f)
            };
            particle.Color = new Color(RandomF.NextFloat(), RandomF.NextFloat(), RandomF.NextFloat());
            particle.LifeTime =
                300;
                //int.MaxValue;

            ParticleCount++;

        }

        private int a = 1;
        private int b = 1;

        public void Update()
        {
            b--;



            //Number of particles added to the system every update
            int total = 1;

            while (b < 0)
            {
                b += a;
                total++;
            }

            for (var i = 0; i < total; i++)
            {
                InitParticle();
            }

            //We remove the particles that reach their life time
            for (var i = 0; i < ParticleCount; i++)
            {
                var particle = _particles[i];

                particle.Update();

                    

                //if (particle.LifeTime > 0)
                if (particle.Transform.LocalPosition.Y >= -48f)
                    continue;

                ParticleCount--;
                _particles[i] = _particles[ParticleCount];
                _particles[ParticleCount] = particle;
                i--;
            }
        }

        public void Draw()
        {
            //Drawing all the particles of the system
            for (var i = 0; i < ParticleCount; ++i)
            {
                _particles[i].Draw();
            }

        }
    }
}