using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace SlashItTheGame
{
    interface IHp
    {
        public byte HpCount { get; }
        public Hp[] Hp { get; set; }

        void ChangeHpStatus(GameTime gameTime, Hp[] hp);
    }
    public class Hp
    {
        public Hp(Vector2 hpPosition)
        {
            _hpPosition = hpPosition;
        }

        private Vector2 _hpPosition;
        public Vector2 HpPosition { get { return _hpPosition; } set { _hpPosition = value; } }

        private AnimatedSprite _hpSprite;
        public AnimatedSprite HpSprite { get { return _hpSprite; } set { _hpSprite = value; } }

        //Логика состояния HP в зависимости от ситуации
        public void Initialize(GameTime gameTime)
        {
            var animation = "idle";
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _hpSprite.Play(animation);
            _hpSprite.Update(deltaSeconds);
        }
    }
}
