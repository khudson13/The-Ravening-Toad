using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using RaveningToad;

namespace The_Ravening_Toad.Items
{
    public class HealthPotion_Large : Item
    {
        public HealthPotion_Large()
        {
            _name = "Full Health Potion";
            _consumable = true;
            _throwable = false;
            _ItemType = ItemType.Potion;
            Color = RLNET.RLColor.White;
            Symbol = '[';
        }

        new public void Consume()
        {
            Game.Player.Health = Game.Player.MaxHealth;
        }
    }
}
