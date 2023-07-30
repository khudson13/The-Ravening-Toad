using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Core;

namespace The_Ravening_Toad.Systems
{
    public class StartScreen
    {
        private bool _active = true;

        public bool active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }
        public void Draw(RLConsole console)
        {
            console.Set(20, 50, RLColor.Red, RLColor.Brown, 'T');
        }
    }
}
