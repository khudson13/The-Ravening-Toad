using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Systems
{
    public class ItemsMenu
    {
        // prints items to screen in a rotating wheel, and processes item activations

        private int[] _inventory = new int[(int)ItemID.NULL];   // copy of player inventory used for display
        private int _current_index = 0;                         // currently selected item
        private readonly int x = 20;                            // x and y coods for selected item
        private readonly int y = 5;                             

        public void Draw(RLConsole console)
        {
            // selected item
            console.Print(x, y, ItemIDtoString((ItemID)_current_index), RLColor.Yellow);
            // previous item

            // next item
        }

        public string ItemIDtoString(ItemID itemID)
        {
            switch (itemID)
            {
                case ItemID.S_Health:
                    return "S_Health";
                case ItemID.M_Health:
                    return "M_Health";
                case ItemID.L_Health:
                    return "L_Health";
                default:
                    return "NULL";
            }
        }


        private string[] choices = { "1 = Save", "2 = Load", "3 = Exit" };
        // Draw each option to the console
        public void fDraw(RLConsole console)
        {
            //console.Clear();

            for (int i = 0; i < choices.Length; i++)
            {
                console.Print(1, i + 1, choices[i], RLColor.White);
            }
        }
    }
}
