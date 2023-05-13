using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace SlashItTheGame
{
    public class Enemy : IHp
    {
        private Hp[] _hp;
        public Hp[] Hp { get { return _hp; } set { _hp = value; } }

        private byte _hpCount;
        public byte HpCount { get { return _hpCount; } }

        private Vector2 _enemyPosition;
        public Vector2 EnemyPosition { get { return _enemyPosition; } set { _enemyPosition = value; } }

        private AnimatedSprite _enemySprite;
        public AnimatedSprite EnemySprite { get { return _enemySprite; } set { _enemySprite = value; } }

        public void Roam(GameTime gameTime)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var animation = "idle";




            _enemySprite.Play(animation);
            _enemySprite.Update(deltaSeconds);
        }

        public void ChangeHpStatus(GameTime gameTime, Hp[] hp)
        {

        }
    }
}
