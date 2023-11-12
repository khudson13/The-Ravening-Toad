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
        bool consumable { get; }
        bool throwable { get; }
        ItemTypes ItemType { get; }
        void Consume();
        void Hurl();
    }
}
