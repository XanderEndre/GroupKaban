using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class Player
    {
        public string name { get; set; }
        public char character { get; set; }

        public Player() { }

        public Player(String name, char character)
        {
            this.name = name;
            this.character = character;
        }

    }
}
