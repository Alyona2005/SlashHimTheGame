using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SlashItTheGame
{
    class FirstPlayer : Character, IHp, IManaged
    {
        public FirstPlayer(List<Hp> amountOfHp)
        {
            Hp = amountOfHp;
            HpCount = (byte)amountOfHp.Count;
        }

        public Texture2D Avatar { get; set; }

        public byte HpCount { get; set; }
        public List<Hp> Hp { get; set; }

        public void ChangeHpCondition(GameTime gameTime, List<Hp> hp, object enemy)
        {
            SecondPlayer secondPlayer = (SecondPlayer)enemy;

            if (secondPlayer.Position.X - Position.X < 10
                && secondPlayer.Position.X - Position.X > -10
                && (secondPlayer.CurrentAnimation == "attackr" || secondPlayer.CurrentAnimation == "attackl")
                && secondPlayer.Position.Y - Position.Y < 20
                /**&& secondPlayer.Position.Y - Position.Y > -5**/)
            {
                HpCount--;
                hp.RemoveAt(HpCount);
            }
        }

        private Vector2 _position;
        public Vector2 Position { get { return _position; } set { _position = value; } }

        private bool leftCheck = false;
        private byte checkAction = 0;

        public void Control(GameTime gameTime)
        {
            CurrentAnimation = "idler";

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var walkSpeed = deltaSeconds * 600;

            var keyboardState = Keyboard.GetState();
            //var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.A))
            {
                CurrentAnimation = "runl";
                _position.X -= walkSpeed;

                leftCheck = true;
                checkAction = 2;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                CurrentAnimation = "runr";
                _position.X += walkSpeed;

                leftCheck = false;
            }

            if (keyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyDown(Keys.W))
            {
                CurrentAnimation = "jumpl";
                _position.X -= walkSpeed;

                leftCheck = true;

                checkAction = 5;
            }

            if (keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyDown(Keys.W))
            {
                CurrentAnimation = "jumpr";
                _position.X += walkSpeed;

                leftCheck = false;
            }
            

            if (keyboardState.IsKeyDown(Keys.E) && leftCheck == false && keyboardState.IsKeyDown(Keys.D))
            {

                CurrentAnimation = "attackr";

                leftCheck = false;
            }

            if (keyboardState.IsKeyDown(Keys.E) && leftCheck == true && keyboardState.IsKeyDown(Keys.A))
            {
                CurrentAnimation = "attackl";

                leftCheck = true;

                checkAction = 3;
            }

            if (_position.Y > 600)
            {
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (leftCheck == true)
                    {
                        CurrentAnimation = "jumpl";

                        _position.Y -= 1000 * dt;

                        leftCheck = true;

                        checkAction = 1;
                    }

                    else
                    {
                        CurrentAnimation = "jumpr";
                        _position.Y -= 1000 * dt;

                        leftCheck = false;
                    }
                }
            }

            if (_position.Y < 828 && keyboardState.IsKeyUp(Keys.W))
            {
                if (leftCheck == true)
                {
                    CurrentAnimation = "falll";
                    _position.Y += 860 * dt;

                    checkAction = 4;
                }

                else
                {
                    CurrentAnimation = "fallr";
                    _position.Y += 860 * dt;
                }
            }

            if (_position.X < 115 && keyboardState.IsKeyUp(Keys.D))
            {
                CurrentAnimation = "idlel";

                _position.X = 100;
            }

            if (_position.X > 1830 && keyboardState.IsKeyUp(Keys.A))
            {
                CurrentAnimation = "idler";

                _position.X = 1830;
            }

            if (keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyDown(Keys.A))
            {
                CurrentAnimation = "idler";
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

                    case 3:

                        CurrentAnimation = "attackl";
                        checkAction = 0;

                        break;

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

            Sprite.Play(CurrentAnimation);
            Sprite.Update(deltaSeconds);
        }
    }
}
