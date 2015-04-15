using Microsoft.Xna.Framework;

namespace MonoParticles
{
    public class Transform
    {
        public Transform Parent { get; set; }
        public Vector3 LocalPosition { get; set; }
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public float Roll { get; set; }

        public Vector3 LocalScale { get; set; }

        public Matrix GetMatrix()
        {
            var matrix = Parent == null ? Matrix.Identity : Parent.GetMatrix();

            var scale = Matrix.CreateScale(LocalScale);
            var rotation = Matrix.CreateFromYawPitchRoll(Yaw, Pitch, Roll);
            var translation = Matrix.CreateTranslation(LocalPosition);

            return scale*rotation*translation*matrix;
        }

        public Transform(Transform parent = null)
        {
            Parent = parent;

            LocalPosition = Vector3.Zero;
            Yaw = Pitch = Roll = 0f;
            LocalScale = Vector3.One;
        }

    }
}
