using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 // Drawable things have a color, character, and x/y coords
 

namespace The_Ravening_Toad.Interfaces
{
    public interface IDrawable
    {
        RLColor Color { get; set; }
        char Symbol { get; set; }
        int X { get; set; }
        int Y { get; set; }

        void Draw(RLConsole console, IMap map);
    }
}
