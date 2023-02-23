using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class OthelloGame
    {
        Board board = new Board();
        public void flipPieces(Tuple<int, int> piece1, Tuple<int, int> piece2)
        {
            if (piece1.Item1 == piece2.Item1)
            {
                for (int i = piece1.Item2; i <= piece2.Item2; i++)
                {
                    board.listOfTiles[piece1.Item1, i].FlipTile();
                }
            }
            else if (piece1.Item2 == piece2.Item2)
            {
                for (int i = piece1.Item1; i <= piece2.Item1; i++)
                {
                    board.listOfTiles[i,piece1.Item2].FlipTile();
                }
            }
            else
            {
                if (piece1.Item1 > piece2.Item1)
                {
                    int j = piece1.Item2;
                    for (int i = piece1.Item1; i <= piece2.Item1;)
                    {
                        i++; j++;
                        board.listOfTiles[i, j].FlipTile();
                    }
                }
                else
                {
                    int j = piece1.Item2;
                    for (int i = piece1.Item1; i <= piece2.Item1;)
                    {
                        i--; j--;
                        board.listOfTiles[i, j].FlipTile();
                    }
                }
            }
        }
    }
}
