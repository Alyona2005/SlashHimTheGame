﻿using Microsoft.Xna.Framework;
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

        private static  Hp[] heroHp = { new Hp(new Vector2(50, 60)), new Hp(new Vector2(120, 60)), new Hp(new Vector2(190, 60)) };
        private Hero hero = new Hero(heroHp);

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

            hero.HeroPosition = new Vector2(600, 830);

            map.BackgroundPosition = new Rectangle(0, 0, 1920, 1080);
            map.GroundPosition = new Rectangle(0, 0, 1920, 1080);
            map.ForestPosition = new Rectangle(0, 0, 1920, 1080);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            hero.HeroSprite = new AnimatedSprite(Content.Load<SpriteSheet>("hero.sf", new JsonContentLoader()));
            hero.HeroSprite.Play("idler");

            /**heroHp[0].HpSprite = new AnimatedSprite(Content.Load<SpriteSheet>("hp.sf", new JsonContentLoader()));
            heroHp[1].HpSprite = new AnimatedSprite(Content.Load<SpriteSheet>("hp.sf", new JsonContentLoader()));
            heroHp[2].HpSprite = new AnimatedSprite(Content.Load<SpriteSheet>("hp.sf", new JsonContentLoader()));**/

            for (int i = 0; i < heroHp.Length; i++)
            {
                heroHp[i].HpSprite = new AnimatedSprite(Content.Load<SpriteSheet>("hp.sf", new JsonContentLoader()));
                heroHp[i].HpSprite.Play("idle");
            }

            map.Background = Content.Load<Texture2D>("8Sky");
            map.Ground = Content.Load<Texture2D>("1Tiles");
            map.Forest = Content.Load<Texture2D>("Forest");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            hero.ToControl(gameTime);

            for (int i = 0; i < heroHp.Length; i++)
            {
                heroHp[i].ChangeStatus(gameTime, hero);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //Отрисовка карты
            _spriteBatch.Draw(map.Background, map.BackgroundPosition, Color.White);
            _spriteBatch.Draw(map.Forest, map.ForestPosition, Color.White);
            _spriteBatch.Draw(map.Ground, map.GroundPosition, Color.White);

            for (int i = 0; i < heroHp.Length; i++)
            {
                _spriteBatch.Draw(heroHp[i].HpSprite, heroHp[i].HpPosition);
            }

            //Отрисовка спрайта игрока
            _spriteBatch.Draw(hero.HeroSprite, hero.HeroPosition);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}