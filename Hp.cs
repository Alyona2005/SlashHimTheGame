using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using System.Collections.Generic;

namespace SlashHimTheGame
{
    interface IHp
    {
        public byte HpCount { get; }
        public List<Hp> Hp { get; set; }

        void ChangeHpCondition(GameTime gameTime, List<Hp> hp, object enemy);
    }
    class Hp
    {
        public Hp()
        {

        }

        public Hp(Vector2 hpPosition)
        {
            Position = hpPosition;
        }

        public Vector2 Position { get; set; }
        public AnimatedSprite Sprite { get; set; }

        // Инициализация HP
        public void Initialize(GameTime gameTime)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Sprite.Play("idle");
            Sprite.Update(deltaSeconds);
            
        }
    }
}
