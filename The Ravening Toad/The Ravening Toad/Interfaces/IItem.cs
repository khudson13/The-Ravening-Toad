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
        bool consumable { get; }
        bool throwable { get; }
        ItemType ItemType { get; }
        void Consume();
        void Hurl();
    }
}
