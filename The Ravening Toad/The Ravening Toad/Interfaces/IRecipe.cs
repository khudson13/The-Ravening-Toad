using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Interfaces
{
    public interface IRecipe
    {
        string Name { get; }
        List<Ingredient> Ingredients { get; }
        int Value { get; }
        int Meat { get; }
        string Stove { get; }
        string Oven { get; }
        int ReadyToMake { get; set; }   // only used in cafe
        int OnHand {  get; set; }       // only used in cafe
    }
}
