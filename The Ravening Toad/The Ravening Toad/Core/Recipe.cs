using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Interfaces;

namespace The_Ravening_Toad.Core
{
    public class Recipe : IRecipe
    {
        private string _name;
        private int _value;
        private int _meat;
        private string _stove;
        private string _oven;

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public int meat
        {
            get
            {
                return _meat;
            }
            set
            {
                _meat = value;
            }
        }

        public string stove
        {
            get
            {
                return _stove;
            }
            set
            {
                _stove = value;
            }
        }

        public string oven
        {
            get
            {
                return _oven;
            }
            set
            {
                _oven = value;
            }
        }
    }
}
