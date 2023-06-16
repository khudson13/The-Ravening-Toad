﻿using RLNET;
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

        //************************
        // DEFINE BASIC ELEMENTS *
        //************************
        public static Player Player { get; private set; }
        public static ToadMap ToadMap1 { get; private set; }
        
        private static bool _renderRequired = true;

        public static CommandSystem CommandSystem { get; private set; }

        //*************
        // BEGIN MAIN *
        //*************
        public static void Main()
        {
            // Store font file in variable for easy changing later
            string fontFileName = "terminal8x8.png";

            //*****************************
            // INITIALIZE CONSOLE WINDOWS *
            // using RLNet                *
            //*****************************
            // Title for top of console
            string consoleTitle = "The Ravening Toad - Level 1";
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

            _messageConsole.SetBackColor(0, 0, _messageWidth, _messageHeight, Palette.DeepWater);
            _messageConsole.Print(1, 1, "Messages", Colors.TextHeading);

            _statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Palette.OldStone);
            _statConsole.Print(1, 1, "Stats", Colors.TextHeading);

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
            // Create Player
            Player = new Player();

            // Create map
            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight);
            ToadMap1 = mapGenerator.CreateMap();
            ToadMap1.UpdatePlayerFieldOfView();

            // Create Command System
            CommandSystem = new CommandSystem();

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

            if (keyPress != null)
            {
                if (keyPress.Key == RLKey.Up || keyPress.Key == RLKey.Keypad8)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
                }
                else if (keyPress.Key == RLKey.Down || keyPress.Key == RLKey.Keypad2)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Down);
                }
                else if (keyPress.Key == RLKey.Left || keyPress.Key == RLKey.Keypad4)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
                }
                else if (keyPress.Key == RLKey.Right || keyPress.Key == RLKey.Keypad6)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Right);
                }
                else if (keyPress.Key == RLKey.Escape)
                {
                    _rootConsole.Close();
                }
            }

            if (didPlayerAct)
            {
                _renderRequired = true;
            }
        }

        // Event handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            // Only redraw as needed
            if (_renderRequired)
            {
                // Draw the map
                ToadMap1.Draw(_mapConsole);

                // and draw the player
                Player.Draw(_mapConsole, ToadMap1);

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