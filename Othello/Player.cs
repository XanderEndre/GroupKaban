using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class Player
    {
        private String name;
        private Tile colour;
        public void TakeTurn() {
            
        }
        public String GetName() {
            return this.name;
        }
        public void SetName(String name) {
            this.name = name;
        }
        public void SetColour(Tile colour)
        {
            this.colour = colour;
        }
        public Tile GetColour() { 
            return new Tile();
        }

    }
}
