﻿using System;
using Microsoft.Xna.Framework;

namespace MonoParticles
{
    public static class GameWindowExtensions
    {
        public static void SetPosition(this GameWindow window, Point position)
        {
            OpenTK.GameWindow otkWindow = GetForm(window);
            if (otkWindow == null) return;

            otkWindow.X = position.X;
            otkWindow.Y = position.Y;
        }

        public static OpenTK.GameWindow GetForm(this GameWindow gameWindow)
        {
            Type type = typeof(OpenTKGameWindow);
            System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
                return field.GetValue(gameWindow) as OpenTK.GameWindow;
            return null;
        }
    }
}