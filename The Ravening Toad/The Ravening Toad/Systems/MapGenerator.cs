using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaveningToad;
using RogueSharp;
using The_Ravening_Toad.Core;

/*
 * This is where we make the map, make the map, make the map
 * This is where we make the map
 * All day long
 */

namespace The_Ravening_Toad.Systems
{
    public class MapGenerator
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;

        private readonly ToadMap _map;

        // Constructor takes dimensions of map and features
        public MapGenerator(int width, int height,
        int maxRooms, int roomMaxSize, int roomMinSize)
        {
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _map = new ToadMap();
        }

        // Generate a new map that places rooms randomly
        public ToadMap CreateMap()
        {
            // Set all properties of all cells to false
            _map.Initialize(_width, _height);

            // Try to place as many rooms as the specified maxRooms
            // Note: Only using decrementing loop because of WordPress formatting
            for (int i = 0; i < _maxRooms; ++i)
            {
                // Determine the size and position of the room randomly
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomXPosition = Game.Random.Next(0, _width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, _height - roomHeight - 1);

                // All rooms are rectangles
                var newRoom = new Rectangle(roomXPosition, roomYPosition,
                  roomWidth, roomHeight);

                // Check new room for intersection with other rooms
                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));

                // If no intersection, add to list
                if (!newRoomIntersects)
                {
                    _map.Rooms.Add(newRoom);
                }
            }
            // Iterate through each room that we wanted placed 
            // call CreateRoom to make it
            foreach (Rectangle room in _map.Rooms)
            {
                CreateRoom(room);
            }

            // Iterate through rooms, skip first room (rooms link to previous rooms)
            for (int r = 1; r < _map.Rooms.Count; r++)
            {
                // Find the center of this room and the previous room
                int previousRoomCenterX = _map.Rooms[r - 1].Center.X;
                int previousRoomCenterY = _map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = _map.Rooms[r].Center.X;
                int currentRoomCenterY = _map.Rooms[r].Center.Y;

                // Halls are 'L' shaped, randomly select one of two possible 'L's
                if (Game.Random.Next(1, 2) == 1)
                {
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                }
                else
                {
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                }
            }

            // put the player in the center of the first room
            PlacePlayer();

            return _map;
        }

        // Given a rectangular area on the map
        // set the cell properties for that area to true
        private void CreateRoom(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right; ++x)
            {
                for (int y = room.Top + 1; y < room.Bottom; ++y)
                {
                    _map.SetCellProperties(x, y, true, true, false);
                }
            }
        }

        // Create tunnel parallel to the x-axis
        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); ++x)
            {
                _map.SetCellProperties(x, yPosition, true, true);
            }
        }

        // Create tunnel parallel to the y-axis
        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); ++y)
            {
                _map.SetCellProperties(xPosition, y, true, true);
            }
        }

        // Find the center of the first room and place Player
        private void PlacePlayer()
        {
            Player player = Game.Player;
            if (player == null)
            {
                player = new Player();
            }

            player.X = _map.Rooms[0].Center.X;
            player.Y = _map.Rooms[0].Center.Y;

            _map.AddPlayer(player);
        }
    }
}

/* I first made one big room for testing. That code is here for reference.
 * 
  public class MapGenerator
    {
        private readonly int _width;
        private readonly int _height;

        private readonly ToadMap _map;

        // constructor sets dimensions of map being created
        public MapGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            _map = new ToadMap();
        }

        // Generate new map as one big room with walls
        public ToadMap CreateMap()
        {
            // Initialize every cell in the map by setting
            // walkable, transparency, and explored to true
            _map.Initialize(_width, _height);
            foreach (Cell cell in _map.GetAllCells())
            {
                _map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }

            // Set the first and last rows in the map to not be transparent or walkable
            foreach (Cell cell in _map.GetCellsInRows(0, _height - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            // Set the first and last columns in the map to not be transparent or walkable
            foreach (Cell cell in _map.GetCellsInColumns(0, _width - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            return _map;
        }
    }
*/