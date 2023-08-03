using RLNET;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Here be the player object

namespace The_Ravening_Toad.Core
{
    public class Player : Actor
    {
        private bool _mainmenu = true;
        private bool _savemenu = false;
        private bool _loadmenu = false;
        private bool _pause = true;
        private string _location = "";

        public bool mainmenu
        {
            get
            {
                return _mainmenu;
            }
            set
            {
                _mainmenu = value;
            }
        }

        public bool savemenu
        {
            get
            {
                return _savemenu;
            }
            set
            {
                _savemenu = value;
            }
        }

        public bool loadmenu
        {
            get
            {
                return _loadmenu;
            }
            set
            {
                _loadmenu = value;
            }
        }

        public bool pause
        {
            get
            {
                return _pause;
            }
            set
            {
                _pause = value;
            }
        }

        public string location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        public Player()
        {
            Attack = 2;
            AttackChance = 50;
            Awareness = 15;
            Color = Colors.Player;
            Defense = 2;
            DefenseChance = 40;
            Meat = 0;
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
            statConsole.Print(1, 5, $"Attack:  {Attack} ({AttackChance}%)", Colors.Text);
            statConsole.Print(1, 7, $"Defense: {Defense} ({DefenseChance}%)", Colors.Text);
            statConsole.Print(1, 9, $"Meat:    {Meat}", Colors.Gold);
        }
    }
}
