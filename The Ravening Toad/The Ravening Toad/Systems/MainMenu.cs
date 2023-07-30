using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ravening_Toad.Systems
{
    public class MainMenu
    {
        private string[] choices = { "1 = Save", "2 = Load", "3 = Exit" };
        // Draw each option to the console
        public void Draw(RLConsole console)
        {
            //console.Clear();
            
            for (int i = 0; i < choices.Length; i++)
            {
                console.Print(1, i + 1, choices[i], RLColor.White);
            }
        }
    }
}
