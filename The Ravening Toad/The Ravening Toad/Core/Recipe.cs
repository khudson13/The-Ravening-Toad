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
        protected string _name;
        protected List<Ingredient> _ingredients;    // add an ingredient muliple times if more than one needed
        protected int _value;
        protected int _meat;
        protected string _stove;
        protected string _oven;
        protected string _description;
        protected int _readyToMake;
        protected int _onHand;

        public string Name
        {
            get { return _name; }
        }

        public List<Ingredient> Ingredients
        {
            get { return _ingredients; }
        }

        public int Value
        {
            get { return _value; }
        }

        public int Meat
        {
            get { return _meat; }
        }

        public string Stove
        {
            get { return _stove; }
        }

        public string Oven
        {
            get { return _oven; }
        }

        public string Description
        {
            get { return _description; }
        }

        public int ReadyToMake
        {
            get { return _readyToMake; }
            set
            {
                if (value < 0)
                {
                    _readyToMake = 0;
                }
                else
                {
                    _readyToMake = value;
                }
            }
        }
    }
}
