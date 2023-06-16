﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}