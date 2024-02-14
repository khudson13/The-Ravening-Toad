using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Recipes
{
    public class MeatWad : Recipe
    {
        public MeatWad()
        {
            _name = "Wad of Raw Meat";  // name of recipe            
            _value = 1;                 // profit from sale
            _meat = 6;                  // required meat for cooking
            _stove = "none";            // no stove required
            _oven = "none";             // no oven required

            // needed ingredients
            // 3 raw rat meat
            _ingredients = new List<Ingredient>();
            for (int i = 0; i < 3; i++)
            {
                _ingredients.Add(Ingredient.Raw_Ratmeat);
            }
        }
    }
}
