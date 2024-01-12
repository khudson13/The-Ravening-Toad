using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * IMPORTANT
 * 
 * when adding new items update the following:
 * 
 * this enum
 * ItemsMenu ItemIDtoString() && constructor
 * 
 * make sure to remove item from inventory in item's Activate()
 */

namespace The_Ravening_Toad.Core
{
    public enum ItemType
    {
        Food,
        Drink,
        Ingredient,
        Potion,
        Lacquer,
        Grenade
    }

    public enum ItemID
    {
        // don't forget to update ItemIDtoString() in ItemsMenu.cs when adding to this
        S_Health,
        M_Health,
        L_Health,
        Grenade,

        END_USABLE, // items beyond this point can't be used in dungeon and don't appear in inventory menu

        NULL
    }
}
