using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using System.Collections.Generic;

namespace SlashItTheGame
{
    public class Hero : IHp
    {
        public Hero(List<Hp> amountOfHp)
        {
            _hp = amountOfHp;
            _hpCount = (byte)amountOfHp.Count;
        }

        private byte _hpCount;
        public byte HpCount { get { return _hpCount; } set { _hpCount = value; } }

        public Texture2D Avatar { get; set; }

        private List<Hp> _hp;
        public List<Hp> Hp { get { return _hp; } set { _hp = value; } }

        private Vector2 _heroPosition;
        public Vector2 HeroPosition { get { return _heroPosition; } set { _heroPosition = value; } }

        private AnimatedSprite _heroSprite;
        public AnimatedSprite HeroSprite { get { return _heroSprite; } set { _heroSprite = value; } }

        public string CurrentAnimation { get; private set; }

        private bool leftCheck = false;
        private byte checkAction = 0;
        private bool checkDamage = false;
        private byte index = 2;

        // Логика управления игроком
        public void Control(GameTime gameTime)
        {
            CurrentAnimation = "idler";

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var walkSpeed = deltaSeconds * 600;

            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.A))
            {
                CurrentAnimation = "runl";
                _heroPosition.X -= walkSpeed;

                leftCheck = true;
                checkAction = 2;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                CurrentAnimation = "runr";
                _heroPosition.X += walkSpeed;

                leftCheck = false;
            }

            if (keyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyDown(Keys.W))
            {
                CurrentAnimation = "jumpl";
                _heroPosition.X -= walkSpeed;

                leftCheck = true;

                checkAction = 5;
            }

            if (keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyDown(Keys.W))
            {
                CurrentAnimation = "jumpr";
                _heroPosition.X += walkSpeed;

                leftCheck = false;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && leftCheck == false)
            {
                CurrentAnimation = "attackr";

                leftCheck = false;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && keyboardState.IsKeyDown(Keys.D))
            {
                CurrentAnimation = "runr";
            }

            /**if (mouseState.LeftButton == ButtonState.Pressed && leftCheck == true)
            {
                animation = "attackl";

                leftCheck = true;

                checkAction = 3;
            }**/

            if (_heroPosition.Y > 600)
            {
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (leftCheck == true)
                    {
                        CurrentAnimation = "jumpl";

                        _heroPosition.Y -= 1000 * dt;

                        leftCheck = true;

                        checkAction = 1;
                    }

                    else
                    {
                        CurrentAnimation = "jumpr";
                        _heroPosition.Y -= 1000 * dt;

                        leftCheck = false;
                    }
                }
            }

            if (_heroPosition.Y < 828 && keyboardState.IsKeyUp(Keys.W))
            {
                if (leftCheck == true)
                {
                    CurrentAnimation = "falll";
                    _heroPosition.Y += 860 * dt;

                    checkAction = 4;
                }

                else
                {
                    CurrentAnimation = "fallr";
                    _heroPosition.Y += 860 * dt;
                }
            }

            if (leftCheck)
            {
                switch (checkAction)
                {
                    case 0:

                        CurrentAnimation = "idlel";
                        break;

                    case 1:

                        CurrentAnimation = "jumpl";
                        checkAction = 0;

                        break;

                    case 2:

                        CurrentAnimation = "runl";
                        checkAction = 0;

                        break;

                   /** case 3:

                        animation = "attackl";
                        checkAction = 0;

                        break;**/

                    case 4:

                        CurrentAnimation = "falll";
                        checkAction = 0;

                        break;

                    case 5:

                        CurrentAnimation = "jumpl";
                        checkAction = 0;

                        break;
                }
            }

            _heroSprite.Play(CurrentAnimation);
            _heroSprite.Update(deltaSeconds);
        }

        // Логика изменения HP игрока
        public void ChangeHpCondition(GameTime gameTime, List<Hp> hp, object enemy)
        {
            Enemy e = (Enemy)enemy;

            if (HeroPosition.X - e.EnemyPosition.X < 40 && HeroPosition.X - e.EnemyPosition.X > -100 && HeroPosition.Y > 800 && e.CurrentAnimation == "attack")
            {
                checkDamage = true;

                if (sw.ElapsedMilliseconds%700 == 0)
                {
                    for (int i = index; i < hp.Count;)
                    {
                        hp[index].Initialize(gameTime, checkDamage);

                        hp.Remove(hp[index]);

                        break;
                    }

                    _hpCount--;

                    index--;
                }              
            }

            else
            {
                for (int i = 0; i < hp.Count; i++)
                {
                    hp[i].Initialize(gameTime, checkDamage);
                }
            }

            checkDamage = false;
        }
    }
}
