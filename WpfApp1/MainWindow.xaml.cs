using File_IO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        ChessSquare selectedSquare;
        ChessSquare[,] chessSquareBoard = new ChessSquare[8, 8];
        List<int[]> moveList;

        public MainWindow()
        {
            InitializeComponent();
            board = new Board();
            SetUpBoard();
            lblTurn.DataContext = board;
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
                    chessSquare.SetPicture(board.GetSpace(column, row));

                    chessSquareBoard[row, column] = chessSquare;

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
                selectedSquare = chessSquare;
                int.TryParse(chessSquare.GetValue(Grid.ColumnProperty).ToString(), out int locX);
                int.TryParse(chessSquare.GetValue(Grid.RowProperty).ToString(), out int locY);
                pieceX = locX;
                pieceY = locY;
                chessSquare.SelectPiece();

                firstClick = false;

                HighLightMoves(pieceX, pieceY);
            }
            else
            {
                int.TryParse(chessSquare.GetValue(Grid.ColumnProperty).ToString(), out int toX);
                int.TryParse(chessSquare.GetValue(Grid.RowProperty).ToString(), out int toY);
                board.Move(pieceX, pieceY, toX, toY);
                selectedSquare.DeselectPiece();
                ResetBoard();

                firstClick = true;
            }
        }

        private void ResetBoard()
        {
            foreach(var move in moveList)
            {
                chessSquareBoard[move[1], move[0]].SetBackground();
            }
        }

        private void HighLightMoves(int x, int y)
        {
            moveList = board.GetPossibleMoves(x, y);

            foreach(var move in moveList)
            {
                chessSquareBoard[move[1], move[0]].PossibleMove();
            }
        }
    }
}
