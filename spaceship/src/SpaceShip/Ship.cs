using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{
    internal class Ship
    {
        // Default position for spaceship (set to middle of screen)
        public static Vector2 defaultPosition = new Vector2(640, 360);
        public Vector2 position = defaultPosition;
        // Sprite is 68px wide by 100px tall
        public int radius = 30;     // slightly smaller than actual radius of sprite
        public int speed = 180;

        /// <summary>
        /// Method runs every frame and updates the status of the ship object
        /// </summary>
        /// <param name="gameTime"></param>
        public void shipUpdate(GameTime gameTime)
        {
            // Add movement for ship
            KeyboardState keyboardState = Keyboard.GetState();
            float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            if ((keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) && position.X < 1280) 
                position.X += speed * dt;
            if ((keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) && position.X > 0) 
                position.X -= speed * dt;
            if ((keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) && position.Y < 720) 
                position.Y += speed * dt;
            if ((keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) && position.Y > 0) 
                position.Y -= speed * dt;
        }
    }
}
