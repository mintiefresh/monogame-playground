using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace rpg
{
    internal class Enemy
    {
        // List of enemies
        public static List<Enemy> enemies = new List<Enemy>();

        // Enemy properties
        private Vector2 position = new Vector2(0, 0);
        private int speed = 150;
        public SpriteAnimation anim;
        public int radius = 30;
        private bool dead = false;

        public Enemy(Vector2 newPos, Texture2D spriteSheet)
        {
            position = newPos;
            anim = new SpriteAnimation(spriteSheet, 10, 6);
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            anim.Position = new Vector2(position.X - Game1.ENEMY_SPRITE_WIDTH, position.Y - Game1.ENEMY_SPRITE_HEIGHT);
            anim.Update(gameTime);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Create a vector that points from enemy to player
            Vector2 moveDir = playerPos - position;
            moveDir.Normalize();        // Normalizing keeps vector length to be 1
            // Update enemy position with new vector
            position += moveDir * speed * dt;
        }
    }
}
