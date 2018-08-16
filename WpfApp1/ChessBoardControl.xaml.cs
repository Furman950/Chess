using File_IO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessDisplay
{
    /// <summary>
    /// Interaction logic for ChessBoardControl.xaml
    /// </summary>
    public partial class ChessBoardControl : UserControl, INotifyPropertyChanged
    {
        private Board board;

        private ChessPieces pieces = new ChessPieces();

        public event PropertyChangedEventHandler PropertyChanged;

        public Board Board {
            get { return board; }
            set { board = value; }
        }


        public ChessBoardControl()
        {
            InitializeComponent();
        }

        public void loadedChessBoard(Object sender, RoutedEventArgs e) {
            MakeBoardCheckered();
            ViewBoardPieces();
        }

        public void ViewBoardPieces() {
            ChessPiece pieceHolder;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    pieceHolder = Board.GetPiece(x, y);
                    if (pieceHolder != null) {
                        PlacePiece(x, y, pieceHolder);
                    }
                }
            }
        }

        public void PlacePiece(int x, int y, ChessPiece pieceHolder) {

            Image piece = GetPieceImage(pieceHolder);
            piece.SetValue(Grid.RowProperty, y);
            piece.SetValue(Grid.ColumnProperty, x);
        }

        public Image GetPieceImage(ChessPiece pieceHolder) {
            Image image = new Image();
            string name = "";
            pieces.ChessPiece.TryGetValue(pieceHolder.Piece.ToString(), out name);
            string imageName = "../../Resources" + name + pieceHolder.Color.ToString() + ".png";
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream iconStream = asm.GetManifestResourceStream(imageName);
            PngBitmapDecoder iconDecoder = new PngBitmapDecoder(iconStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            ImageSource iconSource = iconDecoder.Frames[0];
            image.Source = iconSource;
            return image;
        }

        public void MakeBoardCheckered() {
            ColumnDefinition[] columnDefinitions = BoardDisplay.ColumnDefinitions.ToArray();
            RowDefinition[] rowDefinitions = BoardDisplay.RowDefinitions.ToArray();
            for (int column = 0; column < columnDefinitions.Length; column++)
            {
                int alternation = column % 2;
                for (int row = 0; row < rowDefinitions.Length; row++)
                {
                    if (row % 2 == alternation)
                    {
                        BoardDisplay.Children.Add(AddBoardRectangle(row, column));
                    }
                }
            }
        }

        public Rectangle AddBoardRectangle(int row, int column) {
            Rectangle rect = new Rectangle();
            rect.Fill = new SolidColorBrush(Colors.DarkGray);
            rect.SetValue(Grid.RowProperty, row);
            rect.SetValue(Grid.ColumnProperty, column);
            return rect;
        }
    }
}
