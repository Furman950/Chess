using ChessDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ChessDisplay;

namespace ChessDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board board;
        bool firstClick = true;
        bool lightTurn = true;
        int pieceX, pieceY;
        ChessSquare selectedSquare;
        ChessSquare[,] chessSquareBoard = new ChessSquare[8, 8];
        List<int[]> moveList;
        int kingX, kingY;

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
            
            if (firstClick &&
                (board.CurrentTurn == board[(int)chessSquare.GetValue(Grid.ColumnProperty), (int)chessSquare.GetValue(Grid.RowProperty)].Color))
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
                if (board.Move(pieceX, pieceY, toX, toY))
                {
                    if (chessSquareBoard[kingY, kingX].InCheck)
                        chessSquareBoard[kingY, kingX].InCheck = false;
                }
                selectedSquare?.DeselectPiece();
                firstClick = true;

                ResetBoard();
            }

            if (lightTurn)
            {
                if (board.Check(PieceColor.D))
                    KingInCheck(PieceColor.D);

                lightTurn = false;
            }
            else
            {
                if (board.Check(PieceColor.L))
                    KingInCheck(PieceColor.L);

                lightTurn = true;
            }
        }

        private void KingInCheck(PieceColor color)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.GetPiece(x, y)?.Piece == Pieces.K &&
                        board.GetPiece(x, y)?.Color == color)
                    {
                        chessSquareBoard[y, x].Check();
                        kingX = x;
                        kingY = y;
                    }
                }
            }
        }

        private void ResetBoard()
        {
            if (moveList != null)
            {
                foreach (var move in moveList)
                {
                    chessSquareBoard[move[1], move[0]].IsPossibleMove = false;
                    chessSquareBoard[move[1], move[0]].SetBackground();
                }
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
