using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoParticles
{
    public class ParticleAdvanced
    {
        // The position of the particle
        public Vector2 Position { get; set; }
        //Where to move the particle
        public Vector2 Direction { get; set; }
        // The lifetime of the particle
        public int LifeTime { get; set; }
        // The texture that will be drawn to represent the particle
        public Texture2D Texture { get; set; }
        // The rotation of the particle
        public float Rotation { get; set; }
        // The rotation rate
        public float RotationRate { get; set; }
        // The color of the particle
        public Color Color { get; set; }
        // The fading of the particle
        public float FadeValue { get; set; }
        // The fading rate of the particle
        // they can dissapear/apper smoothly with this
        public float FadeRate { get; set; }
        // The size of the particle
        public float Size { get; set; }
        // The size rate of the particle to make them bigger/smaller
        // over time
        public float SizeRate { get; set; }



        public ParticleAdvanced(Texture2D texture, Vector2 position, Vector2 direction,
            float rotation, float rotationRate, Color color, float fadeValue, float fadeRate,
            float size, float sizeRate, int lifeTime)
        {
            Texture = texture;
            Position = position;
            Direction = direction;
            Rotation = rotation;
            RotationRate = rotationRate;
            Color = color;
            FadeValue = fadeValue;
            FadeRate = fadeRate;
            Size = size;
            SizeRate = sizeRate;
            LifeTime = lifeTime;
        }

        public void Update()
        {
            LifeTime--;
            Position += Direction;
            Rotation += RotationRate;
            FadeValue += FadeRate;
            Size += SizeRate;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture, 
                Position, 
                null, 
                Color * FadeValue, 
                Rotation, 
                Vector2.Zero, 
                Size, 
                SpriteEffects.None, 
                0f
            );
        }
    }
}
