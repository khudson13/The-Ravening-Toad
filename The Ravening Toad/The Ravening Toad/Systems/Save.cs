using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using System.Collections;
using RaveningToad;
using RogueSharp;

namespace The_Ravening_Toad.Systems
{
    public class Save
    {
        public void saveGame(Player player)
        {

            using (StreamWriter writer = new StreamWriter("Save Files\\Save1.txt"))
            {

                writer.WriteLine(true);
                writer.WriteLine(player.Attack);
                writer.WriteLine(player.AttackChance);
                writer.WriteLine(player.Awareness);
                writer.WriteLine(player.Defense);
                writer.WriteLine(player.DefenseChance);
                writer.WriteLine(player.Meat);
                writer.WriteLine(player.Health);
                writer.WriteLine(player.MaxHealth);
                writer.WriteLine(player.X);
                writer.WriteLine(player.Y);
                
                writer.WriteLine(Game.seed);
                writer.WriteLine(Game.ToadMap.Doors.Count);
                for (int i = 0; i < Game.ToadMap.Doors.Count; ++i)
                {
                    writer.WriteLine(Game.ToadMap.Doors[i].X);
                    writer.WriteLine(Game.ToadMap.Doors[i].Y);
                    writer.WriteLine(Game.ToadMap.Doors[i].IsOpen);
                }
                writer.WriteLine(Game.ToadMap.monsters.Count);
                for (int i = 0; i < Game.ToadMap.monsters.Count; ++i)
                {
                    writer.WriteLine(Game.ToadMap.monsters[i].Name);
                    writer.WriteLine(Game.ToadMap.monsters[i].Health);
                    writer.WriteLine(Game.ToadMap.monsters[i].X);
                    writer.WriteLine(Game.ToadMap.monsters[i].Y);
                }
                foreach (Cell cell in Game.ToadMap.GetAllCells())
                {
                    writer.WriteLine(cell.IsExplored);
                }

            }
        }
    }
}