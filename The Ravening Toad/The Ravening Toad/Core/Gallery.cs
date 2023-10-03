using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ravening_Toad.Core
{
    public class Gallery
    {
        private string _filename = "";
        private RLRootConsole _frame;
        private  RLConsole _picture;
        private Map _image;

        public void setFilename(string filename)
        {
            _filename = filename;
            _frame = new RLRootConsole(_filename, 100, 70, 16, 64, 1f, "picature");
            _image = new Map();
            _image.Initialize(16, 64);
            
        }
    }
}
