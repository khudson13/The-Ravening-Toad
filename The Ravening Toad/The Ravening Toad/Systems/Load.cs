using RaveningToad;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Systems
{
    public class Load
    {
        public void loadGame(Player player)
        {
            using (StreamReader reader = new StreamReader("Save Files\\Save1.txt"))
            {
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
                MapGenerator mapGenerator = new MapGenerator(Game.mapWidth, Game.mapHeight, 20, 13, 7, Game.mapLevel);
                Game.ToadMap = mapGenerator.CreateMap();
                Game.ToadMap.UpdatePlayerFieldOfView();
            }
        }
    }
}
