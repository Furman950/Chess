using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_IO.Models
{
    class ChessPieces
    {
        public Dictionary<string, string> ChessPiece { get; }
        public Dictionary<string, string> PieceColor { get; }

        public ChessPieces()
        {
            ChessPiece = new Dictionary<string, string>();
            PieceColor = new Dictionary<string, string>();

            ChessPiece.Add("K", "King");
            ChessPiece.Add("Q", "Queen");
            ChessPiece.Add("B", "Bishop");
            ChessPiece.Add("N", "Knight");
            ChessPiece.Add("R", "Rook");
            ChessPiece.Add("P", "Pawn");

            PieceColor.Add("l", "Light");
            PieceColor.Add("d", "Dark");
        }
    }
}
