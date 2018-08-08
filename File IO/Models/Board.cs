using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
