using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using System.Collections.Generic;

namespace SlashHimTheGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Map map;

        private FirstPlayer firstPlayer;
        private List<Hp> firstPlayerHp;

        private SecondPlayer secondPlayer;
        private List<Hp> secondPlayerHp;

        private Texture2D redWon;
        private Texture2D blueWon;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;


            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {

            firstPlayerHp = new List<Hp>
            {
                new Hp(new Vector2(320, 165)), 
                new Hp(new Vector2(385, 165)), 
                new Hp(new Vector2(450, 165))
            };

            secondPlayerHp = new List<Hp>
            {
                new Hp(new Vector2(1600, 165)),
                new Hp(new Vector2(1535, 165)),
                new Hp(new Vector2(1470, 165)),
            };

            firstPlayer = new FirstPlayer(firstPlayerHp);
            firstPlayer.Position = new Vector2(600, 828);

            secondPlayer = new SecondPlayer(secondPlayerHp);
            secondPlayer.Position = new Vector2(1000, 828);

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

            firstPlayer.Avatar = Content.Load<Texture2D>("avatar");
            firstPlayer.Sprite = new AnimatedSprite(Content.Load<SpriteSheet>("player1.sf", new JsonContentLoader()));
            firstPlayer.Sprite.Play("idler");

            secondPlayer.Avatar = Content.Load<Texture2D>("avatar2");
            secondPlayer.Sprite = new AnimatedSprite(Content.Load<SpriteSheet>("player2.sf", new JsonContentLoader()));
            secondPlayer.Sprite.Play("idlel");

            for (int i = 0; i < firstPlayerHp.Count; i++)
            {
                firstPlayerHp[i].Sprite = new AnimatedSprite(Content.Load<SpriteSheet>("hp.sf", new JsonContentLoader()));
                firstPlayerHp[i].Sprite.Play("idle");
            }

            for (int i = 0; i < secondPlayerHp.Count; i++)
            {
                secondPlayerHp[i].Sprite = new AnimatedSprite(Content.Load<SpriteSheet>("hp.sf", new JsonContentLoader()));
                secondPlayerHp[i].Sprite.Play("idle");
            }

            map.Background = Content.Load<Texture2D>("8Sky");
            map.Ground = Content.Load<Texture2D>("1Tiles");
            map.Forest = Content.Load<Texture2D>("Forest");
            map.BackBushes = Content.Load<Texture2D>("BackBushes");

            redWon = Content.Load<Texture2D>("redWon");
            blueWon = Content.Load<Texture2D>("blueWon");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            firstPlayer.Control(gameTime);
            firstPlayer.ChangeHpCondition(gameTime, firstPlayerHp, secondPlayer);

            secondPlayer.Control(gameTime);
            secondPlayer.ChangeHpCondition(gameTime, secondPlayerHp, firstPlayer);

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

            //отрисовка HP игроков
            for (int i = 0; i < firstPlayerHp.Count; i++)
            {
                _spriteBatch.Draw(firstPlayerHp[i].Sprite, firstPlayerHp[i].Position);
                firstPlayerHp[i].Initialize(gameTime);
            }

            for (int i = 0; i < secondPlayerHp.Count; i++)
            {
                _spriteBatch.Draw(secondPlayerHp[i].Sprite, secondPlayerHp[i].Position);
                secondPlayerHp[i].Initialize(gameTime);
            }

            // Отрисовка спрайтов игроков
            _spriteBatch.Draw(firstPlayer.Sprite, firstPlayer.Position);
            _spriteBatch.Draw(secondPlayer.Sprite, secondPlayer.Position);

            // Отрисовка аватаров игроков
            _spriteBatch.Draw(firstPlayer.Avatar, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(secondPlayer.Avatar, new Vector2(1600, 0), Color.White);

            if (firstPlayerHp.Count == 0)
            {
                _spriteBatch.Draw(blueWon, new Rectangle(0,0, 1920, 1080), Color.White);
            }

            if (secondPlayerHp.Count == 0)
            {
                _spriteBatch.Draw(redWon, new Rectangle(0, 0, 1920, 1080), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}