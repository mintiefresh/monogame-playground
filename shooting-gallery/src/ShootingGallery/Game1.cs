using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        Vector2 targetPosition = new Vector2(300,300);

        int score = 0;
        double timer = 10;


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
            _spriteBatch.DrawString(gameFont, $"Time : {timer}", new Vector2(3, 40), Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}