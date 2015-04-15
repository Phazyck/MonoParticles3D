using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;

namespace MonoParticles
{
    public interface IParticleEngine
    {

        void Update();

        void Draw(SpriteBatch spriteBatch);

    }
}
