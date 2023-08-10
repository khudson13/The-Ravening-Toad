using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Interfaces;

namespace The_Ravening_Toad.Recipes
{
    public class MeatWad : IRecipe
    {
        private readonly string _name = "Wad of Raw Meat";  // name of recipe
        private readonly int _meat = 6;                     // required meat for cooking
        private readonly string _stove = "none";            // no stove required
        private readonly string _oven = "none";             // no oven required

        public string name
        {
            get
            {
                return _name;
            }
        }

        public int meat
        {
            get
            {
                return _meat;
            }
        }

        public string stove
        {
            get
            {
                return _stove;
            }
        }

        public string oven
        {
            get
            {
                return _oven;
            }
        }
    }
}
