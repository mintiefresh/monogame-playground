using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora; 

namespace rpg
{
    enum Dir
    {
        Down,
        Up,
        Left,
        Right
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

            // Load player animation
            player.animArray[0] = new SpriteAnimation(walkDown, 4, 8);
            player.animArray[1] = new SpriteAnimation(walkUp, 4, 8);
            player.animArray[2] = new SpriteAnimation(walkLeft, 4, 8);
            player.animArray[3] = new SpriteAnimation(walkRight, 4, 8);
            player.anim = player.animArray[0];

            // Enemies
            Enemy.enemies.Add(new Enemy(new Vector2(100, 100), skull));
            Enemy.enemies.Add(new Enemy(new Vector2(700, 200), skull));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            // Set Camera to player
            this.camera.Position = player.Position;
            this.camera.Update(gameTime);

            // Projectiles
            foreach (Projectile proj in Projectile.projectiles)
            {
                proj.Update(gameTime);
            }

            // Enemies
            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.Update(gameTime, player.Position);
            }

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
            player.anim.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}