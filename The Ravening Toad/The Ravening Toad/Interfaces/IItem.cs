using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Interfaces
{
    public interface IItem
    {
        string Name { get; }
        ItemID ItemID { get; }
        ItemType ItemType { get; }
        int Owned { get; set; }
        void Activate();
    }
}
