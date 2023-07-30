using RaveningToad;
using RogueSharp;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using The_Ravening_Toad.Monsters;

namespace The_Ravening_Toad.Systems
{
    public class Load
    {
        public bool loading = false;
        private int count = 0;
        public List<Door> Doors { get; set; }
        public List<Monster> Monsters { get; set; }
        public List<bool> CellExplored { get; set; }
        public Player player;

        public Load() 
        {
            Doors = new List<Door>();
            Monsters = new List<Monster>();
            CellExplored = new List<bool>();
        }

        public bool loadGame(Player playerparam, int selection)
        {
            loading = true;
            player = playerparam;
            string filename = "Save Files\\Save" + selection + ".txt";
            using (StreamReader reader = new StreamReader(filename))
            {
                if (bool.Parse(reader.ReadLine()))
                {
                    player.Name = reader.ReadLine();
                    player.Attack = int.Parse(reader.ReadLine());
                    player.AttackChance = int.Parse(reader.ReadLine());
                    player.Awareness = int.Parse(reader.ReadLine());
                    player.Defense = int.Parse(reader.ReadLine());
                    player.DefenseChance = int.Parse(reader.ReadLine());
                    player.Meat = int.Parse(reader.ReadLine());
                    player.Health = int.Parse(reader.ReadLine());
                    player.MaxHealth = int.Parse(reader.ReadLine());
                    player.X = int.Parse(reader.ReadLine());
                    player.Y = int.Parse(reader.ReadLine());

                    Game.seed = int.Parse(reader.ReadLine());
                    Game.Random = new DotNetRandom(Game.seed);
                    count = int.Parse(reader.ReadLine());
                    for (int i = 0; i < count; ++i)
                    {
                        var door = new Door
                        {
                            X = int.Parse(reader.ReadLine()),
                            Y = int.Parse(reader.ReadLine()),
                            IsOpen = bool.Parse(reader.ReadLine())
                        };
                        Doors.Add(door);
                    }
                    count = int.Parse(reader.ReadLine());
                    for (int i = 0; i < count; ++i)
                    {
                        string name = reader.ReadLine();
                        switch (name)
                        {
                            case ("Filth Rat"):
                                var monster = FilthRat.Create();
                                monster.Health = int.Parse(reader.ReadLine());
                                monster.X = int.Parse(reader.ReadLine());
                                monster.Y = int.Parse(reader.ReadLine());
                                Monsters.Add(monster);
                                break;
                        }
                        
                    }

                    MapGenerator mapGenerator = new MapGenerator(Game.mapWidth, Game.mapHeight, 20, 13, 7, Game.mapLevel);
                    Game.ToadMap = mapGenerator.CreateMap();
                    Game.ToadMap.UpdatePlayerFieldOfView();
                    foreach (Cell cell in Game.ToadMap.GetAllCells())
                    {
                        Game.ToadMap.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, bool.Parse(reader.ReadLine()));
                    }
                }
                else
                {
                    loading = false;
                    return false;
                }
            }
            loading = false;
            return true;
        }
    }
}
