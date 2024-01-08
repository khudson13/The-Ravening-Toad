using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaveningToad;
using The_Ravening_Toad.Core;
using The_Ravening_Toad.Systems;

namespace The_Ravening_Toad.Items
{
    public class Grenade : Item
    {
        public Grenade() 
        {
            _name = "Grenade";
            _ItemType = ItemType.Grenade;
            _ID = ItemID.Grenade;
            Color = RLNET.RLColor.White;
            Symbol = '}';
        }

        new static public void Activate()
        {
            if (!Game.ItemsMenu.targeting)
            {
                Game.ItemsMenu.targeting = true;
            }
            else
            {
                // damage in area of effect
            }
        }
    }
}
