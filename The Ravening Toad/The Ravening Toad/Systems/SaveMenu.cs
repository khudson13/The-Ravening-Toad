using RLNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ravening_Toad.Systems
{
    public class SaveMenu
    {
        public int selection = 0;
        private string[] _choices = new string[10];

        public SaveMenu()
        {
            PopulateMenu();
        }
        
        public void Draw(RLConsole console)
        {

            for (int i = 0; i < _choices.Length; i++)
            {
                if (i == selection)
                {
                    console.Print(1, i + 1, _choices[i], RLColor.Yellow);
                }
                else
                {
                    console.Print(1, i + 1, _choices[i], RLColor.White);
                }
            }
        }

        public void PopulateMenu()
        {
            string[] filename = { @"Save Files", "" };
            StreamReader reader;
            for (int i = 0; i < _choices.Length; ++i)
            {
                filename[1] = "Save" + (i) + ".txt";
                reader = new StreamReader(Path.Combine(filename));

                if (bool.Parse(reader.ReadLine()))
                {
                    _choices[i] = reader.ReadLine();
                }
                else
                {
                    _choices[i] = "File" + (i + 1);
                }
            }
        }
    }
}