using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace SlashHimTheGame
{
    interface IManaged
    {
        void Control(GameTime gameTime);
    }

    class Character
    {
        public string CurrentAnimation { get; set; }
        public AnimatedSprite Sprite { get; set; }
    }
}
