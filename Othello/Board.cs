using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Othello
{
    class Board
    {
        // Create an Array of Titles in an 8x8 array
        public Tile[,] listOfTiles = new Tile[8, 8];
        
        // Reset the game board
        public void resetBoard() {
            listOfTiles = new Tile[8, 8];

            listOfTiles[3, 3] = new Tile(true);
            listOfTiles[3, 4] = new Tile(false);
            listOfTiles[4, 3] = new Tile(true);
            listOfTiles[4, 4] = new Tile(false);
        
        }

        // Prints the current game board to the console
        public void printBoard() 
        { 
            for(int row = 0; row < 8; row++)
            {
                for(int col = 0; col < 8; col++)
                {
                    // checks that the item is of the type Tile
                    if (listOfTiles[row, col] is Tile)
                    {
                        if (listOfTiles[row, col].isTileBlack)
                        {
                            System.Console.Write("B ");
                        }
                        else {
                            System.Console.WriteLine("W ");
                        }
                    } else
                    {
                        System.Console.Write(". ");
                    }
                }
                System.Console.WriteLine(" ");
            }
        
        }

    }
}
