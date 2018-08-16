using File_IO.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp1;

namespace ChessDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board board;

        public MainWindow()
        {
            InitializeComponent();
            board = new Board();
            SetUpBoard();
        }

        private void SetUpBoard()
        {
            ChessSquare chessSquare;
            
            bool grey = false;
            for (int column = 0; column < 8; column++)
            {
                for (int row = 0; row < 8; row++)
                {
                    chessSquare = new ChessSquare(grey);
                    chessSquare.SetValue(Grid.ColumnProperty, column);
                    chessSquare.SetValue(Grid.RowProperty, row);
                    chessSquare.MouseDown += ChessBoard_MouseDown;
                    chessSquare.SetPicture(board[column, row]);

                    ChessBoard.Children.Add(chessSquare);
                    grey = !grey;
                }
                grey = !grey;
            }
            
        }

        private void ChessBoard_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        { 
            ChessSquare chessSquare = sender as ChessSquare;
            MessageBox.Show($"Column:{chessSquare?.GetValue(Grid.ColumnProperty)}\nRow:{chessSquare?.GetValue(Grid.RowProperty)}");
        }

    }
}
