using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Othello
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int BOARD_ROWS = 8;
        private const int BOARD_COLS = 8;
        private char[,] gameBoard = new char[BOARD_ROWS, BOARD_COLS];
        private List<Player> playerList = new List<Player>();
        private int playerTurn = 0;
        Grid gameGrid = new Grid();
        Grid board = new Grid();
        Label turnLabel = new Label();

        public MainWindow()
        {

            // Initialize the board
            InitializeComponent();

            // Define the players

            playerList.Add(new Player("Jack", 'O'));
            playerList.Add(new Player("John", 'X'));

            // Find the game board by name and cast it to a Grid
            rootGrid = FindName("rootGrid") as Grid;

            // Create a new game grid that has the top label and the game board
            gameGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            gameGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            // Create the label and add it to the first row

            turnLabel.Content = playerList[playerTurn].name + "'s turn";
            turnLabel.HorizontalAlignment = HorizontalAlignment.Center;
            turnLabel.VerticalAlignment = VerticalAlignment.Center;
            gameGrid.Children.Add(turnLabel);
            Grid.SetRow(turnLabel, 0);



            // If the Grid is found add our new board too it
            if (board != null)
            {
                CreateBoard(board);
                gameGrid.Children.Add(board);
                Grid.SetRow(board, 1);
            }
            // The grid is not found create a new one
            {
                // Create a new grid
                board = new Grid();
                CreateBoard(board);
                gameGrid.Children.Add(board);
                Grid.SetRow(board, 1);
            }

            // Add the new grid to the main window's content
            rootGrid.Children.Add(gameGrid);


            char playerChar = playerList[playerTurn].character;
            char opponentChar = playerList[(playerTurn + 1) % playerList.Count].character;

            UpdateBoard(board);
            ShowPotentialMoves(playerChar, opponentChar);
            UpdateBoard(board);

        }


        public Grid CreateBoard(Grid board)
        {

            // Add Rows
            for (int row = 0; row < BOARD_ROWS; row++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                board.RowDefinitions.Add(rowDefinition);
            }

            // Add Columns
            for (int col = 0; col < BOARD_COLS; col++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                board.ColumnDefinitions.Add(columnDefinition);
            }

            // Add an image element to each cell!
            for (int row = 0; row < BOARD_ROWS; row++)
            {
                for (int col = 0; col < BOARD_COLS; col++)
                {

                    gameBoard[row, col] = ' ';

                    // Define the border
                    Border border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(2);


                    // Create an Image Element
                    Image image = new Image();
                    image.Source = null;
                    image.Margin = new Thickness(2);

                    // Add the Image element tot he border Element
                    border.Child = image;

                    // Add the border element to the Grid
                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);
                    board.Children.Add(border);

                    // Initialize the event for clicking!
                    border.MouseLeftButtonDown += gameLeftMouseClick;
                }
            }

            // Show the Grid Lines
            //board.ShowGridLines = true;

            // Set the boards background color
            board.Background = Brushes.DarkGreen;

            // Initialize the board with starting pieces
            gameBoard[3, 3] = 'O';
            gameBoard[4, 4] = 'O';
            gameBoard[3, 4] = 'X';
            gameBoard[4, 3] = 'X';

            return board;
        }

        private void gameLeftMouseClick(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            int row = Grid.GetRow(border);
            int col = Grid.GetColumn(border);

            char playerChar = playerList[playerTurn].character;
            char opponentChar = playerList[(playerTurn + 1) % playerList.Count].character;

            ShowPotentialMoves(playerChar, opponentChar);


            // Check if the selected cell is a valid move
            if (IsValidMove(row, col, playerChar, opponentChar))
            {
                // Place the player's piece on the selected cell
                gameBoard[row, col] = playerChar;

                // Flip opponent's pieces in each direction
                FlipPieces(row, col, playerChar, opponentChar);

                // Switch to the next player's turn
                playerTurn = (playerTurn + 1) % playerList.Count;
                turnLabel.Content = playerList[playerTurn].name + "'s turn";

                // RESET THE BOARD
                for (int r = 0; r < BOARD_ROWS; r++)
                {
                    for (int c = 0; c < BOARD_COLS; c++)
                    {
                        if (gameBoard[r, c] == 'A')
                        {
                            // Check if this empty cell is a valid move for the current player
                            if (IsValidMove(r, c, playerChar, opponentChar))
                            {
                                // Mark this cell as a potential move by highlighting it or displaying a marker on it
                                HighlightCell(r, c, ' ');
                            }
                        }
                    }
                }

                
                // Update the game board graphics
                UpdateBoard(board);
            }
        }


        private void ShowPotentialMoves(char playerChar, char opponentChar)
        {
            for (int r = 0; r < BOARD_ROWS; r++)
            {
                for (int c = 0; c < BOARD_COLS; c++)
                {
                    if (gameBoard[r, c] == ' ')
                    {
                        // Check if this empty cell is a valid move for the current player
                        if (IsValidMove(r, c, playerChar, opponentChar))
                        {
                            // Mark this cell as a potential move by highlighting it or displaying a marker on it
                            HighlightCell(r, c, 'A');
                        }
                    }
                }
            }
        }


        private void HighlightCell(int row, int col, char newPieceType)
        {
            gameBoard[row, col] = newPieceType;
            UpdateBoard(board);
        }

        // Check if the specified cell is a valid move for the given player
        private bool IsValidMove(int row, int col, char playerChar, char opponentChar)
        {
            // Check if the cell is already occupied
            if (gameBoard[row, col] != ' ' && gameBoard[row, col] != 'A') return false;


            // Check each direction for available flips
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    // Ignore the current cell
                    if (dr == 0 && dc == 0) continue;

                    // Check for valid flips in this direction
                    int r = row + dr, c = col + dc;
                    bool foundOpponent = false;
                    while (r >= 0 && r < BOARD_ROWS && c >= 0 && c < BOARD_COLS && gameBoard[r, c] == opponentChar)
                    {
                        r += dr;
                        c += dc;
                        foundOpponent = true;
                    }
                    if (foundOpponent && r >= 0 && r < BOARD_ROWS && c >= 0 && c < BOARD_COLS && gameBoard[r, c] == playerChar)
                    {
                        return true;
                    }
                }
            }


            // No valid moves were found
            return false;
        }

        // Flip opponent's pieces in each direction
        private void FlipPieces(int row, int col, char playerChar, char opponentChar)
        {
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    // Ignore the current cell
                    if (dr == 0 && dc == 0) continue;

                    // Check for valid flips in this direction
                    int r = row + dr, c = col + dc;
                    bool foundOpponent = false;
                    while (r >= 0 && r < BOARD_ROWS && c >= 0 && c < BOARD_COLS && gameBoard[r, c] == opponentChar)
                    {
                        r += dr;
                        c += dc;
                        foundOpponent = true;
                    }
                    if (foundOpponent && r >= 0 && r < BOARD_ROWS && c >= 0 && c < BOARD_COLS && gameBoard[r, c] == playerChar)
                    {
                        // Flip the pieces in this direction
                        r = row + dr;
                        c = col + dc;
                        while (gameBoard[r, c] == opponentChar)
                        {
                            gameBoard[r, c] = playerChar;
                            r += dr;
                            c += dc;
                        }
                    }
                }
            }
        }

        private void UpdateBoard(Grid board)
        {
            // Iterate over the rows
            for (int row = 0; row < BOARD_ROWS; row++)
            {
                // Iterate over the columns
                for (int col = 0; col < BOARD_COLS; col++)
                {
                    // Get the border at the current row and column
                    Border border = (Border)board.Children[row * BOARD_COLS + col];

                    // Get the image element from the border
                    Image image = (Image)border.Child;

                    // If the game board has it registered as an X
                    if (gameBoard[row, col] == 'X')
                    {
                        image.Source = new BitmapImage(new Uri("resources/black-circle.png", System.UriKind.Relative)); // Use your own image file here
                    }
                    // If the gameboard has it registered as an O
                    else if (gameBoard[row, col] == 'O')
                    {
                        image.Source = new BitmapImage(new Uri("resources/blue-circle.png", System.UriKind.Relative)); // Use your own image file here
                    }
                    else if (gameBoard[row, col] == 'A')
                    {
                        image.Source = new BitmapImage(new Uri("resources/grey-circle.png", System.UriKind.Relative)); // Use your own image file here
                    }
                    // The gameboard has it registered as a null space
                    else
                    {
                        image.Source = new BitmapImage(new Uri("resources/empty-circle.png", System.UriKind.Relative));
                    }
                }
            }
        }
        
        private char CheckForWin() {
            int playerOneCount = 0;
			int playerTwoCount = 0;
            // Check all board positions
			for (int r = 0; r < BOARD_ROWS; r++) {
				for (int c = 0; c < BOARD_COLS; c++) {
                    // If a position is empty the game is not over
                    // Returns a space
                    if (gameBoard[r, c] == '\0' || gameBoard[r, c] == 'A') {
                        return ' ';
                    // Counts the number of spaces with each character
                    } else if (gameBoard[r, c] == playerList[0].character) {
                        playerOneCount++;
                    } else if (gameBoard[r, c] == playerList[1].character) {
                        playerTwoCount++;
                    }
				}
			}
            // Returns the character of the player that won
            return playerOneCount > playerTwoCount ? playerList[0].character : playerList[1].character;
		}
    }
}