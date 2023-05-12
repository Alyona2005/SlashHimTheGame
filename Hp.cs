using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace SlashThemTheGame
{
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
        public void ChangeStatus(GameTime gameTime, Hero hero)
        {
            var animation = "idle";
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;




            _hpSprite.Play(animation);
            _hpSprite.Update(deltaSeconds);
        }
    }
}
