using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using System.Collections.Generic;

namespace SlashItTheGame
{
    interface IHp
    {
        public byte HpCount { get; }
        public List<Hp> Hp { get; set; }

        // У каждого объекта, реализующего данный интерфейс, своя логика изменения HP
        void ChangeHpCondition(GameTime gameTime, List<Hp> hp, object enemy);
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
        public void Initialize(GameTime gameTime, bool check)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (check)
            {
                _hpSprite.Play("remove");
                _hpSprite.Update(deltaSeconds);
            }

            else
            {
                _hpSprite.Play("idle");
                _hpSprite.Update(deltaSeconds);
            }
        }
    }
}
