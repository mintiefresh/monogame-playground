﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceShip
{
    public static class MySounds
    {
        public static Song bgMusic;
    }

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

        // Initialize objects
        Ship playerShip = new Ship();
        Controller gameController = new Controller();

        bool musicPlaying = false;

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
            MySounds.bgMusic = Content.Load<Song>("Sounds/departure");
            //MediaPlayer.Play(MySounds.bgMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Play music for level only
            if (gameController.inGame && !musicPlaying)
            {
                MediaPlayer.Play(MySounds.bgMusic);
                musicPlaying = true;
            }
            if (!gameController.inGame && musicPlaying)
            {
                MediaPlayer.Stop();
                musicPlaying = false;
            }
            // Ship cannot move while in main menu
            if (gameController.inGame)playerShip.shipUpdate(gameTime);
            gameController.controllerUpdate(gameTime);

            // Go through asteroid list and run asteroidUpdate.
            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                gameController.asteroids[i].asteroidUpdate(gameTime);

                // Check for collision between playerShip and asteroid
                int sum = gameController.asteroids[i].radius + playerShip.radius;
                if (Vector2.Distance(gameController.asteroids[i].position, playerShip.position) < sum)
                {
                    gameController.inGame = false;
                    playerShip.position = Ship.defaultPosition;
                    gameController.asteroids.Clear();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // Draw spacebackground
            _spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
            // Draw Spaceship
            _spriteBatch.Draw(shipSprite, new Vector2(playerShip.position.X - 34, playerShip.position.Y - 50), Color.White);
            // Draw every asteroid from its list.
            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                _spriteBatch.Draw(asteroidSprite, new Vector2(gameController.asteroids[i].position.X - gameController.asteroids[i].radius,
                    gameController.asteroids[i].position.Y - gameController.asteroids[i].radius),
                    Color.White);
            }

            // Main Menu
            if (gameController.inGame == false)
            {
                string menuMessage = "Press <Enter> to begin!";
                // Center the message
                Vector2 sizeOfText = gameFont.MeasureString(menuMessage);
                int halfWidth = _graphics.PreferredBackBufferWidth / 2;
                int halfHeight = _graphics.PreferredBackBufferHeight / 3;

                // Draw the menu
                _spriteBatch.DrawString(gameFont, menuMessage, new Vector2(halfWidth - sizeOfText.X / 2, halfHeight), Color.White);
            }

            // Draw timer scoreboard at top left of screen
            _spriteBatch.DrawString(timerFont, $"Time: {Math.Floor(gameController.totalTime).ToString()}", new Vector2(3, 3), Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}