using File_IO.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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
        bool selected = false;
        bool isPossibleMove = false;

        SolidColorBrush gray;
        SolidColorBrush white;
        SolidColorBrush green;
        SolidColorBrush blue;
        SolidColorBrush red;

        public bool IsPossibleMove { get => isPossibleMove; set => isPossibleMove = value; }
        public bool InCheck { get; set; }

        public ChessSquare(bool gray)
        {
            InitializeComponent();
            isGray = gray;
            SetUpColors();
            SetBackground();
        }

        private void SetUpColors()
        {
            gray = new SolidColorBrush(Colors.Gray);
            white = new SolidColorBrush(Colors.White);
            green = new SolidColorBrush(Colors.Green);
            blue = new SolidColorBrush(Colors.DodgerBlue);
            red = new SolidColorBrush(Colors.Red);
        }

        public void SetBackground()
        {
            if (isGray)
                Square.Background = gray;
            else
                Square.Background = white;
        }

        public void SetPicture(Space space)
        {
            b = new Binding {
                Source = space,
                Path = new PropertyPath("Image")
            };

            ChessPieceImage.SetBinding(Image.SourceProperty, b);
        }

        private void HighLightSquare(object sender, MouseEventArgs e)
        {
            Square.Background = blue;
        }

        public void SelectPiece()
        {
            Square.Background = blue;
            selected = true;
        }

        public void DeselectPiece()
        {
            selected = false;
            SetBackground();
        }

        private void UnHightLightSquare(object sender, MouseEventArgs e)
        {
            if (!selected)
                SetBackground();

            if (IsPossibleMove)
                Square.Background = green;

            if (InCheck)
            {
                Square.Background = red;
            }

        }

        internal void PossibleMove()
        {
            Square.Background = green;
            IsPossibleMove = true;
        }

        internal void Check()
        {
            Square.Background = red;
            InCheck = true;
        }
    }
}
