using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using System.Collections;
using RaveningToad;

namespace The_Ravening_Toad.Systems
{
    public class Save
    {
        public void saveGame(Player player)
        {

            using (StreamWriter writer = new StreamWriter("Save Files\\Save1.txt"))
            {
                
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

            }
        }
    }
}