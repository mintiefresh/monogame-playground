using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootingGallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Store assets in variables
        Texture2D targetSprite;
        Texture2D crosshairSprite;
        Texture2D backgroundSprite;
        SpriteFont gameFont;

        // Target sprite information
        Vector2 targetPosition = new Vector2(300,300);
        const int targetRadius = 45;

        // Scoreboard
        int score = 0;
        double timer = 10;

        // Mouse
        MouseState mState;
        bool mRelease = true;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            targetSprite = Content.Load<Texture2D>("target");
            crosshairSprite = Content.Load<Texture2D>("crosshairs");
            backgroundSprite = Content.Load<Texture2D>("sky");
            gameFont = Content.Load<SpriteFont>("galleryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update timer
            if (timer > 0) timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (timer < 0) timer = 0;

            // Check on mouse state and test if user clicked && released left mouse button
            mState = Mouse.GetState();
            if (mState.LeftButton == ButtonState.Pressed && mRelease == true)
            {
                // Get distance betwen mouse and target
                float mouseTargetDist = Vector2.Distance(targetPosition, mState.Position.ToVector2());
                // User scores if clicked inside the target radius and timer is still going
                if (mouseTargetDist < targetRadius && timer > 0)
                {
                    score++;
                }
                mRelease = false;
            }

            // set mRelease to true if the 'button state' is released
            if (mState.LeftButton == ButtonState.Released) mRelease = true;
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            // display background and target
            _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(targetSprite, targetPosition, Color.White);
                
            // display score
            _spriteBatch.DrawString(gameFont, $"Score: {score}", new Vector2(3, 3), Color.White);
            // display timer
            _spriteBatch.DrawString(gameFont, $"Time : {Math.Ceiling(timer)}", new Vector2(3, 40), Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}