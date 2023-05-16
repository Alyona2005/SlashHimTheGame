using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using System.Collections.Generic;

namespace SlashItTheGame
{
    public class Enemy
    {
        public Enemy(List<Hp> amountOfHp)
        {
            _hp = amountOfHp;
            _hpCount = (byte)amountOfHp.Count;
        }

        private List<Hp> _hp;
        public List<Hp> Hp { get { return _hp; } set { _hp = value; } }

        private byte _hpCount;
        public byte HpCount { get { return _hpCount; } }

        private Vector2 _enemyPosition;
        public Vector2 EnemyPosition { get { return _enemyPosition; } set { _enemyPosition = value; } }

        private AnimatedSprite _enemySprite;
        public AnimatedSprite EnemySprite { get { return _enemySprite; } set { _enemySprite = value; } }

        public string CurrentAnimation { get; private set; } = "idle";

        // Логика действий врага
        public void Interact(GameTime gameTime, Hero hero)
        {
            CurrentAnimation = "idle";

            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var walkSpeed = deltaSeconds * 150;

            if (hero.HeroPosition.X - EnemyPosition.X < 30 && hero.HeroPosition.X - EnemyPosition.X > -100 && hero.HeroPosition.Y > 800)
            {
                CurrentAnimation = "attack";
            }

            if (hero.HeroPosition.X - EnemyPosition.X < 30 && hero.HeroPosition.X - EnemyPosition.X > -100 && hero.HeroPosition.Y > 800 && hero.CurrentAnimation == "attackr")
            {
                CurrentAnimation = "damage";
            }

            _enemySprite.Play(CurrentAnimation);
            _enemySprite.Update(deltaSeconds);
        }

        public void ChangeHpCondition(GameTime gameTime, List<Hp> hp, object hero)
        {
            Hero h = (Hero)hero;


        }
    }
}
