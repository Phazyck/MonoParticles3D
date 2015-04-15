using System;
using Microsoft.Xna.Framework;

namespace MonoParticles
{
    public class RandomF
    {
        private static readonly Random Random = new Random();

        public static float NextFloat()
        {
            return (float) Random.NextDouble();
        }

        public static float NextFloat(float to)
        {
            return to * (float)Random.NextDouble();
        }

        public static float NextFloat(float from, float to)
        {
            return from + (to - from)*NextFloat();
        }

        public static Vector2 InsideCircle(float radius)
        {
            var rnd = NextFloat() * MathF.Tau;

            radius *= NextFloat();

            var x = MathF.Cos(rnd);
            var y = MathF.Sin(rnd);

            return new Vector2(x * radius, y * radius);

        }

        public static Vector3 OnUnitSphere()
        {
            var m = Matrix.CreateFromYawPitchRoll(
                NextFloat(MathF.Tau),
                NextFloat(MathF.Tau),
                NextFloat(MathF.Tau)
            );
            return Vector3.Transform(Vector3.One, m);
        }

    }
}
