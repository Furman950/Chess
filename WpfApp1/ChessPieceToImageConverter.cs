using File_IO.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    class ChessPieceToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ChessPiece piece;
            BitmapImage result = null;
            if (targetType == typeof(BitmapImage))
            {
                piece = value as ChessPiece;
                result = new BitmapImage(new Uri("../Resources/P_L.png", UriKind.Relative));
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
