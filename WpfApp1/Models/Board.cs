﻿using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace File_IO.Models {
    public class Board {
        private ChessPiece[][] board;
        private PieceColor lastColor = PieceColor.D;

        public Board() {
            board = new ChessPiece[8][];
            for (int y = 0; y < board.Length; y++) {
                board[y] = new ChessPiece[8];
            }
            SetUpBoard();
        }

        private void SetUpBoard()
        {
            BitmapImage pawnL = new BitmapImage(new Uri("../Resources/P_L.png", UriKind.Relative));
            BitmapImage pawnD = new BitmapImage(new Uri("../Resources/P_D.png", UriKind.Relative));
            BitmapImage transparent = new BitmapImage(new Uri("../Resoures/Tranparent.png", UriKind.Relative));

            //Dark
            this[0, 0] = new ChessPiece(Pieces.R, PieceColor.L, new BitmapImage(new Uri("../Resources/R_D.png", UriKind.Relative)));
            this[1, 0] = new ChessPiece(Pieces.N, PieceColor.L, new BitmapImage(new Uri("../Resources/N_D.png", UriKind.Relative)));
            this[2, 0] = new ChessPiece(Pieces.B, PieceColor.L, new BitmapImage(new Uri("../Resources/B_D.png", UriKind.Relative)));
            this[3, 0] = new ChessPiece(Pieces.Q, PieceColor.L, new BitmapImage(new Uri("../Resources/Q_D.png", UriKind.Relative)));
            this[4, 0] = new ChessPiece(Pieces.K, PieceColor.L, new BitmapImage(new Uri("../Resources/K_D.png", UriKind.Relative)));
            this[5, 0] = new ChessPiece(Pieces.B, PieceColor.L, new BitmapImage(new Uri("../Resources/B_D.png", UriKind.Relative)));
            this[6, 0] = new ChessPiece(Pieces.N, PieceColor.L, new BitmapImage(new Uri("../Resources/N_D.png", UriKind.Relative)));
            this[7, 0] = new ChessPiece(Pieces.R, PieceColor.L, new BitmapImage(new Uri("../Resources/R_D.png", UriKind.Relative)));

            for (int i = 0; i < 8; i++)
            {
                this[i, 1] = new ChessPiece(Pieces.P, PieceColor.L, pawnD);
            }

            //Transpernt image
            for (int row = 2; row < 6; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    this[column, row] = new ChessPiece(transparent);
                }
            }

            ////Light
            this[0, 7] = new ChessPiece(Pieces.R, PieceColor.L, new BitmapImage(new Uri("../Resources/R_L.png", UriKind.Relative)));
            this[1, 7] = new ChessPiece(Pieces.N, PieceColor.L, new BitmapImage(new Uri("../Resources/N_L.png", UriKind.Relative)));
            this[2, 7] = new ChessPiece(Pieces.B, PieceColor.L, new BitmapImage(new Uri("../Resources/B_L.png", UriKind.Relative)));
            this[3, 7] = new ChessPiece(Pieces.Q, PieceColor.L, new BitmapImage(new Uri("../Resources/Q_L.png", UriKind.Relative)));
            this[4, 7] = new ChessPiece(Pieces.K, PieceColor.L, new BitmapImage(new Uri("../Resources/K_L.png", UriKind.Relative)));
            this[5, 7] = new ChessPiece(Pieces.B, PieceColor.L, new BitmapImage(new Uri("../Resources/B_L.png", UriKind.Relative)));
            this[6, 7] = new ChessPiece(Pieces.N, PieceColor.L, new BitmapImage(new Uri("../Resources/N_L.png", UriKind.Relative)));
            this[7, 7] = new ChessPiece(Pieces.R, PieceColor.L, new BitmapImage(new Uri("../Resources/R_L.png", UriKind.Relative)));

            for (int i = 0; i < 8; i++)
            {
                this[i, 6] = new ChessPiece(Pieces.P, PieceColor.L, pawnL);
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
                        if (CheckMove(x, y, kingX, kingY)) {
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
                                    if (boardClone.Move(startX, startY, toX, toY) &&
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
            Board newBoard = new Board() {
                lastColor = this.lastColor
            };
            for (int y = 0; y < board.Length; y++) {
                for (int x = 0; x < board[y].Length; x++) {
                    newBoard[x, y] = board[y][x];
                }
            }
            return newBoard;
        }
        public void Copy(Board otherBoard) {
            for (int y = 0; y < board.Length; y++) {
                for (int x = 0; x < board[y].Length; x++) {
                    board[y][x] = otherBoard[x, y];
                }
            }
            lastColor = otherBoard.lastColor;
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

        public bool Move(int locationX, int locationY, int toX, int toY) {
            ChessPiece movingPiece = this[locationX, locationY];
            if (movingPiece != null) {
                if (this.CheckMove(locationX, locationY, toX, toY) && movingPiece.Color != lastColor) {
                    Board boardClone = this.Clone();
                    boardClone[locationX, locationY] = null;
                    boardClone[toX, toY] = movingPiece;
                    if (!boardClone.Check(movingPiece.Color)) {
                        this.Copy(boardClone);
                        lastColor = movingPiece.Color;
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CheckMove(int locationX, int locationY, int toX, int toY) {
            bool result = false;
            switch (this[locationX, locationY].Piece) {
                case Pieces.K:
                    result = MoveKing(locationX, locationY, toX, toY);
                    break;
                case Pieces.Q:
                    result = MoveQueen(locationX, locationY, toX, toY);
                    break;
                case Pieces.B:
                    result = MoveBishop(locationX, locationY, toX, toY);
                    break;
                case Pieces.N:
                    result = MoveKnight(locationX, locationY, toX, toY);
                    break;
                case Pieces.R:
                    result = MoveRook(locationX, locationY, toX, toY);
                    break;
                case Pieces.P:
                    result = MovePawn(locationX, locationY, toX, toY);
                    break;
            }
            return result;
        }

        private bool MovePawn(int locationX, int locationY, int toX, int toY) {
            ChessPiece movingPiece = this[locationX, locationY];
            int colorCoefficient = 1;
            if (movingPiece.Color == PieceColor.D) {
                colorCoefficient = -1;
            }
            if (locationX == toX) {
                //Two-space movement check
                if ((locationY == 0 || locationY == 6) && toY - locationY == 2 * colorCoefficient) {
                    if (this[locationX, locationY + colorCoefficient] == null &&
                        this[locationX, locationY + colorCoefficient * 2] == null) {
                        return true;
                    }
                } else if (toY - locationY == colorCoefficient) {           //One-space movement check
                    if (this[locationX, locationY + colorCoefficient] == null) {
                        return true;
                    }
                }
            } else if (Math.Abs(toX - locationX) == 1 && toY - locationY == colorCoefficient) {     //Capture check
                if (this[locationX, locationY + colorCoefficient] != null) {
                    return true;
                }
            }
            return false;
        }

        private bool MoveRook(int locationX, int locationY, int toX, int toY) {
            if (locationX == toX ^ locationY == toY) {
                return CheckDirection(locationX, locationY, toX, toY);
            } else {
                return false;
            }
        }

        private bool MoveKnight(int locationX, int locationY, int toX, int toY) {
            bool isValidLocation = IsValidMoveKnight(locationX, locationY, toX, toY);
            ChessPiece placeMovedTo = this.GetPiece(toX, toY);
            bool isNotOccupiedByFriendlyPiece = placeMovedTo == null ||
                placeMovedTo.Color != this[locationX, locationY].Color;
            bool isValidMove = isValidLocation && isNotOccupiedByFriendlyPiece;
            return isValidMove;
        }

        private bool IsValidMoveKnight(int locationX, int locationY, int toX, int toY) {
            bool isValid;
            int absoluteValueX = Math.Abs(locationX - toX);
            int absoluteValueY = Math.Abs(locationY - toY);
            switch (absoluteValueX) {
                case 1:
                    isValid = absoluteValueY == 2;
                    break;
                case 2:
                    isValid = absoluteValueY == 1;
                    break;
                default:
                    isValid = false;
                    break;
            }
            return isValid;
        }

        private bool MoveBishop(int locationX, int locationY, int toX, int toY) {
            if (Math.Abs(locationX - toX) == Math.Abs(locationY - toY) && Math.Abs(locationX - toX) != 0) {
                return CheckDirection(locationX, locationY, toX, toY);
            } else {
                return false;
            }
        }

        private bool MoveQueen(int locationX, int locationY, int toX, int toY) {
            if (locationX == toX && locationY == toY) {
                return false;
            } else {
                return CheckDirection(locationX, locationY, toX, toY);
            }
        }

        private bool MoveKing(int locationX, int locationY, int toX, int toY) {
            if (Math.Abs(locationX - toX) < 2 && Math.Abs(locationY - toY) < 2 &&
                !(locationX == toX && locationY == toY)) {
                return this[toX, toY] == null || this[toX, toY].Color != this[locationX, locationY].Color;
            } else {
                return false;
            }
        }

        private bool CheckDirection(int locationX, int locationY, int toX, int toY) {
            return CheckDirection(locationX, locationY, toX, toY, this[locationX, locationY]);
        }
        private bool CheckDirection(int locationX, int locationY, int toX, int toY, ChessPiece startPiece) {
            if (locationX == toX && locationY == toY) {
                if (this[locationX, locationY] == null ||
                    this[locationX, locationY].Color != startPiece.Color) {
                    return true;
                } else {
                    return false;
                }
            } else if ((locationX != toX && locationY != toY) ||
                Math.Abs(locationX - toX) != Math.Abs(locationY - toY)) {
                return false;
            } else if (this[locationX, locationY] == null || this[locationX, locationY] == startPiece) {
                if (locationX == toX) {
                    return CheckDirection(locationX, locationY + (Math.Abs(toY - locationY) / (toY - locationY)),
                        toX, toY, startPiece);
                } else if (locationY == toY) {
                    return CheckDirection(locationX + (Math.Abs(toX - locationX) / (toX - locationX)), locationY,
                        toX, toY, startPiece);
                } else {
                    return CheckDirection(locationX + (Math.Abs(toX - locationX) / (toX - locationX)),
                        locationY + (Math.Abs(toY - locationY) / (toY - locationY)), toX, toY, startPiece);
                }
            } else {
                return false;
            }
        }
    }
}
