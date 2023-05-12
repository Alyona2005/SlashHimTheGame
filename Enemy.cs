using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace SlashThemTheGame
{
    public class Enemy
    {
        public Vector2 EnemyPosition { get; set; }

        private AnimatedSprite _enemySprite;
        public AnimatedSprite EnemySprite { get { return _enemySprite; } set { _enemySprite = value; } }

        public void Roam(GameTime gameTime)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var animation = "idle";


            _enemySprite.Play(animation);
            _enemySprite.Update(deltaSeconds);
        }
    }
}
