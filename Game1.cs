using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Media;

namespace SlashThemTheGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private AnimatedSprite _heroSprite;

        private Vector2 _heroPosition;

        private Texture2D _background;

        private Texture2D _ground;

        private Rectangle _groundPosition = new Rectangle(0, 0, 1920, 1080);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            //_graphics.IsFullScreen = true;


            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _background = Content.Load<Texture2D>("8Sky");

            _ground = Content.Load<Texture2D>("1Tiles");

            //var spriteSheet = 

            var sprite = new AnimatedSprite(Content.Load<SpriteSheet>("hero.sf", new JsonContentLoader()));

            sprite.Play("idler");
            _heroPosition = new Vector2(600, 841);
            _heroSprite = sprite;
        }

        protected override void Update(GameTime gameTime)
        { 
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var walkSpeed = deltaSeconds * 128;
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            var animation = "idler";

            if ((keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up)) && animation == "idler")
            {
                animation = "jumpr";
                _heroPosition.Y -= 500 * dt;
            }

            if ((keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up)) && animation == "idlel")
            {
                animation = "jumpl";
                _heroPosition.Y -= 500 * dt;
            }

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                animation = "runl";
                _heroPosition.X -= walkSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                animation = "runr";
                _heroPosition.X += walkSpeed;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && animation == "idler")
            {
                animation = "attackr";
            }

            if (mouseState.LeftButton == ButtonState.Pressed && animation == "idlel")
            {
                animation = "attackl";
            }

            _heroSprite.Play(animation);

            _heroSprite.Update(deltaSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.Draw(_background, new Rectangle(0, 0, 1920, 1080), Color.White);

            _spriteBatch.Draw(_ground, _groundPosition, Color.White);

            _spriteBatch.Draw(_heroSprite, _heroPosition);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}