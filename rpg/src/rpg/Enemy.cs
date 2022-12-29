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

        public Enemy(Vector2 newPos, Texture2D spriteSheet)
        {
            position = newPos;
            anim = new SpriteAnimation(spriteSheet, 10, 6);
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public void Update(GameTime gameTime)
        {
            anim.Position = new Vector2(position.X - Game1.ENEMY_SPRITE_WIDTH, position.Y - Game1.ENEMY_SPRITE_HEIGHT);
            anim.Update(gameTime);
        }
    }
}
