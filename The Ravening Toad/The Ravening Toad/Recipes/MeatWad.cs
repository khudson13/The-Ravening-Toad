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
            name = "Wad of Raw Meat";  // name of recipe
            value = 1;                 // profit from sale
            meat = 6;                  // required meat for cooking
            stove = "none";            // no stove required
            oven = "none";             // no oven required
        }
    }
}
