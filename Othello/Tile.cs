using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class Tile
    {

        public Tile(bool isTileBlack)
        {
            this.isTileBlack = isTileBlack;
        }

        public bool isTileBlack { get; set; }
        public void FlipTile() { isTileBlack = !isTileBlack; }
    }
}
