using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using RaveningToad;

namespace The_Ravening_Toad.Items
{
    public class HealthPotion_Small : Item
    {
        public HealthPotion_Small()
        {
            _name = "Small Health Potion";
            _consumable = true;
            _throwable = false;
            _ItemType = ItemType.Potion;
            _ID = ItemID.S_Health;
            Color = RLNET.RLColor.White;
            Symbol = '[';
        }

        new public void Activate()
        {
            Game.Player.Health += 1;
        }
    }
}
