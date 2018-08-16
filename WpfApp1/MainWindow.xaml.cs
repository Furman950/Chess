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
        bool firstClick = true;
        int pieceX, pieceY;

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
            if (firstClick)
            {
                int.TryParse(chessSquare.GetValue(Grid.ColumnProperty).ToString(), out int locX);
                int.TryParse(chessSquare.GetValue(Grid.RowProperty).ToString(), out int locY);
                pieceX = locX;
                pieceY = locY;

                firstClick = false;
            }
            else
            {
                int.TryParse(chessSquare.GetValue(Grid.ColumnProperty).ToString(), out int toX);
                int.TryParse(chessSquare.GetValue(Grid.RowProperty).ToString(), out int toY);
                board.Move(pieceX, pieceY, toX, toY);

                firstClick = true;
            }
        }
    }
}
