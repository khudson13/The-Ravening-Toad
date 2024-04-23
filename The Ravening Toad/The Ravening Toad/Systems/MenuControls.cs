using RaveningToad;
using RLNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Systems
{
    public class MenuControls
    {
        public void MenuControl(RLKey key)
        {
            //************
            // MAIN MENU *
            //************
            if (Game.GameState.mainmenu)
            {
                if (key == RLKey.Escape)
                {
                    Game.GameState.pause = false;
                    Game.GameState.mainmenu = false;
                }
                else if (key == RLKey.Number1)
                {
                    Game.GameState.savemenu = true;
                    Game.SaveMenu.PopulateMenu();
                    Game.GameState.mainmenu = false;
                }
                else if (key == RLKey.Number2)
                {
                    Game.GameState.loadmenu = true;
                    Game.LoadMenu.PopulateMenu();
                    Game.GameState.mainmenu = false;
                }
                else if (key == RLKey.Number3)
                {
                    Game.ExitGame();
                }
            }
            
            //************
            // SAVE MENU *
            //************
            if (Game.GameState.savemenu)
            {
                if (key == RLKey.Up)
                {
                    if (Game.SaveMenu.selection > 0)
                    {
                        --Game.SaveMenu.selection;
                    }
                }
                else if (key == RLKey.Down)
                {
                    if (Game.SaveMenu.selection < 9)
                    {
                        ++Game.SaveMenu.selection;
                    }
                }
                else if (key == RLKey.Enter)
                {
                    Game.Save.saveGame(Game.Player, Game.SaveMenu.selection);
                    Game.SaveMenu.PopulateMenu();
                    Game.MessageLog.Add("Game Saved");
                    Game.GameState.pause = false;
                    Game.GameState.savemenu = false;
                }
                else if (key == RLKey.Escape)
                {
                    Game.GameState.savemenu = false;
                    Game.GameState.mainmenu = true;
                }
            }

            //************
            // LOAD MENU *
            //************
            if (Game.GameState.loadmenu)
            {
                if (key == RLKey.Up)
                {
                    if (Game.LoadMenu.selection > 0)
                    {
                        --Game.LoadMenu.selection;
                    }
                }
                else if (key == RLKey.Down)
                {
                    if (Game.LoadMenu.selection < 9)
                    {
                        ++Game.LoadMenu.selection;
                    }
                }
                else if (key == RLKey.Enter && !Game.LoadMenu.delete)
                {
                    if (Game.Load.loadGame(Game.Player, Game.LoadMenu.selection))
                    {
                        Game.CommandSystem = new CommandSystem();
                        Game.MessageLog.Add("Game Loaded");
                        Game.GameState.pause = false;
                        Game.GameState.loadmenu = false;
                        Game.GameState.location = "dungeon";
                    }
                    else
                    {
                        Game.MessageLog.Add("LOAD FAILED!");
                        Game.GameState.mainmenu = false;
                        Game.GameState.loadmenu = false;
                    }
                }
                else if (key == RLKey.Enter && Game.LoadMenu.delete)
                {
                    if (Game.LoadMenu.choices[Game.LoadMenu.selection] == "really delete?")
                    {
                        string filename = "Save Files\\Save" + Game.LoadMenu.selection + ".txt";
                        using (StreamWriter writer = new StreamWriter(filename))
                        {

                            writer.WriteLine(false);
                        }
                        Game.LoadMenu.PopulateMenu();
                    }
                    else
                    {
                        Game.LoadMenu.choices[Game.LoadMenu.selection] = "really delete?";
                    }
                }
                else if (key == RLKey.Escape)
                {
                    Game.LoadMenu.PopulateMenu();
                    Game.GameState.loadmenu = false;
                    Game.LoadMenu.delete = false;
                    if (Game.GameState.location != "start")
                    {
                        Game.GameState.mainmenu = true;
                    }
                }
            }

            //*************
            // START MENU *
            //*************
            if (Game.GameState.location == "start" && !Game.GameState.loadmenu)
            {
                if (key == RLKey.Number1)
                {
                    Game.GameState.location = "dungeon";
                    Game.GameState.pause = false;
                }
                else if (key == RLKey.Number2)
                {
                    Game.GameState.loadmenu = true;
                }
                else if (key == RLKey.Number3)
                {
                    Game.GameState.loadmenu = true;
                    Game.LoadMenu.delete = true;
                }
                else if (key == RLKey.Number4)
                {
                    Game.ExitGame();
                }
            }

            //************
            // CAFE MENU *
            //************
            if (Game.GameState.location == "cafe")
            {
                if (key == RLKey.Number1)
                {
                    for (int i = 0; i < (Game.ToadCafe.tables * 2); ++i)
                    {
                        Game.ToadCafe.SellMostValuable();
                    }
                    Game.GameState.location = "dungeon";
                    Game.GameState.pause = false;
                    Game.MessageLog.Add($"{Game.Player.Name} has returned to the dungeon!");
                }
                else if (key == RLKey.Number2)
                {
                    Game.Player.Meat -= 6;
                    ++Game.ToadCafe.readytoserve[0];
                    Game.MessageLog.Add($"The Toad prepared a {Game.ToadCafe.viableRecipes[0].Name}! Delicious!");
                }
            }
        }
    }
}
