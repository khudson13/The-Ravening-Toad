using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ravening_Toad.Systems
{
    public class Save
    {
        public void saveGame()
        {
            string[] textLines2 = { "Testing",
                             "Testing" };

            using (StreamWriter writer = new StreamWriter("Save Files\\Save1.txt"))
            {
                foreach (string ln in textLines2)
                {
                    writer.WriteLine(ln);
                }
            }
        }
    }
}

//C: \Users\Kendall\Desktop\The-Ravening-Toad\The Ravening Toad\The Ravening Toad\Save Files\Save1.txt