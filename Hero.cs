using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;

namespace SlashThemTheGame
{
    public class Hero
    {
        private Vector2 _heroPosition;
        public Vector2 HeroPosition { get { return _heroPosition; } set { _heroPosition = value; } }
        private AnimatedSprite _heroSprite;
        public AnimatedSprite HeroSprite { get { return _heroSprite; } set { _heroSprite = value; } }

        private bool leftCheck = false;
        private byte checkMove = 0;
        public void Move(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var walkSpeed = deltaSeconds * 600;
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            var animation = "idler";

            if (_heroPosition.Y < 835 && (keyboardState.IsKeyUp(Keys.W)))
            {
                if (leftCheck == true)
                {
                    animation = "falll";
                    _heroPosition.Y += 700 * dt;
                }

                else
                {
                    animation = "fallr";
                    _heroPosition.Y += 700 * dt;
                }
            }

            if (keyboardState.IsKeyDown(Keys.W) && leftCheck == true)
            {
                animation = "jumpl";

                _heroPosition.Y -= 500 * dt;

                leftCheck = true;

                checkMove = 1;
            }

            if (keyboardState.IsKeyDown(Keys.W) && leftCheck == false)
            {
                animation = "jumpr";
                _heroPosition.Y -= 500 * dt;

                leftCheck = false;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                animation = "runl";
                _heroPosition.X -= walkSpeed;

                leftCheck = true;
                checkMove = 2;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                animation = "runr";
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

                checkMove = 3;

            }

            if (leftCheck)
            {
                switch (checkMove)
                {
                    case 0:

                        animation = "idlel";

                        break;

                    case 1:

                        animation = "jumpl";

                        checkMove = 0;

                        break;

                    case 2:

                        animation = "runl";

                        checkMove = 0;

                        break;

                    case 3:

                        animation = "attackl";

                        checkMove = 0;

                        break;

                    default:

                        animation = "idlel";

                        break;
                }
            }

            _heroSprite.Play(animation);

            _heroSprite.Update(deltaSeconds);
        }
    }
}
