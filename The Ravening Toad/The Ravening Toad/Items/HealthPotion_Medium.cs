﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using RaveningToad;

namespace The_Ravening_Toad.Items
{
    public class HealthPotion_Medium : Item
    {
        public HealthPotion_Medium()
        {
            _name = "Medium Health Potion";
            _consumable = true;
            _throwable = false;
            _ItemType = ItemType.Potion;
            _ID = ItemID.M_Health;
            Color = RLNET.RLColor.White;
            Symbol = '[';
        }

        public override void Activate()
        {
            Game.Player.Health += 5;
            Game.ItemsMenu.DeductItem();
        }

        public override string ToString()
        {
            return "Medium Health Potion";
        }
    }
}