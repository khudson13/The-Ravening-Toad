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
        private bool _mainmenu = false;
        private bool _savemenu = false;
        private bool _loadmenu = false;
        private bool _pause = true;
        private string _location = "start";
        private int[] _inventory = new int[(int)ItemID.END_USABLE]; // indexed by ItemID enum, each entry is the number of that item owned
        public List<Monster> visible_monsters = new List<Monster>();

        public void AddItem(ItemID ID, int amount)
        {
            _inventory[(int)ID] += amount;
        }
        public void SubtractItem(ItemID ID, int amount = 1)
        {
            if (_inventory[(int)ID] >= amount)
            {
                _inventory[(int)ID] -= amount;
            }
        }
        public int GetItemAmmount(ItemID ID)
        {
            return _inventory[(int)ID];
        }
        public int[] GetInventory() { return _inventory; }

        public bool Mainmenu
        {
            get{ return _mainmenu; }
            set{ _mainmenu = value; }
        }

        public bool Savemenu
        {
            get{ return _savemenu; }
            set{ _savemenu = value; }
        }

        public bool Loadmenu
        {
            get{ return _loadmenu; }
            set{ _loadmenu = value; }
        }

        public bool Pause
        {
            get{ return _pause; }
            set{ _pause = value; }
        }

        public string Location
        {
            get{ return _location; }
            set{ _location = value; }
        }

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

            // temporary starting inventory for testing
            for (int i = 0; i < _inventory.Length; ++i)
            {
                _inventory[i] = 1;
            }
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
