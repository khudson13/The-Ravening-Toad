using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Here be the player object

namespace The_Ravening_Toad.Core
{
    public class Player : Actor
    {
        public Player()
        {
            Awareness = 15;
            Name = "The Toad";
            Color = Colors.Player;
            Symbol = '@';
            X = 10;
            Y = 10;
        }
    }
}
