using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Interfaces;

namespace The_Ravening_Toad.Core
{
    public abstract class Item : IItem, IDrawable
    {
        //********
        // IItem *
        //********

        // vars
        protected string _name;
        protected bool _consumable;
        protected bool _throwable;
        protected ItemType _ItemType;
        protected ItemID _ID;
        protected int _owned = 0;

        // getters
        public string Name { get { return _name; } }
        public ItemType ItemType { get { return _ItemType; } }
        public ItemID ItemID { get { return _ID; } }
        public int Owned
        {
            get { return _owned; }
            set
            {
                if (value >= 0)
                {
                    _owned = value;
                }
            }
        }
        // methods
        public Item() { }
        public virtual void Activate() { }

        //************
        // IDrawable *
        //************

        // vars
        private RLColor _color;
        private char _symbol;
        private int _X;
        private int _Y;

        // getters & setters
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
