using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

// color assignments

namespace The_Ravening_Toad.Core
{
    public class Colors
    {
        // COLOR ASSIGNMENTS

        public static RLColor FloorBackground = RLColor.Black;
        public static RLColor Floor = Palette.AlternateDarkest;
        public static RLColor FloorBackgroundFov = Palette.Dark;
        public static RLColor FloorFov = Palette.Alternate;

        public static RLColor WallBackground = Palette.SecondaryDarkest;
        public static RLColor Wall = Palette.Secondary;
        public static RLColor WallBackgroundFov = Palette.SecondaryDarker;
        public static RLColor WallFov = Palette.SecondaryLighter;

        public static RLColor TextHeading = Palette.Light;
    }
}
