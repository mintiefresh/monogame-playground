using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Declare Variables
        Texture2D shipSprite;
        Texture2D asteroidSprite;
        Texture2D spaceSprite;
        SpriteFont gameFont;
        SpriteFont timerFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Set game resolution to 1280 x 720
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Assign variables to the assets
            shipSprite = Content.Load<Texture2D>("ship");
            asteroidSprite = Content.Load<Texture2D>("asteroid");
            spaceSprite = Content.Load<Texture2D>("space");
            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("timerFont");


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // Draw spacebackground
            _spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
            // Draw Spaceship
            _spriteBatch.Draw(shipSprite, new Vector2(200, 200), Color.White);
            // Draw Asteroid
            _spriteBatch.Draw(asteroidSprite, new Vector2(500, 500), Color.White);


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}