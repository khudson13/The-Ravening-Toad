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

        public string name
        {
            get { return _name; }
        }

        public List<Ingredient> ingredients
        {
            get { return _ingredients; }
        }

        public int value
        {
            get { return _value; }
        }

        public int meat
        {
            get { return _meat; }
        }

        public string stove
        {
            get { return _stove; }
        }

        public string oven
        {
            get { return _oven; }
        }
    }
}
