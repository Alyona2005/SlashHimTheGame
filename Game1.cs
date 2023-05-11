using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;

namespace SlashThemTheGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Hero hero = new Hero();

        private Map map = new Map();

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

            hero.HeroPosition = new Vector2(600, 835);

            map.GroundPosition = new Rectangle(0, 0, 1920, 1080);
            map.backgroundPosition = new Rectangle(0, 0, 1920, 1080);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            var heroSprite = new AnimatedSprite(Content.Load<SpriteSheet>("hero.sf", new JsonContentLoader()));

            heroSprite.Play("idler");
            
            hero.HeroSprite = heroSprite;

            map.Background = Content.Load<Texture2D>("8Sky");
            map.Ground = Content.Load<Texture2D>("1Tiles");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            hero.Move(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.Draw(map.Background, map.backgroundPosition, Color.White);

            _spriteBatch.Draw(map.Ground, map.GroundPosition, Color.White);

            _spriteBatch.Draw(hero.HeroSprite, hero.HeroPosition);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}