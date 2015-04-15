#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace MonoParticles
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        SpriteBatch _spriteBatch;

        private GraphicsDeviceManager graphics;

        private Texture2D _texture;

        private Model _model;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
          
            Content.RootDirectory = "Content";
        }
        
        private ParticleEngine3D _engine;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("boxtexture");
            _model = Content.Load<Model>("box");
            
            /*
            var v = new Vector2(
                GraphicsDevice.Viewport.Width/2f,
                GraphicsDevice.Viewport.Height/2f
            );
             */

            _engine = new ParticleEngine3D(_model, Vector3.Zero, _texture);
            //new ParticleEngineBasic(texture, v);
            //new ParticleEngineAdvanced(texture, v);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _engine.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.SteelBlue);

            _engine.Draw();


            base.Draw(gameTime);
        }
    }
}
