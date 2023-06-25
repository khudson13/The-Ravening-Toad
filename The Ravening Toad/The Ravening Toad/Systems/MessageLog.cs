using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace The_Ravening_Toad.Systems
{
    public class MessageLog
    {
        // Max log based on console size
        private static readonly int _maxLines = 9;

        // Log queue
        private readonly Queue<string> _lines;

        public MessageLog()
        {
            _lines = new Queue<string>();
        }

        // Add a line to the log
        public void Add(string message)
        {
            _lines.Enqueue(message);

            // Remove lines as they fall off
            if (_lines.Count > _maxLines)
            {
                _lines.Dequeue();
            }
        }

        // Draw each line of the log to the console
        public void Draw(RLConsole console)
        {
            console.Clear();
            string[] lines = _lines.ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                console.Print(1, i + 1, lines[i], RLColor.White);
            }
        }
    }
}
