using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ravening_Toad.Behaviors;
using The_Ravening_Toad.Systems;

namespace The_Ravening_Toad.Core
{
    public class Monster : Actor
    {

        public int? TurnsAlerted { get; set; }

        public void DrawStats(RLConsole statConsole, int position)
        {
            // Y=13 is below the player stats.
            // Multiply by 2 to leave space
            int yPosition = 13 + (position * 2);

            // Print monster symbol
            statConsole.Print(1, yPosition, Symbol.ToString(), Color);

            // Health bar width = current health / max health
            int width = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0);
            int remainingWidth = 16 - width;

            // Use colors to indicate damage
            statConsole.SetBackColor(3, yPosition, width, 1, Palette.Primary);
            statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, Palette.PrimaryDarkest);

            // Print the monsters name above health bar
            statConsole.Print(2, yPosition, $": {Name}", Palette.Light);
        }

        public virtual void PerformAction(CommandSystem commandSystem)
        {
            var behavior = new StandardMoveAndAttack();
            behavior.Act(this, commandSystem);
        }
    }
}
