using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{
    internal class Controller
    {
        public List<Asteroid> asteroids = new List<Asteroid>();
        public double timer = 2;
        public double maxTime = 2;
        public int nextSpeed = 240;
        public double totalTime = 0;

        public bool inGame = false;     // for main menu


        /// <summary>
        /// Method runs every frame and updates the controller object.
        /// </summary>
        /// <param name="gameTime"></param>
        public void controllerUpdate(GameTime gameTime)
        {
            if (inGame)
            {
                // Count the timer down to spawn asteroids
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                // Keep track of time elapsed in game;
                totalTime += gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // Only start game if user presses enter
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    inGame = true;
                    // reset all timer/speeds
                    totalTime = 0;
                    timer = 2;
                    maxTime = 2;
                    nextSpeed = 240;
                }
            }

            // Create asteroids
            if (timer <= 0)
            {
                // Create an asteroid every 2 seconds (or less)
                asteroids.Add(new Asteroid(nextSpeed));
                timer = maxTime;

                // Increase asteroid spawn and speed as game progresses
                if (maxTime > 0.5) maxTime -= 0.1;
                if (nextSpeed < 720) nextSpeed += 4;
            }
        }
    }
}
