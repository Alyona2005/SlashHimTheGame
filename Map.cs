using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SlashThemTheGame
{
    public class Map
    {
        public Texture2D Background { get; set; }
        public Rectangle backgroundPosition { get; set; }
        public Texture2D Ground { get; set; }
        public Rectangle GroundPosition { get; set; }
    }
}
