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
        public int current_index = 0;                           // currently selected item
        private readonly int x = 50;                            // x and y coods for selected item
        private readonly int y = 5;                             

        public void Draw(RLConsole console)
        {
            // selected item
            console.Print(x, y, ItemIDtoString((ItemID)current_index), RLColor.Yellow);
            // previous item
            if (current_index - 1 >= 0)
            {
                console.Print(x - 10, y + 1, ItemIDtoString((ItemID)(current_index - 1)), RLColor.Gray);
            }
            // next item
            if (current_index + 1 <= _inventory.Length)
            {
                console.Print(x + 10, y + 1, ItemIDtoString((ItemID)(current_index + 1)), RLColor.Gray);
            }
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
                    return "";
            }
        }

        public int GetInventorySize()
        {
            return _inventory.Length;
        }
    }
}
