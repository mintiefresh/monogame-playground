using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Comora;
using System;

namespace rpg
{
    enum Dir
    {
        Down,
        Up,
        Left,
        Right
    }

    public static class MySounds
    {
        public static SoundEffect projectileSound;
        public static SoundEffect playerDeath;
        public static SoundEffect enemyHit;
        public static Song bgMusic;
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        /****************
        *   CONSTANTS   *
        *****************/
        public const int RESOLUTION_WIDTH = 1280;
        public const int RESOLUTION_HEIGHT = 720;
        public const int PLAYER_SPRITE_RADIUS = 48;
        public const int ENEMY_SPRITE_WIDTH = 48;
        public const int ENEMY_SPRITE_HEIGHT = 66;
        public const int TOP_BORDER = 200;
        public const int RIGHT_BORDER = 1275;
        public const int BOTTOM_BORDER = 1250;
        public const int LEFT_BORDER = 225;


        /****************
        *   VARIABLES   *
        *****************/
        Texture2D playerSprite;
        Texture2D walkDown;
        Texture2D walkUp;
        Texture2D walkRight;
        Texture2D walkLeft;

        Texture2D background;
        Texture2D ball;
        Texture2D skull;

        bool deathSound = false;
        SpriteFont gameFont;

        double timer = 0;
        int score = 0;

        /****************
        *    OBJECTS    *
        *****************/
        Player player = new Player();
        Camera camera; // from Comora package

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Set resolution of game
            _graphics.PreferredBackBufferWidth = RESOLUTION_WIDTH;
            _graphics.PreferredBackBufferHeight = RESOLUTION_HEIGHT;
            _graphics.ApplyChanges();

            this.camera = new Camera(_graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Content
            playerSprite = Content.Load<Texture2D>("Player/player");
            walkDown = Content.Load<Texture2D>("Player/walkDown");
            walkUp = Content.Load<Texture2D>("Player/walkUp");
            walkRight = Content.Load<Texture2D>("Player/walkRight");
            walkLeft = Content.Load<Texture2D>("Player/walkLeft");

            background = Content.Load<Texture2D>("background");
            ball = Content.Load<Texture2D>("ball");
            skull = Content.Load<Texture2D>("skull");
            gameFont = Content.Load<SpriteFont>("gameFont");

            // Load player animation
            player.animArray[0] = new SpriteAnimation(walkDown, 4, 8);
            player.animArray[1] = new SpriteAnimation(walkUp, 4, 8);
            player.animArray[2] = new SpriteAnimation(walkLeft, 4, 8);
            player.animArray[3] = new SpriteAnimation(walkRight, 4, 8);
            player.anim = player.animArray[0];

            // Load sounds
            MySounds.projectileSound = Content.Load<SoundEffect>("Sounds/handcannon");
            MySounds.playerDeath = Content.Load<SoundEffect>("Sounds/death");
            MySounds.enemyHit = Content.Load<SoundEffect>("Sounds/enemy_hit");
            MySounds.bgMusic = Content.Load<Song>("Sounds/megaman");
            MediaPlayer.Play(MySounds.bgMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Sounds for dead only
            if (player.Dead && !deathSound)
            {
                deathSound = true;
                MediaPlayer.Stop();
                MySounds.playerDeath.Play();
            }

            // Player
            player.Update(gameTime);
            this.camera.Position = player.Position;
            this.camera.Update(gameTime);

            // Controller to spawn enemies on a timer
            if (!player.Dead) Controller.Update(gameTime, skull);

            // Projectiles
            foreach (Projectile proj in Projectile.projectiles)
            {
                proj.Update(gameTime);
            }

            // Enemies
            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.Update(gameTime, player.Position, player.Dead);
                // Check for enemy/player collision
                int sum = enemy.Radius + player.Radius;
                if (Vector2.Distance(player.Position, enemy.Position) < sum)
                {
                    player.Dead = true;
                }
            }

            // Check for collisions between projectiles and enemies
            foreach (Projectile proj in Projectile.projectiles)
            {
                foreach (Enemy enemy in Enemy.enemies)
                {
                    // Distance between radii of enemy and projectile
                    int sum = proj.radius + enemy.Radius;
                    // If distance between projectile and radius is less than sum, there was collision
                    if (Vector2.Distance(proj.Position, enemy.Position) < sum)
                    {
                        // Set 'collided/dead' flags to true
                        proj.Collided = true;
                        enemy.Dead = true;
                        MySounds.enemyHit.Play();
                        score++;
                    }
                }
            }
            // Remove every projectile/enemy whose collided/dead flag is true.
            Projectile.projectiles.RemoveAll(p => p.Collided);
            Enemy.enemies.RemoveAll(e => e.Dead);

            timer += gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(this.camera); // Get camera updated onto player
            // Draw background in upper left
            _spriteBatch.Draw(background, new Vector2(-500, -500), Color.White);

            // Draw Enemies
            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.anim.Draw(_spriteBatch);
            }

            // Draw projectile
            foreach (Projectile proj in Projectile.projectiles)
            {
                _spriteBatch.Draw(ball, new Vector2(proj.Position.X - PLAYER_SPRITE_RADIUS, proj.Position.Y - PLAYER_SPRITE_RADIUS), Color.White);
            }

            // Draw player and player animation
            if (!player.Dead) player.anim.Draw(_spriteBatch);

            if (player.Dead)
            {
                _spriteBatch.DrawString(gameFont, "GAME OVER", new Vector2(player.Position.X - PLAYER_SPRITE_RADIUS, player.Position.Y-PLAYER_SPRITE_RADIUS), Color.White);
            }

            // Draw score
            _spriteBatch.DrawString(gameFont, $"Score: {score}", new Vector2(player.Position.X - 550, player.Position.Y - 350), Color.White);

            // Draw timer
            _spriteBatch.DrawString(gameFont, $"Time : {Math.Ceiling(timer)}", new Vector2(player.Position.X - 550, player.Position.Y - 300), Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}