using File_IO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp1.Models {
    public class Space : INotifyPropertyChanged {
        private ChessPiece chessPiece;
        public ChessPiece ChessPiece {
            get { return chessPiece; }
            set {
                chessPiece = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChessPiece"));
                Image = chessPiece.BitmapImage;
            }
        }

        private BitmapImage image;
        public BitmapImage Image {
            get { return image; }
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
