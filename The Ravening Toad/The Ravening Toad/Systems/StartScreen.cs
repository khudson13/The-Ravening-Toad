﻿using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Systems
{
    public class StartScreen
    {
        public void Draw(RLConsole console)
        {
            
            console.Print(13, 10, "THE RAVENING TOAD", RLColor.Yellow);
            console.Print(15, 15, "1 = new game", RLColor.Yellow);
            console.Print(15, 16, "2 = load game", RLColor.Yellow);
            console.Print(15, 17, "3 = delete game", RLColor.Yellow);
            console.Print(15, 18, "4 = exit", RLColor.Yellow);
        }
    }
}
