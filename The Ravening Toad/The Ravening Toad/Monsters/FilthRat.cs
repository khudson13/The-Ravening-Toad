using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using RogueSharp.DiceNotation;

namespace The_Ravening_Toad.Monsters
{
    public class FilthRat : Monster
    {
        public static FilthRat Create()
        {
            int health = Dice.Roll("2D5");
            return new FilthRat
            {
                Attack = Dice.Roll("1D3"),
                AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.FilthRatColor,
                Defense = Dice.Roll("1D3"),
                DefenseChance = Dice.Roll("10D4"),
                Meat = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Filth Rat",
                Speed = 14,
                Symbol = 'r'
            };
        }
    }
}
