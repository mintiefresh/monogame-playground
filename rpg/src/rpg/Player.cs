using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rpg
{
    internal class Player
    {
        /****************
        *   VARIABLES   *
        *****************/
        private Vector2 position = new Vector2(500, 300);
        private int speed = 300;
        private Dir direction = Dir.Down;
        private bool isMoving = false;

        // Animated movement
        public SpriteAnimation anim;
        public SpriteAnimation[] animArray = new SpriteAnimation[4];


        /******************
        *  GETTER/SETTER  *
        *******************/
        public Vector2 Position
        {
            get { return position; }
        }
        public void setX(float newX) { position.X = newX; }
        public void setY(float newY) { position.Y = newY; }

        /****************
        *    METHODS    *
        *****************/
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Player will only move isn't moving (set to false every frame)
            isMoving = false;

            // Detect which direction player is moving
            // Player moves Right
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                direction = Dir.Right;
                isMoving = true;
            }
            // Player moves Left
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                direction = Dir.Left;
                isMoving = true;
            }
            // Player moves Down
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                direction = Dir.Down;
                isMoving = true;
            }
            // Player moves Up
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                direction = Dir.Up;
                isMoving = true;
            }
             
            // Move player in direction detected above
            if (isMoving)
            {
                switch (direction)
                {
                    case Dir.Right:
                        position.X += speed * dt;
                        break;
                    case Dir.Left:
                        position.X -= speed * dt;
                        break;
                    case Dir.Down:
                        position.Y += speed * dt;
                        break;
                    case Dir.Up:
                        position.Y -= speed * dt;
                        break;
                }
            }

            // Movement animation
            switch (direction)
            {
                case Dir.Down:
                    anim = animArray[0];
                    break;
                case Dir.Up:
                    anim = animArray[1];
                    break;
                case Dir.Left:
                    anim = animArray[2];
                    break;
                case Dir.Right:
                    anim = animArray[3];
                    break;
            }

            anim.Position = new Vector2(position.X - Game1.PLAYER_SPRITE_RADIUS, position.Y - Game1.PLAYER_SPRITE_RADIUS);

            if (isMoving) // Walking animation when player moves
            {
                anim.Update(gameTime);
            }
            else // Set animation to standstill when player is not moving
            {
                anim.setFrame(1);
            }
        } // end of Update()
    } // end of Player Class
}
