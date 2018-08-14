using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_IO.Models
{

    public enum Pieces
    {
        K, Q, B, N, R, P,
    }

    public enum PieceColor
    {
        L, D
    }
    public class ChessPiece
    {
        public Pieces Piece{ get; set; }

        public PieceColor Color { get; set; }


        public ChessPiece(Pieces piece, PieceColor color)
        {
            this.Piece = piece;
            this.Color = color;
        }

        //Light is Uppercase
        public override string ToString()
        {
            switch (this.Color)
            {
                case PieceColor.L:
                    return $"{this.Piece.ToString()}";
                case PieceColor.D:
                    return $"{this.Piece.ToString().ToLower()}";
            }

            return "";
        }
    }
}
