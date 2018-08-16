
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
        public BitmapImage BitmapImage { get; set; } = new BitmapImage();

        public ChessPiece(Pieces piece, PieceColor color)
        {
            this.Piece = piece;
            this.Color = color;
        }

        public ChessPiece(Pieces piece, PieceColor color, BitmapImage bitmapImage) : this(piece, color)
        {
            this.BitmapImage = bitmapImage;
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
