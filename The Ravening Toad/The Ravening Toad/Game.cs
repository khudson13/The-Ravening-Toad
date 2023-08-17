using RLNET;
using RogueSharp.Random;
using System;
using System.Data;
using System.IO;
using The_Ravening_Toad.Core;
using The_Ravening_Toad.Interfaces;
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
        public static readonly int mapWidth = 80;
        public static readonly int mapHeight = 48;
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

        public static int mapLevel = 1;

        //************************
        // DEFINE BASIC ELEMENTS *
        //************************
        public static Player Player { get; set; }
        public static ToadMap ToadMap { get; set; }

        public static CommandSystem CommandSystem { get; set; }
        public static MenuControls MenuControls { get; set; }

        public static MessageLog MessageLog { get; private set; }
        public static MainMenu MainMenu { get; private set; }
        public static SaveMenu SaveMenu { get; private set; }
        public static LoadMenu LoadMenu { get; private set; }
        public static StartScreen StartScreen { get; private set; }
        public static ToadCafe ToadCafe { get; private set; }

        public static SchedulingSystem SchedulingSystem { get; private set; }

        public static Save Save { get; private set; }
        public static Load Load { get; private set; }

        // Singleton of IRandom used throughout the game when generating random numbers
        public static int seed { get; set; }
        public static IRandom Random { get; set; }

        private static bool _renderRequired = true;


        //*************
        // BEGIN MAIN *
        //*************
        public static void Main()
        {
            // Store font file in variable for easy changing later
            string fontFileName = "terminal8x8.png";

            // Generate seed for random number generator using time
            seed = (int)DateTime.UtcNow.Ticks;
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
            _mapConsole = new RLConsole(mapWidth, mapHeight);
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
            MessageLog.Add($"Map - Level {mapLevel} -  created with seed '{seed}'");

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

            // Prepare menus
            StartScreen = new StartScreen();
            MainMenu = new MainMenu();
            SaveMenu = new SaveMenu();
            LoadMenu = new LoadMenu();

            // Create Save/Load Systems
            Save = new Save();
            Load = new Load();

            // Create map
            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight, 20, 13, 7, mapLevel);
            ToadMap = mapGenerator.CreateMap();
            ToadMap.UpdatePlayerFieldOfView();
            ToadCafe = new ToadCafe();

            // Create Command System
            CommandSystem = new CommandSystem();
            MenuControls = new MenuControls();

            //**************
            // AND ACTION! *
            //**************
            // Begin RLNET's game loop
            _rootConsole.Run();
        }

        // exit game
        public static void ExitGame()
        {
            _rootConsole.Close();
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
                // primary player controls
                if (keyPress != null && !Player.pause)
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
                            seed = (int)DateTime.UtcNow.Ticks;
                            Random = new DotNetRandom(seed);
                            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight, 20, 13, 7, mapLevel);
                            ToadMap.Clear();
                            ToadMap = mapGenerator.CreateMap();
                            MessageLog = new MessageLog();
                            CommandSystem = new CommandSystem();
                            Player.pause = true;
                            Player.location = "cafe";
                            MessageLog.Add($"{Player.Name} has returned to the cafe");
                        }
                    }
                    else if (keyPress.Key == RLKey.Escape)
                    {
                        Player.pause = true;
                        Player.mainmenu = true;
                        _renderRequired = true;
                    }
                }
                // menu input
                else if (Player.pause)
                {
                    if (keyPress != null)
                    {
                        MenuControls.MenuControl(keyPress.Key);
                        _renderRequired = true;
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
                _inventoryConsole.Clear();

                // Draw the map
                if (Player.location == "dungeon")
                {
                    ToadMap.Draw(_mapConsole, _statConsole);

                    // and draw the player
                    Player.Draw(_mapConsole, ToadMap);
                }
                else if (Player.location == "start")
                {
                    StartScreen.Draw(_mapConsole);
                }
                else if (Player.location == "cafe")
                {
                    ToadCafe.Draw(_mapConsole);
                }

                // and draw the console
                MessageLog.Draw(_messageConsole);

                // and draw stats
                Player.DrawStats(_statConsole);

                // if main menu open, then draw it
                if (Player.mainmenu)
                {
                    MainMenu.Draw(_inventoryConsole);
                }
                // if save menu open, then draw it
                if (Player.savemenu)
                {
                    SaveMenu.Draw(_inventoryConsole);
                }
                // if load menu open, then draw it
                if (Player.loadmenu)
                {
                    LoadMenu.Draw(_inventoryConsole);
                }

                // Blit the sub consoles to the root console in the correct locations
                RLConsole.Blit(_mapConsole, 0, 0, mapWidth, mapHeight,
                  _rootConsole, 0, _inventoryHeight);
                RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight,
                  _rootConsole, mapWidth, 0);
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