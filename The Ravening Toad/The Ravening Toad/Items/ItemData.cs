using RLNET;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Items
{
    public class ItemData
    {
        // THIS CLASS CONTAINS ALL ITEM RELATED DATA, WHICH CAN BE RETRIEVED USING ANY OF THE
        // GETTER METHODS LISTED BELOW, FOLLOWED BY THE ITEM MASTER LIST, FOLLOWED BY ALL
        // THE ITEM 'ACTIVATE' FUNCTIONS IN ALPHABETICAL ORDER

        public RLColor GetColor(ItemID ID)
        {
            return InterpretColor(AccessItem(ID, "color"));
        }
        public string GetType(ItemID ID)
        {
            return AccessItem(ID, "type");
        }
        public string GetName(ItemID ID)
        {
            return AccessItem(ID, "name");
        }

        public string GetSymbol(ItemID ID)
        {
            return AccessItem(ID, "symbol");
        }

        private string AccessItem(ItemID ID, string Data)
        {
            switch (ID)
            {
                case ItemID.S_Health:
                    if (Data == "color") { return "white"; }
                    else if (Data == "type") { return "potion"; }
                    else if (Data == "name") { return "Small Health Potion"; }
                    else if (Data == "symbol") { return "["; }
                    break;

                default:
                    break;

            }
                    


            return "FAIL";
        }

        private RLColor InterpretColor(string Color)
        {
            return RLColor.White;
        }

        private ItemType InterpretType(string Type)
        {
            return ItemType.Food;
        }
    }
}
