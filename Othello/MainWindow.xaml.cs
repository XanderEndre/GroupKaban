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
        Grid board;

        public MainWindow()
        {
            board = new Grid();
            InitializeComponent();

            Content = CreateBoard(board);

            UpdateBoard();
        }


        public Grid CreateBoard(Grid board)
        {
            // Initialize the board


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

            //// Add an image element to each cell!
            for (int row = 0; row < BOARD_ROWS; row++)
            {
                for (int col = 0; col < BOARD_COLS; col++)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("empty-circle.png", System.UriKind.Relative));
                    Grid.SetRow(image, row);
                    Grid.SetColumn(image, col);
                    board.Children.Add(image);
                }
            }

            // Show the Grid Lines
            board.ShowGridLines = true;

            // Set the boards background color
            board.Background = Brushes.DarkGreen;

            // Initialize the board with starting pieces
            gameBoard[3, 3] = 'O';
            gameBoard[4, 4] = 'O';
            gameBoard[3, 4] = 'X';
            gameBoard[4, 3] = 'X';


            // Initalize the default

            return board;
        }

        private void UpdateBoard()
        {
            // Iterate over the rows
            for (int row = 0; row < BOARD_ROWS; row++)
            {
                // Iterate over the columns
                for (int col = 0; col < BOARD_COLS; col++)
                {
                    // Set the image to the current row and column
                    Image? image = board.Children[row * BOARD_COLS + col] as Image;
                    // If the game board has it registered as an X
                    if (gameBoard[row, col] == 'X')
                    {
                        image.Source = new BitmapImage(new System.Uri("black-circle.png", System.UriKind.Relative)); // Use your own image file here
                    }
                    // If the gameboard has it registered as an O
                    else if (gameBoard[row, col] == 'O')
                    {
                        image.Source = new BitmapImage(new System.Uri("blue-circle.png", System.UriKind.Relative)); // Use your own image file here
                    }
                }
            }
        }
    }
}