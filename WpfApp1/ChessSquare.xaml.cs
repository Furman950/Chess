using ChessDisplay.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessDisplay
{
    /// <summary>
    /// Interaction logic for ChessSquare.xaml
    /// </summary>
    public partial class ChessSquare : UserControl
    {
        Binding b;

        SolidColorBrush gray = Brushes.Gray;
        SolidColorBrush white = Brushes.White;
        SolidColorBrush green = Brushes.Green;
        SolidColorBrush blue = Brushes.DodgerBlue;
        SolidColorBrush red = Brushes.Red;

        private bool isGray = false;
        private bool isClicked = false;
        private bool isPossible = false;
        private bool isChecked = false;
        private bool isHighlighted = false;
        public bool IsGray {
            get { return isGray; }
            set {
                isGray = value;
                UpdateColor();
            }
        }
        public bool IsClicked {
            get { return isClicked; }
            set {
                isClicked = value;
                UpdateColor();
            }
        }
        public bool IsPossible {
            get { return isPossible; }
            set {
                isPossible = value;
                UpdateColor();
            }
        }
        public bool IsChecked {
            get { return isChecked; }
            set {
                isChecked = value;
                UpdateColor();
            }
        }
        public bool IsHiglighted {
            get { return isHighlighted; }
            set {
                isHighlighted = value;
                UpdateColor();
            }
        }

        public ChessSquare(bool gray)
        {
            InitializeComponent();
            IsGray = gray;
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
            IsHiglighted = true;
        }

        public void SelectPiece()
        {
            IsClicked = true;
        }

        public void DeselectPiece()
        {
            IsClicked = false;
        }

        private void UnHightLightSquare(object sender, MouseEventArgs e)
        {
            IsHiglighted = false;

        }

        internal void PossibleMove()
        {
            IsPossible = true;
        }

        internal void Check()
        {
            IsChecked = true;
        }

        public void UpdateColor() {
            if (IsHiglighted || IsClicked) {
                SetBackground(blue);
            } else if (IsChecked) {
                SetBackground(red);
            } else if (IsPossible) {
                SetBackground(green);
            } else if (IsGray) {
                SetBackground(gray);
            } else {
                SetBackground(white);
            }
        }
        private void SetBackground(SolidColorBrush color) {
            if (Square.Background != color) {
                Square.Background = color;
            }
        }
    }
}
