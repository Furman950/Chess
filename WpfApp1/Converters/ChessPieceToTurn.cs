using File_IO.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1.Converters
{
    class ChessPieceToTurn : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PieceColor color = (PieceColor)value;
            string turn;

            turn = (color == PieceColor.L) ? "Turn: Light" : "Turn: Dark";

            return turn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
