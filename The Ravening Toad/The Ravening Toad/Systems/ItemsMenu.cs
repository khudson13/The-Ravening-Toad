﻿using RaveningToad;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using The_Ravening_Toad.Items;

// ALL DATA RELATED TO ITEMS LIVES HERE

namespace The_Ravening_Toad.Systems
{
    public class ItemsMenu
    {
        // prints items to screen in a rotating wheel, and processes item activations

        private Item[] _item_definitions = new Item[(int)ItemID.END_USABLE];            // access item functionality here, items indexed in same order as _inventory for ease of use
        private bool _inventory_empty = false;                                          // is inventory empty
        public bool targeting = false;                                                  // currently selecting target for throw
        public int target = 0;                                                          // index of current target in visible monsters
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
                int index_to_print = current_index - 1;
                while (index_to_print >= 0)
                {
                    if (_item_definitions[index_to_print].Owned > 0)
                    {
                        console.Print(x - 10, y + 1, ItemIDtoString((ItemID)(index_to_print)), RLColor.Gray);
                        index_to_print = -1;
                    }
                    else
                    {
                        --index_to_print;
                    }
                }

                // next item
                index_to_print = current_index + 1;
                while (index_to_print < _item_definitions.Length)
                {
                    if (_item_definitions[index_to_print].Owned > 0)
                    {
                        console.Print(x + 10, y + 1, ItemIDtoString((ItemID)(index_to_print)), RLColor.Gray);
                        index_to_print = _item_definitions.Length;
                    }
                    else
                    {
                        ++index_to_print;
                    }
                }
            }
        }

        public void ActivateItem()
        {
            _item_definitions[current_index].Activate();
        }

        public void AddItem(ItemID ID)
        {
            ++_item_definitions[(int)ID].Owned;
        }

        public void DeductItem()
        {
            --_item_definitions[current_index].Owned;
            if (_item_definitions[current_index].Owned == 0)
            {
                while (_item_definitions[current_index].Owned == 0 && current_index < _item_definitions.Length - 1)
                {
                    ++current_index;
                }
                if (_item_definitions[current_index].Owned == 0)
                {
                    while (_item_definitions[current_index].Owned == 0 && current_index > 0)
                    {
                        --current_index;
                    }
                    if (_item_definitions[current_index].Owned == 0)
                    {
                        _inventory_empty = true;
                    }
                }
            }
        }

        public int GetInventorySize()
        {
            return _item_definitions.Length;
        }

        public int GetQuantity(int index)
        {
            return _item_definitions[index].Owned;
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
                case ItemID.Grenade:
                    return "Grenade";
                default:
                    return "";
            }
        }
        
        public bool CanTarget()
        {
            // returns whether or not targets exist and sets targeting mode
            if (!Game.Player.visible_monsters.Any())
            {
                return false;                
            }
            target = 0;
            Game.Player.visible_monsters[0].BackColor = RLColor.Red;
            Game.GameState.pause = true;
            targeting = true;
            return true;
        }

        // navigate targeting selection
        public void ChooseTarget(RLKey key)
        {
            Game.Player.visible_monsters[target].BackColor = Game.Player.visible_monsters[target].DefaultBackColor;
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
                Game.Player.visible_monsters[target].BackColor = RLColor.Red;
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
                Game.Player.visible_monsters[target].BackColor = RLColor.Red;
            }
            else if (key == RLKey.Space)
            {
                Game.Player.visible_monsters[target].BackColor = Game.Player.visible_monsters[target].DefaultBackColor;
                _item_definitions[current_index].Activate();                
            }            
        }

        public ItemsMenu()
        {
            // POPULATE ITEM DEFINITIONS LIST
            int i = 0;
            foreach (Item x in _item_definitions)
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
                
                ++i;
            }
        }
    }
}
