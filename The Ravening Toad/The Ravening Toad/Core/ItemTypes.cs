using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        NULL
    }
}
