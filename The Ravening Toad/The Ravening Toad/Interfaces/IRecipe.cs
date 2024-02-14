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
        string name { get; }
        List<Ingredient> ingredients { get; }
        int value { get; }
        int meat { get; }
        string stove { get; }
        string oven { get; }
    }
}
