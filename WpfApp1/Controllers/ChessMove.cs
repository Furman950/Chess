using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;
using File_IO.Models;

namespace WpfApp1.Controllers
{
    public class MovingPiece {
        private ChessPiece movingPiece;
        private int locationX, locationY, toX, toY;
        private Board board;
        private PieceColor lastColor = PieceColor.D;

        public bool Move(int locationX, int locationY, int toX, int toY, Board board) {
            if ((movingPiece = board.GetPiece(locationX, locationY)) != null) {
                if (CheckMove(locationX, locationY, toX, toY, board) && movingPiece.Color != lastColor) {
                    Board boardClone = board.Clone();
                    boardClone[locationX, locationY] = null;
                    boardClone[toX, toY] = movingPiece;
                    if (!boardClone.Check(movingPiece.Color)) {
                        board = boardClone;
                        lastColor = movingPiece.Color;
                        return true;
                    }
                }
            }
            return false;
        }
        public void FlipColor() {
            if (lastColor == PieceColor.D) {
                lastColor = PieceColor.L;
            } else {
                lastColor = PieceColor.D;
            }
        }
        public bool CheckMove(int locationX, int locationY, int toX, int toY, Board board) {
            this.locationX = locationX;
            this.locationY = locationY;
            this.toX = toX;
            this.toY = toY;
            this.board = board;

            bool result = false;
            switch (movingPiece.Piece) {
                case Pieces.K:
                    result = MoveKing();
                    break;
                case Pieces.Q:
                    result = MoveQueen();
                    break;
                case Pieces.B:
                    result = MoveBishop();
                    break;
                case Pieces.N:
                    result = MoveKnight();
                    break;
                case Pieces.R:
                    result = MoveRook();
                    break;
                case Pieces.P:
                    result = MovePawn();
                    break;
            }
            return result;
        }

        private bool MovePawn()
        {
            int colorCoefficient = 1;
            if (movingPiece.Color == PieceColor.D) {
                colorCoefficient = -1;
            }
            if (locationX == toX) {
                //Two-space movement check
                if ((locationY == 0 || locationY == 6) && toY - locationY == 2 * colorCoefficient) {
                    if (board[locationX, locationY + colorCoefficient] == null &&
                        board[locationX, locationY + colorCoefficient * 2] == null) {
                        return true;
                    }
                } else if (toY - locationY == colorCoefficient) {           //One-space movement check
                    if (board[locationX, locationY + colorCoefficient] == null) {
                        return true;
                    }
                }
            } else if (Math.Abs(toX - locationX) == 1 && toY - locationY == colorCoefficient) {     //Capture check
                if (board[locationX, locationY + colorCoefficient] != null) {
                    return true;
                }
            }
            return false;
        }

        private bool MoveRook()
        {
            if (locationX == toX ^ locationY == toY) {
                return CheckDirection(locationX, locationY, toX, toY);
            } else {
                return false;
            }
        }

        private bool MoveKnight()
        {
            bool isValidLocation = IsValidMoveKnight();
            ChessPiece placeMovedTo = board.GetPiece(toX, toY);
            bool isNotOccupiedByFriendlyPiece = placeMovedTo == null || placeMovedTo.Color != movingPiece.Color;
            bool isValidMove = isValidLocation && isNotOccupiedByFriendlyPiece;
            return isValidMove;
        }

        private bool IsValidMoveKnight() {
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

        private bool MoveBishop()
        {
            if (Math.Abs(locationX - toX) == Math.Abs(locationY - toY) && Math.Abs(locationX - toX) != 0) {
                return CheckDirection(locationX, locationY, toX, toY);
            } else {
                return false;
            }
        }

        private bool MoveQueen()
        {
            if (locationX == toX && locationY == toY) {
                return false;
            } else {
                return CheckDirection(locationX, locationY, toX, toY);
            }
        }

        private bool MoveKing()
        {
            if (Math.Abs(locationX - toX) < 2 && Math.Abs(locationY - toY) < 2 &&
                !(locationX == toX && locationY == toY)) {
                return board[toX, toY] == null || board[toX, toY].Color != movingPiece.Color;
            } else {
                return false;
            }
        }

        private bool CheckDirection(int locationX, int locationY, int toX, int toY) {
            if (locationX == toX && locationY == toY) {
                if (board[locationX, locationY] == null || board[locationX, locationY].Color != movingPiece.Color) {
                    return true;
                } else {
                    return false;
                }
            } else if ((locationX != toX && locationY != toY) ||
                Math.Abs(locationX - toX) != Math.Abs(locationY - toY)) {
                return false;
            } else if (board[locationX, locationY] == null || board[locationX, locationY] == movingPiece) {
                if (locationX == toX) {
                    return CheckDirection(locationX, locationY + (Math.Abs(toY - locationY) / (toY - locationY)),
                        toX, toY);
                } else if (locationY == toY) {
                    return CheckDirection(locationX + (Math.Abs(toX - locationX) / (toX - locationX)), locationY,
                        toX, toY);
                } else {
                    return CheckDirection(locationX + (Math.Abs(toX - locationX) / (toX - locationX)),
                        locationY + (Math.Abs(toY - locationY) / (toY - locationY)), toX, toY);
                }
            } else {
                return false;
            }
        }
    }
}
