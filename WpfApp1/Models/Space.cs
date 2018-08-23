using ChessDisplay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChessDisplay.Models {
    public class Space : INotifyPropertyChanged {
        static BitmapImage transparent = new BitmapImage(new Uri("../Resoures/Tranparent.png", UriKind.Relative));

        private ChessPiece chessPiece;
        public ChessPiece ChessPiece {
            get { return chessPiece; }
            set {
                chessPiece = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChessPiece"));
                if (chessPiece == null) {
                    Image = null;
                } else {
                    Image = chessPiece?.BitmapImage;
                }
            }
        }

        private BitmapImage image;
        public BitmapImage Image {
            get {
                if (image == null) {
                    return transparent;
                } else {
                    return image;
                }
            }
            set {
                image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
            }
        }

        public Space(ChessPiece piece) {
            ChessPiece = piece;
        }
        public Space() { }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
