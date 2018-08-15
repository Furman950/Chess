using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Controllers;

namespace File_IO.Models {
    public class Board {
        private ChessPiece[][] board;

        public Board() {
            board = new ChessPiece[8][];
            for (int i = 0; i < board.Length; i++) {
                board[i] = new ChessPiece[8];
            }
        }

        public void SetPiece(int x, int y, ChessPiece piece) {
            board[y][x] = piece;
        }

        public ChessPiece GetPiece(int x, int y) {
            return board[y][x];
        }

        public ChessPiece this[int x, int y] {
            get {
                return board[y][x];
            }
            set {
                board[y][x] = value;
            }
        }

        public bool Check(PieceColor kingColor) {
            int kingX = 0;
            int kingY = 0;
            for (int y = 0; y < board.Length; y++) {
                for (int x = 0; x < board[y].Length; x++) {
                    if (board[y][x] != null && board[y][x].Piece == Pieces.K && board[y][x].Color == kingColor) {
                        kingX = x;
                        kingY = y;
                    }
                }
            }
            for (int y = 0; y < board.Length; y++) {
                for (int x = 0; x < board[y].Length; x++) {
                    if (board[y][x] != null && board[y][x].Color != kingColor) {
                        if (WpfApp1.Controllers.MovingPiece.CheckMove(x, y, kingX, kingY, this)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckMate(PieceColor kingColor) {
            if (Check(kingColor)) {
                //Find King
                int kingX = 0;
                int kingY = 0;
                ChessPiece king = new ChessPiece(Pieces.K, kingColor);
                for (int y = 0; y < board.Length; y++) {
                    for (int x = 0; x < board[y].Length; x++) {
                        if (board[y][x] != null && board[y][x].Piece == Pieces.K && board[y][x].Color == kingColor) {
                            kingX = x;
                            kingY = y;
                            king = board[y][x];
                        }
                    }
                }

                //Try moves to get out of check
                for (int startY = 0; startY < board.Length; startY++) {
                    for (int startX = 0; startX < board[startY].Length; startX++) {
                        if (board[startY][startX] != null && board[startY][startX].Color == kingColor) {
                            for (int toY = 0; toY < board.Length; toY++) {
                                for (int toX = 0; toX < board[toY].Length; toX++) {
                                    Board boardClone = this.Clone();
                                    MovingPiece testMove = new MovingPiece();
                                    if (kingColor == PieceColor.D) {
                                        testMove.FlipColor();
                                    }
                                    if (testMove.Move(startX, startY, toX, toY, boardClone) &&
                                        !boardClone.Check(kingColor)) {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            } else {
                return false;
            }
        }

        public Board Clone() {
            Board newBoard = new Board();
            for (int y = 0; y < board.Length; y++) {
                for (int x = 0; x < board[y].Length; x++) {
                    newBoard[x, y] = board[y][x];
                }
            }
            return newBoard;
        }

        public override string ToString() {
            StringBuilder output = new StringBuilder();
            //for (int y = 0; y < board[0].Length; y++) {
            //    for (int x = 0; x < board.Length; x++) {
            //        if (board[x][y] == null) {
            //            output.Append("-");
            //        } else {
            //            output.Append(board[x][y].ToString());
            //        }
            //    }
            //    if (y != 7) {
            //        output.Append("\n");
            //    }
            //}
            foreach (ChessPiece[] row in board) {
                foreach (ChessPiece piece in row) {
                    if (piece == null) {
                        output.Append("-");
                    } else {
                        output.Append(piece.ToString());
                    }
                }
                if (row != board.Last()) {
                    output.Append("\n");
                }
            }
            return output.ToString();
        }
    }
}
