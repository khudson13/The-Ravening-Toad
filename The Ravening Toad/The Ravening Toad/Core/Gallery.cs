using OpenTK.Graphics.ES11;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ravening_Toad.Core
{
    public class Gallery : Map
    {
        private string _filename = "";
        private RLRootConsole _frame;
        private RLConsole _picture;

        public void setFilename(string filename)
        {
            _filename = filename;
            _frame = new RLRootConsole(_filename, 100, 70, 16, 64, 1f, "picature");
            _frame.SetChar(0, 1, 1);
        }

        public void Draw()
        {
            _frame.Draw();
        }
    }
}
