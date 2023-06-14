using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace SlashItTheGame
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
