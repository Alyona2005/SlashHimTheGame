using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;

namespace SlashItTheGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Map map;

        private Hero hero;
        private Hp[] heroHp;

        private Enemy enemy;
        private Hp[] enemyHp;

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
            heroHp = new Hp[]
            {
                new Hp(new Vector2(320, 165)), 
                new Hp(new Vector2(385, 165)), 
                new Hp(new Vector2(450, 165))
            };

            enemyHp = new Hp[]
            {
                new Hp(),
                new Hp(),
                new Hp(),
                new Hp(),
                new Hp()
            };

            hero = new Hero(heroHp);
            hero.HeroPosition = new Vector2(600, 828);

            enemy = new Enemy(enemyHp);
            enemy.EnemyPosition = new Vector2(1000, 830);

            map = new Map();
            map.BackgroundPosition = new Rectangle(0, 0, 1920, 1080);
            map.GroundPosition = new Rectangle(0, 0, 1920, 1080);
            map.ForestPosition = new Rectangle(0, 0, 1920, 1080);
            map.BackBushesPosition = new Rectangle(0, 0, 1920, 1080);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            hero.Avatar = Content.Load<Texture2D>("avatar");
            hero.HeroSprite = new AnimatedSprite(Content.Load<SpriteSheet>("hero.sf", new JsonContentLoader()));
            hero.HeroSprite.Play("idler");

            enemy.EnemySprite = new AnimatedSprite(Content.Load<SpriteSheet>("enemy.sf", new JsonContentLoader()));
            enemy.EnemySprite.Play("idle");

            for (int i = 0; i < heroHp.Length; i++)
            {
                heroHp[i].HpSprite = new AnimatedSprite(Content.Load<SpriteSheet>("hp.sf", new JsonContentLoader()));
                heroHp[i].HpSprite.Play("idle");
            }

            map.Background = Content.Load<Texture2D>("8Sky");
            map.Ground = Content.Load<Texture2D>("1Tiles");
            map.Forest = Content.Load<Texture2D>("Forest");
            map.BackBushes = Content.Load<Texture2D>("BackBushes");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            hero.ToControl(gameTime);
            hero.ChangeHpCondition(gameTime, heroHp, enemy);

            enemy.Roam(gameTime);
            enemy.ChangeHpCondition(gameTime, enemyHp, hero);

            base.Update(gameTime);
        }

        // Отрисовка кадра
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Отрисовка карты
            _spriteBatch.Draw(map.Background, map.BackgroundPosition, Color.White);
            _spriteBatch.Draw(map.Forest, map.ForestPosition, Color.White);
            _spriteBatch.Draw(map.BackBushes, map.BackBushesPosition, Color.White);
            _spriteBatch.Draw(map.Ground, map.GroundPosition, Color.White);

            for (int i = 0; i < heroHp.Length; i++)
            {
                _spriteBatch.Draw(heroHp[i].HpSprite, heroHp[i].HpPosition);
            }

            // Отрисовка врага
            _spriteBatch.Draw(enemy.EnemySprite, enemy.EnemyPosition);

            // Отрисовка спрайта игрока
            _spriteBatch.Draw(hero.HeroSprite, hero.HeroPosition);

            // Отрисовка аватара персонажа
            _spriteBatch.Draw(hero.Avatar, new Vector2(0, 0), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}