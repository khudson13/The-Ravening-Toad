using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ravening_Toad.Interfaces
{
    public interface IRecipe
    {
        string name { get; set; }
        int value { get; set; }
        int meat { get;  set; }
        string stove { get; set; }
        string oven { get; set; }
    }
}
