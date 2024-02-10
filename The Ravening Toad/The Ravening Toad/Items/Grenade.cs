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

        public override void Activate()
        {
            // if not targeting, start targeting
            if (!Game.ItemsMenu.targeting)
            {
                Game.ItemsMenu.CanTarget(Game._mapConsole);
            }

            // second activation, once target has been selected
            else
            {
                Console.WriteLine("BOOM");

                // set values for area of effect
                int left_of_monster = Game.Player.visible_monsters[Game.ItemsMenu.target].X - 1;
                int right_of_monster = Game.Player.visible_monsters[Game.ItemsMenu.target].X + 1;
                int above_monster = Game.Player.visible_monsters[Game.ItemsMenu.target].Y - 1;
                int below_monster = Game.Player.visible_monsters[Game.ItemsMenu.target].Y + 1;

                foreach (Monster monster in Game.Player.visible_monsters)
                {            
                    // damage every monster in area of effect
                    if ((monster.X >= left_of_monster && monster.X <= right_of_monster) && (monster.Y <= below_monster && monster.Y >= above_monster))
                    {
                        monster.Health -= 5;
                        CommandSystem.ResolveDamage(monster, true, 5);
                    }
                }
                Game.ItemsMenu.targeting = false;
                Game.Player.Pause = false;
                Game.ItemsMenu.DeductItem();
            }
        }
    }
}
