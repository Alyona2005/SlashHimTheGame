using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace SlashItTheGame
{
    public class Enemy : IHp
    {
        public Enemy(Hp[] amountOfHp) 
        {
            _hp = amountOfHp;
            _hpCount = (byte)amountOfHp.Length;
        }

        private Hp[] _hp;
        public Hp[] Hp { get { return _hp; } set { _hp = value; } }

        private byte _hpCount;
        public byte HpCount { get { return _hpCount; } }

        private Vector2 _enemyPosition;
        public Vector2 EnemyPosition { get { return _enemyPosition; } set { _enemyPosition = value; } }

        private AnimatedSprite _enemySprite;
        public AnimatedSprite EnemySprite { get { return _enemySprite; } set { _enemySprite = value; } }

        // Логика передвиженя врага
        public void Roam(GameTime gameTime)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var animation = "idle";
            var walkSpeed = deltaSeconds * 600;

            if (animation == "idle")
            {
                _enemyPosition.X -= walkSpeed;

                if (_enemyPosition.X == 900)
                {
                    _enemyPosition.X += walkSpeed;
                }
            }

            _enemySprite.Play(animation);
            _enemySprite.Update(deltaSeconds);
        }

        public void ChangeHpCondition(GameTime gameTime, Hp[] hp, object hero)
        {
            
        }
    }
}
