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
        private int BOARD_ROWS = 8;
        private int BOARD_COLS = 8;
        Grid board;

        public MainWindow()
        {
            board = new Grid();
            InitializeComponent();
            Content = CreateBoard(board);
        }

 
        public Grid CreateBoard(Grid board)
        {
            // Initialize the board
           

            // Add Rows
            for(int row = 0; row < BOARD_ROWS; row++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                board.RowDefinitions.Add(rowDefinition);
            }

            // Add Columns
            for(int col = 0; col < BOARD_COLS; col++)
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
                    image.Source = new BitmapImage();
                    Grid.SetRow(image, row);
                    Grid.SetColumn(image, col);
                    board.Children.Add(image);
                }
            }

            // Show the Grid Lines
            board.ShowGridLines = true;

            // Initalize the default

            return board;
        }
    }
}
