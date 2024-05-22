using RLNET;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaveningToad;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Systems
{
    public class PlayerControls
    {
        public bool ControlParser(RLKey key)
        {
            bool didPlayerAct = false;

            // DIRECTION CONTROLS
            if (key == RLKey.Up || key == RLKey.W)
            {
                didPlayerAct = Game.CommandSystem.MovePlayer(Direction.Up);
            }
            else if (key == RLKey.Down || key == RLKey.S)
            {
                didPlayerAct = Game.CommandSystem.MovePlayer(Direction.Down);
            }
            else if (key == RLKey.Left || key == RLKey.A)
            {
                didPlayerAct = Game.CommandSystem.MovePlayer(Direction.Left);
            }
            else if (key == RLKey.Right || key == RLKey.D)
            {
                didPlayerAct = Game.CommandSystem.MovePlayer(Direction.Right);
            }
            // ITEM CONTROLS
            else if (key == RLKey.E)
            {
                // select next item with non-zero quantity
                if (Game.ItemsMenu.current_index < Game.ItemsMenu.GetInventorySize() - 1)
                {
                    int temp = Game.ItemsMenu.current_index;
                    ++Game.ItemsMenu.current_index;
                    while (Game.ItemsMenu.GetQuantity(Game.ItemsMenu.current_index) == 0 && Game.ItemsMenu.current_index < Game.ItemsMenu.GetInventorySize() - 1)
                    {
                        ++Game.ItemsMenu.current_index;
                    }
                    if (Game.ItemsMenu.GetQuantity(Game.ItemsMenu.current_index) == 0)
                    {
                        Game.ItemsMenu.current_index = temp;
                    }
                    else
                    {
                        didPlayerAct = true;
                    }
                }
            }
            else if (key == RLKey.Q)
            {
                // select previous item with non-zero quantity
                if (Game.ItemsMenu.current_index > 0)
                {
                    int temp = Game.ItemsMenu.current_index;
                    --Game.ItemsMenu.current_index;
                    while (Game.ItemsMenu.GetQuantity(Game.ItemsMenu.current_index) == 0 && Game.ItemsMenu.current_index > 0)
                    {
                        --Game.ItemsMenu.current_index;
                    }
                    if (Game.ItemsMenu.GetQuantity(Game.ItemsMenu.current_index) == 0)
                    {
                        Game.ItemsMenu.current_index = temp;
                    }
                    else
                    {
                        didPlayerAct = true;
                    }
                }
            }
            // activate selected item
            else if (key == RLKey.Space)
            {
                Game.ItemsMenu.ActivateItem();
                didPlayerAct = true;
            }
            // USE EXIT
            else if (key == RLKey.Period)
            {
                if (Game.ToadMap.CanMoveDownToNextLevel())
                {
                    Game.seed = (int)DateTime.UtcNow.Ticks;
                    Game.Random = new DotNetRandom(Game.seed);
                    MapGenerator mapGenerator = new MapGenerator(Game.mapWidth, Game.mapHeight, 20, 13, 7, Game.mapLevel);
                    Game.ToadMap.Clear();
                    Game.ToadMap = mapGenerator.CreateMap();
                    Game.MessageLog = new MessageLog();
                    Game.CommandSystem = new CommandSystem();
                    Game.GameState.pause = true;
                    Game.GameState.location = "cafe";
                    Game.MessageLog.Add($"{Game.Player.Name} has returned to the cafe");
                }
            }
            // OPEN MENU
            else if (key == RLKey.Escape)
            {
                Game.GameState.pause = true;
                Game.GameState.mainmenu = true;
                didPlayerAct = true;
            }

            return didPlayerAct;
        }
    }
}
