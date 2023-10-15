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
        public void Draw(RLConsole console)
        {
            /*
            console.Print(13, 10, "THE RAVENING TOAD", RLColor.Yellow);
            console.Print(15, 15, "1 = new game", RLColor.Yellow);
            console.Print(15, 16, "2 = load game", RLColor.Yellow);
            console.Print(15, 17, "3 = delete game", RLColor.Yellow);
            console.Print(15, 18, "4 = exit", RLColor.Yellow);
            */

            //console.Print(30, 25, "-----", RLColor.Green);
            //console.Print(70, 25, "-----", RLColor.Green);
 //                                                                                                                                                  *
console.Print(0, 0, 
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 1, 
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 2, 
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 3, 
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 4,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 5,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 6,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 7,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 8,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 9,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 10,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 11,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 12,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 13,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 14,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 15,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 16,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 17,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 18,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 19,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 20,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 21,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 22,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 23,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 24,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 25,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 26,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 27,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 28,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 29,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 30,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 31,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 32,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 33,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 34,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 35,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 36,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 37,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 38,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 39,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 40,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 41,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 42,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 43,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 44,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 45,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 46,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 47,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 48,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 49,
"*----------------------------------------------------------------------------------------------------------------------------------------------------*", RLColor.Green); console.Print(0, 50,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 51,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 52,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 53,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 54,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 55,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 56,
"*                                                                   --------                                                                         *", RLColor.Green); console.Print(0, 57,
"*                                                              ----          ----                                                                    *", RLColor.Green); console.Print(0, 58,
"*                                                         ----                    ----                                                               *", RLColor.Green); console.Print(0, 59,
"*                                                     ----          --------           ----                                                          *", RLColor.Green); console.Print(0, 60,
"*                                                 ----         ----          ----          ----                                                      *", RLColor.Green); console.Print(0, 61,
"*                                             ----        ----                    ----         ----                                                  *", RLColor.Green); console.Print(0, 62,
"*                                         ---         ----                             ----        ---                                               *", RLColor.Green); console.Print(0, 63,
"*                                       --        ----                                     ----       --                                             *", RLColor.Green); console.Print(0, 64,
"*                                      --      ----                                          ----      --                                            *", RLColor.Green); console.Print(0, 65,
"*                                     --     ---                                                ---     --                                           *", RLColor.Green); console.Print(0, 66,
"*                                     --                                                                --                                           *", RLColor.Green); console.Print(0, 67,
"*                                    --                                                                  --                                          *", RLColor.Green); console.Print(0, 68,
"*                                   --                                                                    --                                         *", RLColor.Green); console.Print(0, 69,
"*                                   --                                                                    --                                         *", RLColor.Green); console.Print(0, 70,
"*                                    --                                                                  --                                          *", RLColor.Green); console.Print(0, 71,
"*                                     --                                                                --                                           *", RLColor.Green); console.Print(0, 72,
"*                                     --                                                                --                                           *", RLColor.Green); console.Print(0, 73,
"*                                      --                                                              --                                            *", RLColor.Green); console.Print(0, 74,
"*                                       --                                                            --                                             *", RLColor.Green); console.Print(0, 75,
"*                                         ---                                                      ---                                               *", RLColor.Green); console.Print(0, 76,
"*                                            ----                                              ----                                                  *", RLColor.Green); console.Print(0, 77,
"*                                                ----                                      ----                                                      *", RLColor.Green); console.Print(0, 78,
"*                                                    ----                              ----                                                          *", RLColor.Green); console.Print(0, 79,
"*                                                         ----                    ----                                                               *", RLColor.Green); console.Print(0, 80,
"*                                                              ----          ----                                                                    *", RLColor.Green); console.Print(0, 81,
"*                                                                   --------                                                                         *", RLColor.Green); console.Print(0, 82,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 83,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 84,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 85,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 86,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 87,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 88,
"*                                                                                                                                                    *", RLColor.Green); console.Print(0, 89,
"*                                                                                                                                                    *", RLColor.Green); 

        }
    }
}
