using System;

namespace MonoParticles
{
    public static class MathF
    {
        public static readonly float Pi = (float) Math.PI;

        public static readonly float Tau = (float) Math.PI*2;

        public static float Sin(float rad)
        {
            return (float) Math.Sin(rad);
        }

        public static float Cos(float rad)
        {
            return (float)Math.Cos(rad);
        }
    }
}
