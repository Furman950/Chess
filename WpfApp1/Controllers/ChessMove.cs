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
            
        }

        private bool MoveRook()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private bool MoveQueen()
        {
            throw new NotImplementedException();
        }

        private bool MoveKing()
        {
            throw new NotImplementedException();
        }
    }
}
