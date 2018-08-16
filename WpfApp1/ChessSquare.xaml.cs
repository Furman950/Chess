using File_IO.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ChessSquare.xaml
    /// </summary>
    public partial class ChessSquare : UserControl
    {
        Binding b;
        public ChessSquare(bool grey)
        {
            InitializeComponent();

            if (grey)
                Square.Background = new SolidColorBrush(Colors.Gray);
            else
                Square.Background = new SolidColorBrush(Colors.White);
        }

        public void SetPicture(Space space)
        {
            b = new Binding {
                Source = space,
                Path = new PropertyPath("Image")
            };

            ChessPieceImage.SetBinding(Image.SourceProperty, b);
        }
    }
}
