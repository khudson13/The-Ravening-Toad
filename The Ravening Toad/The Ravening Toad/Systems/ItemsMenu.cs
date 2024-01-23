using RaveningToad;
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
                    if (_inventory[index_to_print] > 0)
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
                while (index_to_print < _inventory.Length)
                {
                    if (_inventory[index_to_print] > 0)
                    {
                        console.Print(x + 10, y + 1, ItemIDtoString((ItemID)(index_to_print)), RLColor.Gray);
                        index_to_print = _inventory.Length;
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

        public void DeductItem()
        {
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
            _inventory = Game.Player.GetInventory();
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
                case ItemID.Grenade:
                    return "Grenade";
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
            _item_definitions[(int)ItemID.S_Health] = new HealthPotion_Small();
            _item_definitions[(int)ItemID.M_Health] = new HealthPotion_Medium();
            _item_definitions[(int)ItemID.L_Health] = new HealthPotion_Large();
            _item_definitions[(int)ItemID.Grenade] = new Grenade();
            // POPULATE ITEM DEFINITIONS ARRAY
            /*for (int i = 0; i < (int)ItemID.END_USABLE; ++i)
            {
                switch (i){
                    case (int)ItemID.S_Health:
                        Console.WriteLine((int)ItemID.S_Health);
                        Console.WriteLine(i);
                        _item_definitions[i] = new HealthPotion_Small();
                        Console.WriteLine(ItemIDtoString(_item_definitions[i].ItemID));
                        break;
                    case (int)ItemID.M_Health:
                        Console.WriteLine((int)ItemID.M_Health);
                        Console.WriteLine(i);
                        _item_definitions[i] = new HealthPotion_Medium();
                        Console.WriteLine(ItemIDtoString(_item_definitions[i].ItemID));
                        break;
                    case ((int)ItemID.L_Health):
                        Console.WriteLine((int)ItemID.L_Health);
                        Console.WriteLine(i);
                        _item_definitions[i] = new HealthPotion_Large();
                        Console.WriteLine(ItemIDtoString(_item_definitions[i].ItemID));
                        break;
                    case ((int)ItemID.Grenade):
                        Console.WriteLine((int)ItemID.Grenade);
                        Console.WriteLine(i);
                        _item_definitions[i] = new Grenade();
                        Console.WriteLine(ItemIDtoString(_item_definitions[i].ItemID));
                        break;
                    default:
                        break;
                }
            }*/
            foreach (Item x in _item_definitions)
            {
                Console.WriteLine(x.ToString());
                Console.WriteLine(ItemIDtoString(x.ItemID));
                Console.WriteLine(x.Name);
            }

            Item _health = new HealthPotion_Small();
            Console.WriteLine(ItemIDtoString(_health.ItemID));
        }
    }
}
