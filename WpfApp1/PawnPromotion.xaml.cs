using ChessDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for PawnPromotion.xaml
    /// </summary>
    public partial class PawnPromotion : Window
    {
        public Pieces ChosenPiece { get; set; }
        public PawnPromotion()
        {
            InitializeComponent();
            var pawnPromotion = this.Owner as ChessDisplay.MainWindow;
            
        }

        private void btn_Clicked(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            char firstLetter = btn.Content.ToString()[0];

            switch (firstLetter)
            {
                case 'Q':
                    ChosenPiece = Pieces.Q;
                    break;
                case 'K':
                    ChosenPiece = Pieces.N;
                    break;
                case 'B':
                    ChosenPiece = Pieces.B;
                    break;
                case 'R':
                    ChosenPiece = Pieces.R;
                    break;
            }

            this.Close();
        }
    }
}
