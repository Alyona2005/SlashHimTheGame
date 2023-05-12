using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;

namespace SlashThemTheGame
{
    public class Hero
    {
        public Hero(Hp[] amountOfHp)
        {
            _hp = amountOfHp;
        }

        private Hp[] _hp;

        public Hp[] Hp { get { return _hp; } private set { _hp = value; } }

        private Vector2 _heroPosition;
        public Vector2 HeroPosition { get { return _heroPosition; } set { _heroPosition = value; } }

        private AnimatedSprite _heroSprite;
        public AnimatedSprite HeroSprite { get { return _heroSprite; } set { _heroSprite = value; } }

        private bool leftCheck = false;
        private byte checkAction = 0;

        //Логика управления игроком
        public void ToControl(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var walkSpeed = deltaSeconds * 600;

            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            var animation = "idler";

            if (keyboardState.IsKeyDown(Keys.A))
            {
                animation = "runl";
                _heroPosition.X -= walkSpeed;

                leftCheck = true;
                checkAction = 2;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                animation = "runr";
                _heroPosition.X += walkSpeed;

                leftCheck = false;
            }

            if (keyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyDown(Keys.W))
            {
                animation = "jumpl";
                _heroPosition.X -= walkSpeed;

                leftCheck = true;

                checkAction = 5;
            }

            if (keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyDown(Keys.W))
            {
                animation = "jumpr";
                _heroPosition.X += walkSpeed;

                leftCheck = false;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && leftCheck == false)
            {
                animation = "attackr";

                leftCheck = false;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && leftCheck == true)
            {
                animation = "attackl";

                leftCheck = true;

                checkAction = 3;
            }

            if (_heroPosition.Y > 600)
            {
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (leftCheck == true)
                    {
                        animation = "jumpl";

                        _heroPosition.Y -= 1000 * dt;

                        leftCheck = true;

                        checkAction = 1;
                    }

                    else
                    {
                        animation = "jumpr";
                        _heroPosition.Y -= 1000 * dt;

                        leftCheck = false;
                    }
                }
            }

            if (_heroPosition.Y < 830 && keyboardState.IsKeyUp(Keys.W))
            {
                if (leftCheck == true)
                {
                    animation = "falll";
                    _heroPosition.Y += 860 * dt;

                    checkAction = 4;
                }

                else
                {
                    animation = "fallr";
                    _heroPosition.Y += 860 * dt;
                }
            }

            if (leftCheck)
            {
                switch (checkAction)
                {
                    case 0:

                        animation = "idlel";
                        break;

                    case 1:

                        animation = "jumpl";
                        checkAction = 0;

                        break;

                    case 2:

                        animation = "runl";
                        checkAction = 0;

                        break;

                    case 3:

                        animation = "attackl";
                        checkAction = 0;

                        break;

                    case 4:

                        animation = "falll";
                        checkAction = 0;

                        break;

                    case 5:

                        animation = "jumpl";
                        checkAction = 0;

                        break;
                }
            }

            _heroSprite.Play(animation);

            _heroSprite.Update(deltaSeconds);
        }
    }
}
