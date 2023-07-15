using RLNET;
using RogueSharp.Random;
using System;
using The_Ravening_Toad.Core;
using The_Ravening_Toad.Systems;

namespace RaveningToad
{
    public class Game
    {
        //*************************
        // DEFINE PRIMARY WINDOWS *
        // measured in 8x8 tiles  *
        //*************************
        // Main Screen
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;
        private static RLRootConsole _rootConsole;

        // Map Window
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;
        private static RLConsole _mapConsole;

        // Message Window
        private static readonly int _messageWidth = 80;
        private static readonly int _messageHeight = 11;
        private static RLConsole _messageConsole;

        // Stats Window
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;
        private static RLConsole _statConsole;

        // Inventory Window
        private static readonly int _inventoryWidth = 80;
        private static readonly int _inventoryHeight = 11;
        private static RLConsole _inventoryConsole;

        private static int _mapLevel = 1;

        //************************
        // DEFINE BASIC ELEMENTS *
        //************************
        public static Player Player { get; set; }
        public static ToadMap ToadMap { get; private set; }

        public static CommandSystem CommandSystem { get; private set; }

        public static MessageLog MessageLog { get; private set; }

        public static SchedulingSystem SchedulingSystem { get; private set; }

        public static Save Save { get; private set; }

        // Singleton of IRandom used throughout the game when generating random numbers
        public static IRandom Random { get; private set; }

        private static bool _renderRequired = true;


        //*************
        // BEGIN MAIN *
        //*************
        public static void Main()
        {
            // Store font file in variable for easy changing later
            string fontFileName = "terminal8x8.png";

            // Generate seed for random number generator using time
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);

            //*****************************
            // INITIALIZE CONSOLE WINDOWS *
            // using RLNet                *
            //*****************************
            // Title for top of console
            string consoleTitle = $"The Ravening Toad - Seed {seed}";
            // RLNet creates root console with the font, 8x8 tiles
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
              8, 8, 1f, consoleTitle);
            // Initialize sub consoles and Blit to root console
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _messageConsole = new RLConsole(_messageWidth, _messageHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _inventoryConsole = new RLConsole(_inventoryWidth, _inventoryHeight);

            //* Set background color and text for each console *

            //_mapConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, Colors.FloorBackground);
            //_mapConsole.Print(1, 1, "Map", Colors.TextHeading);

            //_messageConsole.SetBackColor(0, 0, _messageWidth, _messageHeight, Palette.DeepWater);
            //_messageConsole.Print(1, 1, "Messages", Colors.TextHeading);

            // Create a new MessageLog and print the random seed used to generate the level
            MessageLog = new MessageLog();
            MessageLog.Add("The Toad is on the hunt!");
            MessageLog.Add($"Map - Level {_mapLevel} -  created with seed '{seed}'");

            //_statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Palette.OldStone);
            //_statConsole.Print(1, 1, "Stats", Colors.TextHeading);

            _inventoryConsole.SetBackColor(0, 0, _inventoryWidth, _inventoryHeight, Palette.Wood);
            _inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);

            //*************************
            // DRAWING EVENT HANDLERS *
            //*************************
            // Set up a handler for RLNET's Update event
            _rootConsole.Update += OnRootConsoleUpdate;
            // Set up a handler for RLNET's Render event
            _rootConsole.Render += OnRootConsoleRender;

            //******************************
            // INSTANTIATE PRIMARY OBJECTS *
            //******************************

            // Create Scheduling System
            SchedulingSystem = new SchedulingSystem();


            // Create map
            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight, 20, 13, 7, _mapLevel);
            ToadMap = mapGenerator.CreateMap();
            ToadMap.UpdatePlayerFieldOfView();

            // Create Command System
            CommandSystem = new CommandSystem();

            // Create Save/Load Systems
            Save = new Save();

            //**************
            // AND ACTION! *
            //**************
            // Begin RLNET's game loop
            _rootConsole.Run();
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            //**********************
            // SEE IF PLAYER ACTED *
            // UPDATE IF YES       *
            //**********************
            bool didPlayerAct = false;
            RLKeyPress keyPress = _rootConsole.Keyboard.GetKeyPress();

            if (CommandSystem.IsPlayerTurn)
            {
                if (keyPress != null)
                {
                    if (keyPress.Key == RLKey.Up)
                    {
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
                    }
                    else if (keyPress.Key == RLKey.Down)
                    {
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Down);
                    }
                    else if (keyPress.Key == RLKey.Left)
                    {
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
                    }
                    else if (keyPress.Key == RLKey.Right)
                    {
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Right);
                    }
                    else if (keyPress.Key == RLKey.Period)
                    {
                        if (ToadMap.CanMoveDownToNextLevel())
                        {
                            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight, 20, 13, 7, ++_mapLevel);
                            ToadMap = mapGenerator.CreateMap();
                            MessageLog = new MessageLog();
                            CommandSystem = new CommandSystem();
                            _rootConsole.Title = $"The Ravening Toad - Level {_mapLevel}";
                            didPlayerAct = true;
                        }
                    }
                    else if (keyPress.Key == RLKey.S)
                    {
                        Save.saveGame(Player);
                    }
                    else if (keyPress.Key == RLKey.Escape)
                    {
                        _rootConsole.Close();
                    }
                }

                if (didPlayerAct)
                {
                    _renderRequired = true;
                    CommandSystem.EndPlayerTurn();
                }
            }
            else
            {
                CommandSystem.ActivateMonsters();
                _renderRequired = true;
            }
        }

        // Event handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            // Only redraw as needed
            if (_renderRequired)
            {
                // blank it before drawing
                _mapConsole.Clear();
                _statConsole.Clear();
                _messageConsole.Clear();

                // Draw the map
                ToadMap.Draw(_mapConsole, _statConsole);

                // and draw the player
                Player.Draw(_mapConsole, ToadMap);

                // and draw the console
                MessageLog.Draw(_messageConsole);

                // and draw stats
                Player.DrawStats(_statConsole);

                // Blit the sub consoles to the root console in the correct locations
                RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight,
              _rootConsole, 0, _inventoryHeight);
                RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight,
                  _rootConsole, _mapWidth, 0);
                RLConsole.Blit(_messageConsole, 0, 0, _messageWidth, _messageHeight,
                  _rootConsole, 0, _screenHeight - _messageHeight);
                RLConsole.Blit(_inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight,
                  _rootConsole, 0, 0);

                // Tell RLNET to draw the console
                _rootConsole.Draw();

                _renderRequired = false;
            }
        }
    }
}