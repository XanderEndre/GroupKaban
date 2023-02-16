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
        private Piece colour;
        public void TakeTurn() {
            
        }
        public String GetName() {
            return this.name;
        }
        public void SetName(String name) {
            this.name = name;
        }
        public void SetColour(Piece colour)
        {
            this.colour = colour;
        }
        public Piece GetColour() { 
            return new Piece();
        }

    }
}
