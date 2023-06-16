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
        // Draw called on map update
        // Renders all symbols/colors for each cell on map sub console
        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
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
