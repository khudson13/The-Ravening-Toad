using RaveningToad;
using RLNET;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Here be the player object
// Find all player data here

// player inventory stored in ItemsMenu

namespace The_Ravening_Toad.Core
{
    public class Player : Actor
    {
        public List<Monster> visible_monsters = new List<Monster>();

        public Player()
        {
            Attack = 20;
            AttackChance = 50;
            damage = 2;
            Awareness = 15;
            Color = Colors.Player;
            Defense = 20;
            DefenseChance = 40;
            Meat = 0;
            Cash = 0;
            Health = 100;
            MaxHealth = 100;
            Name = "The Toad";
            Speed = 10;
            Symbol = '@';
        }

        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:    {Name}", Colors.Text);
            statConsole.Print(1, 3, $"Health:  {Health}/{MaxHealth}", Colors.Text);
            statConsole.Print(1, 5, $"Attack:  {Attack}", Colors.Text);
            statConsole.Print(1, 7, $"Defense: {Defense}", Colors.Text);
            statConsole.Print(1, 9, $"Meat:    {Meat}", Colors.Text);
            statConsole.Print(1, 11, $"Cash:    {Cash}", Colors.Text);
        }
    }
}
