using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Items
{
    // THIS IS ONLY FOR COPY/PASTE WHEN ADDING ITEMS
    // DO NOT INSTANTIATE
    internal class _0_ITEM_TEMPLATE
    {
        // vars
        static protected string _name;
        static protected bool _consumable;
        static protected bool _throwable;
        static protected ItemType _ItemType;
        static protected ItemID _ID;
        private RLColor _color;
        private char _symbol;
        private int _X;
        private int _Y;

        // methods
        public virtual void Activate() { }

        // getters & setters
        public string Name { get { return _name; } }
        public ItemType ItemType { get { return _ItemType; } }
        public ItemID ItemID { get { return _ID; } }
        public RLColor Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public char Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }
        public int X
        {
            get { return _X; }
            set { _X = value; }
        }
        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        // methods
        public void Draw(RLConsole console, IMap map)
        {
            // Don't draw actors in cells that haven't been explored
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }

            // Only draw the actor with the color and symbol when they are in field-of-view
            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                // When not in field-of-view just draw a normal floor
                //console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
                return;
            }
        }
    }
}
