using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;
using RogueSharp.DiceNotation;

namespace The_Ravening_Toad.Monsters
{
    public class DirtyRat : Monster
    {
        public static DirtyRat Create()
        {
            int health = Dice.Roll("1D5");
            return new DirtyRat
            {
                Attack = 10,
                AttackChance = Dice.Roll("25D3"),
                damage = 1,
                Awareness = 10,
                Color = Colors.FilthRatColor,
                Defense = 12,
                DefenseChance = Dice.Roll("10D4"),
                Meat = 1,
                Health = health,
                MaxHealth = health,
                Name = "Dirty Rat",
                Speed = 14,
                Symbol = 'r'
            };
        }
    }
}
