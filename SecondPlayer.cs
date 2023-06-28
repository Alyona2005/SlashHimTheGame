using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace SlashHiTheGamme
{
    class SecondPlayer : Character, IHp, IManaged
    {
        public SecondPlayer(List<Hp> amountOfHp)
        {
            Hp = amountOfHp;
            HpCount = (byte)amountOfHp.Count;
        }

        public Texture2D Avatar { get; set; }

        public byte HpCount { get; set; }
        public List<Hp> Hp { get; set; }

        public void ChangeHpCondition(GameTime gameTime, List<Hp> hp, object enemy)
        {
            FirstPlayer firstPlayer = (FirstPlayer)enemy;

            if (firstPlayer.Position.X - Position.X < 10 
                && firstPlayer.Position.X - Position.X > -10 
                && (firstPlayer.CurrentAnimation == "attackr" || firstPlayer.CurrentAnimation == "attackl")
                && firstPlayer.Position.Y - Position.Y < 20
                /**&& firstPlayer.Position.Y - Position.Y > -5**/)
            {
                HpCount--;
                hp.RemoveAt(HpCount);
            }
        }

        private Vector2 _position;
        public Vector2 Position { get { return _position; } set { _position = value; } }

        private bool rightCheck = false;
        private byte checkAction = 0;

        public void Control(GameTime gameTime)
        {
            CurrentAnimation = "idlel";

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var walkSpeed = deltaSeconds * 600;

            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                CurrentAnimation = "runl";
                _position.X -= walkSpeed;

                rightCheck = false;
            }
           
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                CurrentAnimation = "runr";
                _position.X += walkSpeed;

                rightCheck = true;
                checkAction = 2;   
            }

            if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Up))
            {
                CurrentAnimation = "jumpl";
                _position.X -= walkSpeed;

                rightCheck = false;
            }

            if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Up))
            {
                CurrentAnimation = "jumpr";
                _position.X += walkSpeed;

                rightCheck = true;
                checkAction = 5;
            }

            if (keyboardState.IsKeyDown(Keys.RightShift) && rightCheck == false && keyboardState.IsKeyDown(Keys.Left))
            {
                CurrentAnimation = "attackl";

                rightCheck = false;
            }

            if (keyboardState.IsKeyDown(Keys.RightShift) && rightCheck == true && keyboardState.IsKeyDown(Keys.Right))
            {
                CurrentAnimation = "attackr";

                rightCheck = true;

                checkAction = 3;
            }

            if (_position.Y > 600)
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    if (rightCheck == true)
                    {
                        CurrentAnimation = "jumpr";

                        _position.Y -= 1000 * dt;

                        rightCheck = true;

                        checkAction = 1;
                    }

                    else
                    {
                        CurrentAnimation = "jumpl";
                        _position.Y -= 1000 * dt;

                        rightCheck = false;
                    }
                }
            }

            if (_position.Y < 828 && keyboardState.IsKeyUp(Keys.Up))
            {
                if (rightCheck == true)
                {
                    CurrentAnimation = "fallr";
                    _position.Y += 860 * dt;

                    checkAction = 4;
                }

                else
                {
                    CurrentAnimation = "falll";
                    _position.Y += 860 * dt;
                }
            }

            if (_position.X < 115 && keyboardState.IsKeyUp(Keys.Right))
            {
                CurrentAnimation = "idlel";

                _position.X = 100;
            }

            if (_position.X > 1830 && keyboardState.IsKeyUp(Keys.Left))
            {
                CurrentAnimation = "idler";

                _position.X = 1830;
            }

            if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Left))
            {
                CurrentAnimation = "idlel";
            }

            if (rightCheck)
            {
                switch (checkAction)
                {
                    case 0:

                        CurrentAnimation = "idler";

                        break;

                    case 1:

                        CurrentAnimation = "jumpr";
                        checkAction = 0;

                        break;

                    case 2:

                        CurrentAnimation = "runr";
                        checkAction = 0;

                        break;

                    case 3:

                        CurrentAnimation = "attackr";
                        checkAction = 0;

                        break;

                    case 4:

                        CurrentAnimation = "fallr";
                        checkAction = 0;

                        break;

                    case 5:

                        CurrentAnimation = "jumpr";
                        checkAction = 0;

                        break;
                }
            }

            Sprite.Play(CurrentAnimation);
            Sprite.Update(deltaSeconds);
        }
    }
}
