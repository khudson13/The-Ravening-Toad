using RaveningToad;
using RLNET;
using RogueSharp;
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

        // **READYTOMAKE AND READYTOSERVE ARE BEING MIGRATED INTO THEIR SPECIFIC RECIPE FILES, TO BE UPDATED WHEN RETURNING TO CAFE OR COOKING**

        public List<Recipe> recipes;            // known recipes
        public List<Recipe> viableRecipes;      // recipes which can be made right now
        public List<int> readytoserve;          // how many of a given recipe already on hand, indexes matched to viableRecipes
        public List<int> readytomake;           // like readytoserve,but how many can be made with ingredients on hand
        private int[] ingredients;              // Stores ingredient quantities, indexed by ingredients enum.

        public ToadCafe()
        {
            recipes = new List<Recipe>();
            viableRecipes = new List<Recipe>();
            readytoserve = new List<int>();
            readytomake = new List<int>();
            ingredients = new int[(int)Ingredient.NULL];

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
                        console.Print(13, Y, "You have " + Game.Player.Meat + " fresh meat", RLColor.Yellow);
                        ++Y;
                        break;
                    case 1:
                        console.Print(13, Y, "You have " + tables + " tables available, seating 2 customers each", RLColor.Yellow);
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
            ++Y;
            int count = 2;
            foreach (var meal in recipes)
            {
                if (IsViableRecipe(meal))
                {
                    viableRecipes.Add(meal);
                    readytomake.Add(meal.Meat / Game.Player.Meat);
                    readytoserve.Add(0);
                    console.Print(13, Y, count + "= prepare " + meal.Name + "(" + Game.Player.Meat / viableRecipes.Last().Meat + ")", RLColor.Yellow);
                    ++Y;
                    ++count;
                }
            }
        }

        public bool SellMostValuable()
        {
            int index = 0;      // index of most valuable meal on hand

            // outta food
            int count = 0;
            foreach (int x in readytoserve)
            {
                if (x > 0)
                {
                    ++count;
                }
            }
            if (count == 0)
            {
                return false;
            }

            // find most valuable food
            for (int i = 0; i < viableRecipes.Count; ++i)
            {
                if (readytoserve[i] > 0 && viableRecipes[i].Value > viableRecipes[index].Value)
                {
                    index = i;
                } 
            }

            // pay the player
            Game.Player.Cash += viableRecipes[index].Value;

            // sell the food
            Game.MessageLog.Add($"{viableRecipes[index].Name} sold for {viableRecipes[index].Value} gold");
            --readytoserve[index];
            if (readytoserve[index] == 0)
            {
                readytoserve.RemoveAt(index);
                viableRecipes.RemoveAt(index);
            }

            
            return true;
        }

        private bool IsViableRecipe(IRecipe recipe)
        {
            if (recipe == null) return false;
            if (recipe.Meat > Game.Player.Meat) return false;
            if (recipe.Stove != stove) return false;
            if (recipe.Oven != oven) return false;

            return true;
        }
    }
}
