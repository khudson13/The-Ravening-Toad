using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ravening_Toad.Core
{
    // Direction values correspond to numpad numbers
    // note: diagonals don't actually work yet
    public enum Direction
    {
        None = 0,
        DownLeft = 1,
        Down = 2,
        DownRight = 3,
        Left = 4,
        Center = 5,
        Right = 6,
        UpLeft = 7,
        Up = 8,
        UpRight = 9
    }
}
