using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChessDisplay
{
    class ChessPieceImages
    {
        public static System.Drawing.Image[] blackPieces;

        public static System.Drawing.Image[] whitePieces;

        public static void setChessAssets(string url) {
            splitImageIntoImagePieces(url);
        }

        private static void splitImageIntoImagePieces(string img)
        {
            var image = System.Drawing.Image.FromFile(img);
            blackPieces = setPieceImages(image, 1);
            whitePieces = setPieceImages(image, 2);
            
        }

        private static System.Drawing.Image[] setPieceImages(System.Drawing.Image src, int row) {
            System.Drawing.Image[] allPieces = new System.Drawing.Image[5];
            row--;
            for (int piece = 0; piece < 5; piece++)
            {
                allPieces[piece] = new Bitmap(150, 140);
                var graphics = Graphics.FromImage(allPieces[piece]);
                graphics.DrawImage(src, new Rectangle(0, 0, 140, 150), new Rectangle(row * 140, piece * 150, 140, 150), GraphicsUnit.Pixel);
                graphics.Dispose();
            }
            return allPieces;
        }
    }
}
