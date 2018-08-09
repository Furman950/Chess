using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;
using File_IO.Models;

namespace WpfApp1.Controllers
{
    public class MovingPiece
    {
        private ChessPiece movingPiece;
        private int locationX, locationY, toX, toY;
        private Board board;

        public bool Move(int locationX, int locationY, int toX, int toY, Board board)
        {
            if ((movingPiece = board.GetPiece(locationX, locationY)) != null)
            {
                this.locationX = locationX;
                this.locationY = locationY;
                this.toX = toX;
                this.toY = toY;
                this.board = board;

                switch (movingPiece.Piece)
                {
                    case Pieces.K:
                        return MoveKing();
                    case Pieces.Q:
                        return MoveQueen();
                    case Pieces.B:
                        return MoveBishop();
                    case Pieces.N:
                        return MoveKnight();
                    case Pieces.R:
                        return MoveRook();
                    case Pieces.P:
                        return MovePawn();
                }
            }

            return false;
        }
        private bool MovePawn()
        {
            if (movingPiece.Color == PieceColor.L)
            {
                //Move two squares check
                if (toY == 3 && locationY == 1 && toX == locationX)
                {
                    for (int y = (locationY + 1); y <= toY; y++)
                    {
                        if (board[toX, y] != null)
                        {
                            return false;
                        }
                    }
                }

                //Check for moving to square
                else if (toY - locationY == 1 && toX == locationX)
                {

                }

                //Check for Capturing
                else if (toY - locationY == 1 && (toX == locationX - 1 || toX == locationX + 1) &&
                    (board[toX, toY] != null))
                {

                }
                else
                    return false;
            }

            else
            {
                //Move two squares check
                if (toY == 4 && locationY == 6 && toX == locationX)
                {
                    for (int y = (locationY - 1); y >= toY; y--)
                    {
                        if (board[toX, y] != null)
                        {
                            return false;
                        }
                    }
                }

                //Check for moving to square
                else if (toY - locationY == 1 && toX == locationX)
                {

                }

                //Check for Capturing
                else if (toY - locationY == 1 && (toX == locationX - 1 || toX == locationX + 1) &&
                    (board[toX, toY] != null))
                {

                }

                else
                    return false;
            }


            board[toX, toY] = movingPiece;
            board[locationX, locationY] = null;

            return true;
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
            if(isValidMove) {
                board.SetPiece(toX, toY, movingPiece);
                board.SetPiece(locationX, locationY, null);
            }
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
            } else if (board[locationX, locationY] == null) {
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
