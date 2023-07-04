using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaveningToad;
using RLNET;
using RogueSharp;

// Define map properties

namespace The_Ravening_Toad.Core
{
    // ToadMap extends the RogueSharp Map class
    public class ToadMap : Map
    {
        // List of rooms (rectangles)
        public List<Rectangle> Rooms;

        // List of doors
        public List<Door> Doors { get; set; }

        // List of monsters
        private readonly List<Monster> _monsters;

        public ToadMap()
        {
            // Initialize all lists for new dungeon
            Rooms = new List<Rectangle>();
            Doors = new List<Door>();
            _monsters = new List<Monster>();
        }

        public void AddMonster(Monster monster)
        {
            _monsters.Add(monster);
            // set monster location not walkable
            SetIsWalkable(monster.X, monster.Y, false);
            Game.SchedulingSystem.Add(monster);
        }

        // Called by MapGenerator, adds player to first room generated
        public void AddPlayer(Player player)
        {
            Game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFieldOfView();
            Game.SchedulingSystem.Add(player);
        }

        // Iterate through each Cell in the room and return true if any are walkable
        public bool DoesRoomHaveWalkableSpace(Rectangle room)
        {
            for (int x = 1; x <= room.Width - 2; x++)
            {
                for (int y = 1; y <= room.Height - 2; y++)
                {
                    if (IsWalkable(x + room.X, y + room.Y))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Draw called on map update
        // Renders all symbols/colors for each cell on map sub console
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }

            foreach (Door door in Doors)
            {
                door.Draw(mapConsole, this);
            }

            // draw monsters
            // Index is position in stat window
            int i = 0;

            // Iterate through and draw monsters
            foreach (Monster monster in _monsters)
            {
                monster.Draw(mapConsole, this);
                // When monster in FoV also draw their stats
                if (IsInFov(monster.X, monster.Y))
                {
                    // Pass index to DrawStats and increment it afterwards
                    monster.DrawStats(statConsole, i);
                    i++;
                }
            }
        }

        // Return door or null if not found.
        public Door GetDoor(int x, int y)
        {
            return Doors.SingleOrDefault(d => d.X == x && d.Y == y);
        }

        public Monster GetMonsterAt(int x, int y)
        {
            return _monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        // Look for a random location in the room that is walkable.
        public Point GetRandomWalkableLocationInRoom(Rectangle room)
        {
            if (DoesRoomHaveWalkableSpace(room))
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = Game.Random.Next(1, room.Width - 2) + room.X;
                    int y = Game.Random.Next(1, room.Height - 2) + room.Y;
                    if (IsWalkable(x, y))
                    {
                        return new Point(x, y);
                    }
                }
            }

            // If no walkable location return null
            return new Point(-1, -1);
        }

        // Actor opens door
        private void OpenDoor(Actor actor, int x, int y)
        {
            Door door = GetDoor(x, y);
            if (door != null && !door.IsOpen)
            {
                door.IsOpen = true;
                var cell = GetCell(x, y);
                // Once opened mark as transparent and don't block FoV
                SetCellProperties(x, y, true, cell.IsWalkable, cell.IsExplored);

                Game.MessageLog.Add($"{actor.Name} opened a door");
            }
        }

        public void RemoveMonster(Monster monster)
        {
            _monsters.Remove(monster);
            SetIsWalkable(monster.X, monster.Y, true);
            Game.SchedulingSystem.Remove(monster);
        }

        // Returns true when able to place the Actor on the cell or false otherwise
        public bool SetActorPosition(Actor actor, int x, int y)
        {
            // Only allow actor placement if the cell is walkable
            if (GetCell(x, y).IsWalkable)
            {
                // The cell the actor was previously on is now walkable
                SetIsWalkable(actor.X, actor.Y, true);
                // Update the actor's position
                actor.X = x;
                actor.Y = y;
                // The new cell the actor is on is now not walkable
                SetIsWalkable(actor.X, actor.Y, false);
                // Try to open a door if one exists here
                OpenDoor(actor, x, y);
                // If Player, reposition field-of-view
                if (actor is Player)
                {
                    UpdatePlayerFieldOfView();
                }
                return true;
            }
            return false;
        }

        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            // Don't draw unexplored cells
            if (!cell.IsExplored)
            {
                return;
            }

            // Draw walls and floors
            // Walkable = floor, not = wall - '.' for floor and '#' for walls
            // Lighter in field of view
            if (IsInFov(cell.X, cell.Y))
            {
                
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
            // Darker outside field of view
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }

        // A helper method for setting the IsWalkable property on a Cell
        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            ICell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        // When Player moves, update field-of-view
        public void UpdatePlayerFieldOfView()
        {
            Player player = Game.Player;
            // Compute the field-of-view based on the player's location and awareness
            ComputeFov(player.X, player.Y, player.Awareness, true);
            // Mark all cells in field-of-view as having been explored
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }
    }
}
