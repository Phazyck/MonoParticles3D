#region File Description
//-----------------------------------------------------------------------------
// Game1.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoParticles
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game2 : Game
    {
        public static Matrix view;
        public static Matrix proj;

        private SpriteFont font;
        private SpriteBatch spriteBatch;

        private Vector3 cameraLookAt;
        
        Model box;
        Texture2D boxTexture;

        // Set the avatar position and rotation variables.



        private float avatarDistance;
        private float avatarElevation;
        private float avatarYaw;

        private float _avatarPitch;

        float avatarPitch
        {
            get { return _avatarPitch; }
            set
            {

                if (Math.Abs(value) <= MathHelper.PiOver2)
                {
                    _avatarPitch = value;
                }


            }
        }

        float _speed = 3f;

        // Set field of view of the camera in radians (pi/4 is 45 degrees).
        static float viewAngle = MathHelper.PiOver4;

        // Set distance from the camera of the near and far clipping planes.
        static float nearClip = 1.0f;
        static float farClip = 2000.0f;

        GraphicsDeviceManager graphics;

        private ParticleEngine3D engine;

        private bool paused = false;
        private bool stepping = false;

        public Game2()
        {
            
            int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
            int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferHeight = h,
                PreferredBackBufferWidth = w
            };

            var x = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - w;
            var y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - h;
            
            x /= 4;
            y /= 8;

            Window.SetPosition(new Point(x, y));

            ResetCamera();
            
            Content.RootDirectory = "Content";
        }

        private void ResetCamera()
        {
            avatarDistance = 144;
            avatarElevation = 0f;
            avatarYaw = 0;
            avatarPitch = 0;
            cameraLookAt = Vector3.Zero;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            engine = new ParticleEngine3D(
                Content.Load<Model>("Models/Cube"), 
                Vector3.Up * 48,
                //Vector3.Zero    
                null
                //Content.Load<Texture2D>("Textures/Brick")
            );

            font = Content.Load<SpriteFont>("Fonts/Debug");
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        private KeyboardState oldState = Keyboard.GetState();
        private KeyboardState newState = Keyboard.GetState();

        private bool KeyPressed(Keys key)
        {
            return newState.IsKeyDown(key) && !oldState.IsKeyDown(key);

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit

            oldState = newState;

            newState = Keyboard.GetState();


            if (newState.IsKeyDown(Keys.Escape))
                Exit();

            if (KeyPressed(Keys.P))
            {
                paused = !paused;
            }

            if (paused)
            {
                if (!KeyPressed(Keys.Right))
                    return;
            }

            UpdateAvatarPosition();
            UpdateCamera();
            engine.Update();



            base.Update(gameTime);
        }

        /// <summary>
        /// Updates the position and direction of the avatar.
        /// </summary>
        void UpdateAvatarPosition()
        {
            var keyboardState = Keyboard.GetState();


            var moveSpeed = _speed;

            if (keyboardState.IsKeyDown(Keys.LeftShift))
                moveSpeed *= 2;

            float speed = moveSpeed/60f;

            if (keyboardState.IsKeyDown(Keys.W))
            {
                avatarPitch += speed;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                avatarPitch -= speed;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                avatarYaw += speed;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                avatarYaw -= speed;
            }

            if (keyboardState.IsKeyDown(Keys.I))
            {
                cameraLookAt.Y += moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.K))
            {
                cameraLookAt.Y -= moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.J))
            {
                cameraLookAt.X += moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.L))
            {
                cameraLookAt.X -= moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.U))
            {
                cameraLookAt.Z += moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.O))
            {
                cameraLookAt.Z -= moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                avatarDistance += moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                avatarDistance -= moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                ResetCamera();
            }

            if (keyboardState.IsKeyDown(Keys.Z))
            {
                avatarYaw = MathHelper.PiOver2 * 0;
            }

            if (keyboardState.IsKeyDown(Keys.X))
            {
                avatarYaw = MathHelper.PiOver2 * 1;
            }

            if (keyboardState.IsKeyDown(Keys.C))
            {
                avatarYaw = MathHelper.PiOver2 * 2;
            }

            if (keyboardState.IsKeyDown(Keys.V))
            {
                avatarYaw = MathHelper.PiOver2 * 3;
            }

            if (keyboardState.IsKeyDown(Keys.R))
            {
                Initialize();
            }


        }

        /// <summary>
        /// Updates the position and direction of the camera relative to the avatar.
        /// </summary>
        void UpdateCamera()
        {

            // Set up the view matrix and projection matrix.


            var cameraPosition = Vector3.Backward*avatarDistance;
            cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateFromYawPitchRoll(
                avatarYaw, avatarPitch, 0f));
            cameraPosition += cameraLookAt;



            view = Matrix.CreateLookAt(cameraPosition, cameraLookAt, Vector3.Up);

            proj = Matrix.CreatePerspectiveFieldOfView(viewAngle, graphics.GraphicsDevice.Viewport.AspectRatio,
                                                          nearClip, farClip);
        }

        /// <summary>
        /// Draws the box model; a reference point for the avatar.
        /// </summary>
        void DrawModel(Model model, Matrix world, Texture2D texture, Transform transform)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (var be in mesh.Effects.Cast<BasicEffect>())
                {
                    be.Projection = proj;
                    be.View = transform.GetMatrix() * view;
                    be.World = world;
                    be.Texture = texture;
                    be.TextureEnabled = true;
                    
                }
                mesh.Draw();
            }
        }

        private float boxRotation;

        private Transform _transform = new Transform();

        private void DrawDebugInfo() {

            spriteBatch.Begin();

            var s = "Particles: " + engine.ParticleCount.ToString();
            var pos = new Vector2(32, 32);


            spriteBatch.DrawString(font, s, pos, Color.White);
            
            spriteBatch.End();
            

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            /*
            float deltaTime = gameTime.ElapsedGameTime.Milliseconds;
            deltaTime /= 1000f;

            var totalTime = (float) gameTime.TotalGameTime.TotalSeconds;
             
            _transform.Yaw += deltaTime;
            _transform.Pitch += deltaTime;
            //_transform.Roll += deltaTime;


            var pos = _transform.LocalPosition;
            pos.Y = 5f * (float) Math.Sin(totalTime);

            _transform.LocalPosition = pos;

            var scl = _transform.LocalScale;

            scl.X = scl.Z = ((float)Math.Sin(totalTime) + 1.5f);
            scl.Y = ((float)Math.Cos(totalTime) + 1.5f);


            _transform.LocalScale =
                scl * 4f;
                

            DrawModel(box, Matrix.Identity, boxTexture, _transform);
            */
            engine.Draw();

            DrawDebugInfo();
            
            base.Draw(gameTime);
        }
    }
}
