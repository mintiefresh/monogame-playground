using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{
    internal class Asteroid
    {
        public Vector2 position = new Vector2(600, 300);
        public int speed = 200;
        public int radius = 59;     // asteroid sprite is 118px by 118px

        // Constructor
        // Create asteroid with a set speed and increase as game progresses
        public Asteroid(int newSpeed)
        {
            speed = newSpeed;
            Random rand = new Random();
            position = new Vector2(1380, rand.Next(0, 721));    // asteroid will spawn off screen
        }

        /// <summary>
        /// Method runs every frame and updates the asteroid object.
        /// </summary>
        /// <param name="gameTime"></param>        
        public void asteroidUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt;
        }
    }
}
