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
            _ItemType = ItemType.Potion;
            _ID = ItemID.L_Health;
            Color = RLNET.RLColor.White;
            Symbol = '[';
        }

        public override void Activate()
        {
            Game.Player.Health = Game.Player.MaxHealth;
            Game.ItemsMenu.DeductItem();
        }
    }
}
