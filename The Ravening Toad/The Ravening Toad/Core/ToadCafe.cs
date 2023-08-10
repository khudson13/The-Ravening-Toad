using RaveningToad;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Interfaces;
using The_Ravening_Toad.Recipes;

namespace The_Ravening_Toad.Core
{
    public class ToadCafe
    {
        public int tables = 1;                  // how many tables in the cafe
        public string stove = "none";           // type of cooking surface
        public string oven = "none";            // type of oven
        public string storage = "none";         // ingredient storage
        public List<IRecipe> recipes;    
        
        public ToadCafe()
        {
            recipes.Add(new MeatWad());
        }

        public void Draw(RLConsole console)
        {
            int Y = 10;
            console.Print(13, Y, "THE TOAD CAFE", RLColor.Yellow);
            Y += 2;

            for (int i = 0; i < 4; ++i)
            {
                switch (i)
                {
                    case 0:
                        console.Print(13, Y, "You have" + Game.Player.Meat + "fresh meat", RLColor.Yellow);
                        ++Y;
                        break;
                    case 1:
                        console.Print(13, Y, "You have" + tables + "tables available", RLColor.Yellow);
                        ++Y;
                        break;
                    case 2:
                        console.Print(13, Y, "Your stove is: " + stove, RLColor.Yellow);
                        ++Y;
                        break;
                    case 3:
                        console.Print(13, Y, "Your oven is: " + oven, RLColor.Yellow);
                        ++Y;
                        break;
                    case 4:
                        console.Print(13, Y, "Your storage is: " + storage, RLColor.Yellow);
                        ++Y;
                        break;
                }
            }

            ++Y;
            console.Print(13, Y, "1 = sell food and return to dungeon", RLColor.Yellow);
            int count = 2;
            foreach (var meal in recipes)
            {
                if (IsViableRecipe(meal))
                {
                    console.Print(13, Y, count + "= prepare " + meal.name, RLColor.Yellow);
                    ++Y;
                    ++count;
                }
            }
        }

        private bool IsViableRecipe(IRecipe recipe)
        {
            if (recipe == null) return false;
            if (recipe.meat > Game.Player.Meat) return false;
            if (recipe.stove != stove) return false;
            if (recipe.oven != oven) return false;

            return true;
        }
    }
}
