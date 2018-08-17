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
        bool isGray;
        bool selected;
        public ChessSquare(bool gray)
        {
            InitializeComponent();
            isGray = gray;
            SetBackground();
        }

        public void SetBackground()
        {
            if (isGray)
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

        private void HighLightSquare(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Square.Background = new SolidColorBrush(Colors.DodgerBlue);
        }

        public void SelectPiece()
        {
            Square.Background = new SolidColorBrush(Colors.DodgerBlue);
            selected = true;
        }

        public void DeselectPiece()
        {
            selected = false;
            SetBackground();
        }

        private void UnHightLightSquare(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!selected)
            {
                SetBackground();
            }
        }

        internal void PossibleMove()
        {
            Square.Background = new SolidColorBrush(Colors.Green);
        }
    }
}
