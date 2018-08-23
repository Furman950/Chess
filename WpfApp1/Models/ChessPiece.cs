
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChessDisplay.Models
{

    public enum Pieces
    {
        K, Q, B, N, R, P,
    }

    public enum PieceColor
    {
        L, D
    }
    public class ChessPiece : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private BitmapImage bitmapImage;
        public Pieces Piece{ get; set; }

        public PieceColor Color { get; set; }
        public BitmapImage BitmapImage {
            get { return bitmapImage; }
            set {
                bitmapImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BitmapImage"));
            } } 

        public ChessPiece(BitmapImage bitmapImage)
        {
            this.BitmapImage = bitmapImage;
        }

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
