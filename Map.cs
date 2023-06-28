using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SlashHimTheGame
{
    public class Map
    {
        public Texture2D BackBushes { get; set; }
        public Rectangle BackBushesPosition { get; set; }

        public Texture2D Background { get; set; }
        public Rectangle BackgroundPosition { get; set; }

        public Texture2D Ground { get; set; }
        public Rectangle GroundPosition { get; set; }

        public Texture2D Forest { get; set; }
        public Rectangle ForestPosition { get; set;}
    }
}
