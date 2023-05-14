using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace SlashItTheGame
{
    interface IHp
    {
        public byte HpCount { get; }
        public Hp[] Hp { get; set; }

        // У каждого объекта, реализующего данный интерфейс, своя логика изменения HP
        void ChangeHpCondition(GameTime gameTime, Hp[] hp, object enemy);
    }
    public class Hp
    {
        public Hp()
        {

        }

        public Hp(Vector2 hpPosition)
        {
            _hpPosition = hpPosition;
        }

        private Vector2 _hpPosition;
        public Vector2 HpPosition { get { return _hpPosition; } set { _hpPosition = value; } }

        private AnimatedSprite _hpSprite;
        public AnimatedSprite HpSprite { get { return _hpSprite; } set { _hpSprite = value; } }

        // Инициализация HP
        public void Initialize(GameTime gameTime)
        {
            var animation = "idle";
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _hpSprite.Play(animation);
            _hpSprite.Update(deltaSeconds);
        }
    }
}
