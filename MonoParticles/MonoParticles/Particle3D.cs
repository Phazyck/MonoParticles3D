using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoParticles
{
    public class Particle3D
    {

        // The position of the particle
        public Transform Transform { get; set; }
        // The lifetime of the particle
        public int LifeTime { get; set; }
        // The texture that will be drawn to represent the particle
        public Model Model { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }


        public Transform Change { get; set; }

        public Particle3D()
        {
            Model = null;
            Texture = null;
            Transform = null;
            Color = Color.Black;
            Change = null;
            LifeTime = 0;

        }

        public Particle3D(Model model, Texture2D texture, Transform transform, Transform change,
            Color color, float fadeValue, float fadeRate, int lifeTime)

        {
            Model = model;
            Texture = texture;
            Transform = transform;
            Color = color;
            Change = change;
            LifeTime = lifeTime;

        }

        public void Update()
        {
            LifeTime--;

            Transform.LocalPosition += Change.LocalPosition;
            Transform.LocalScale += Change.LocalScale;
            Transform.Yaw += Change.Yaw;
            Transform.Pitch += Change.Pitch;
            Transform.Roll += Change.Roll;
        }

        public void Draw()
        {
            foreach (var mesh in Model.Meshes)
            {
                foreach (var be in mesh.Effects.Cast<BasicEffect>())
                {
                    be.LightingEnabled = true;

                    be.AmbientLightColor = new Vector3(0.053f, 0.098f, 0.181f) * 1.2f;
                    be.SpecularColor = Color.White.ToVector3();
                    be.DiffuseColor =
                        //Color.ToVector3();
                        Color.Gray.ToVector3();
                    
                    be.DirectionalLight0.Enabled = true;
                    be.DirectionalLight0.DiffuseColor = Color.White.ToVector3();
                    be.DirectionalLight0.Direction =
                        Vector3.Transform(Vector3.Forward, 
                        
                        Matrix.Invert(
                            Matrix.CreateFromYawPitchRoll(
                                Transform.Yaw,
                                Transform.Pitch,
                                Transform.Roll
                            ))
                            );
                    be.DirectionalLight0.SpecularColor = Color.White.ToVector3();
                    be.SpecularPower = 100f;

                    be.DirectionalLight1.Enabled = false;
                    be.DirectionalLight2.Enabled = false;

                    be.Projection = Game2.proj;
                    be.View = Transform.GetMatrix() * Game2.view;
                    be.World = Matrix.Identity;

                    if (Texture != null)
                    {
                        be.Texture = Texture;
                        be.TextureEnabled = true;
                    }
                    else
                    {
                        be.TextureEnabled = false;
                    }
                }
                mesh.Draw();
            }
        }
    }
}
