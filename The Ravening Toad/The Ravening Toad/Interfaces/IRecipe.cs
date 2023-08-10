using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ravening_Toad.Interfaces
{
    public interface IRecipe
    {
        string name { get; }
        int meat { get; }
        string stove { get; }
        string oven { get; }
    }
}
