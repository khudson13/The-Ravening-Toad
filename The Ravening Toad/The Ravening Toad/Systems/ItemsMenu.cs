﻿using RaveningToad;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using The_Ravening_Toad.Items;

namespace The_Ravening_Toad.Systems
{
    public class ItemsMenu
    {
        // prints items to screen in a rotating wheel, and processes item activations

        private int[] _inventory = Game.Player.GetInventory();                          // copy of player inventory used for display
        private readonly Item[] _item_definitions = new Item[(int)ItemID.END_USABLE];   // access item functionality here, items indexed in same order as _inventory for ease of use
        private bool _inventory_empty = false;                                          // is inventory empty
        public bool targeting = false;                                                  // currently selecting target for throw
        private int target = 0;                                                         // index of current target in visible monsters
        public int current_index = 0;                                                   // currently selected item
        private readonly int x = 50;                                                    // x and y coods for selected item
        private readonly int y = 5;                             

        public void Draw(RLConsole console)
        {
            if (_inventory_empty)
            {
                console.Print(x, y, "NOTHING", RLColor.Yellow);
            }
            else
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
        }

        public void ActivateItem()
        {
            _item_definitions[current_index].Activate();
            --_inventory[current_index];
            if (_inventory[current_index] == 0)
            {
                while (_inventory[current_index] == 0 && current_index < _inventory.Length - 1)
                {
                    ++current_index;
                }
                if (_inventory[current_index] == 0)
                {
                    while (_inventory[current_index] == 0 && current_index > 0)
                    {
                        --current_index;                        
                    }
                    if (_inventory[current_index] == 0)
                    {
                        _inventory_empty = true;
                    }
                }
            }
        }

        public int GetInventorySize()
        {
            return _inventory.Length;
        }

        public int GetQuantity(int index)
        {
            return _inventory[index];
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
        
        public bool CanTarget(RLConsole statconsole)
        {
            // returns whether or not targets exist and sets targeting mode
            if (!Game.Player.visible_monsters.Any())
            {
                return false;
            }
            target = 0;
            statconsole.SetBackColor(Game.Player.visible_monsters[0].X, Game.Player.visible_monsters[target].Y, RLColor.Red);
            Game.Player.Pause = true;
            targeting = true;
            return true;
        }

        // navigate targeting selection
        public void ChooseTarget(RLConsole statconsole, RLKey key)
        {
            if (key == RLKey.W || key == RLKey.D)
            {
                if (target == Game.Player.visible_monsters.Count - 1)
                {
                    target = 0;
                }
                else
                {
                    ++target;
                }
                statconsole.SetBackColor(Game.Player.visible_monsters[0].X, Game.Player.visible_monsters[target].Y, RLColor.Red);
            }
            else if (key == RLKey.S || key == RLKey.A)
            {
                if (target == 0)
                {
                    target = Game.Player.visible_monsters.Count - 1;
                }
                else
                {
                    --target;
                }
                statconsole.SetBackColor(Game.Player.visible_monsters[0].X, Game.Player.visible_monsters[target].Y, RLColor.Red);
            }
            else if (key == RLKey.Space)
            {
                _item_definitions[current_index].Activate();
            }

            
        }

        public ItemsMenu()
        {
            // POPULATE ITEM DEFINITIONS ARRAY
            for (int i = 0; i < (int)ItemID.END_USABLE; ++i)
            {
                switch (i){
                    case (int)ItemID.S_Health:
                        _item_definitions[i] = new HealthPotion_Small();
                        break;
                    case (int)ItemID.M_Health:
                        _item_definitions[i] = new HealthPotion_Medium();
                        break;
                    case ((int)ItemID.L_Health):
                        _item_definitions[i] = new HealthPotion_Large();
                        break;
                    case ((int)ItemID.Grenade):
                        _item_definitions[i] = new Grenade();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
