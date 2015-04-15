using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoParticles
{
    public class ParticleBasic
    {
        // The position of the particle
        public Vector2 Position { get; set; }
        //Where to move the particle
        public Vector2 Direction { get; set; }
        // The lifetime of the particle
        public int LifeTime { get; set; }
        // The texture that will be drawn to represent the particle
        public Texture2D Texture { get; set; }

        public ParticleBasic(Texture2D texture, Vector2 position, Vector2 direction, int lifeTime)
        {
            Texture = texture;
            Position = position;
            Direction = direction;
            LifeTime = lifeTime;
        }

        public void Update()
        {
            //We reduce the lifeTime of the particle
            LifeTime--;
            // Updating the position in the desired direction
            Position += Direction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Basic drawing
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
