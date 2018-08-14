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
            saveChessSet(blackPieces, "./Images/dark/");
            saveChessSet(blackPieces, "./Images/light/");
            
        }

        private static void saveChessSet(System.Drawing.Image[] chessSet, string filePath) {
            SaveImage(chessSet[0], filePath + "/King.jpg");
            SaveImage(chessSet[1], filePath + "/Queen.jpg");
            SaveImage(chessSet[2], filePath + "/Rook.jpg");
            SaveImage(chessSet[3], filePath + "/Bishop.jpg");
            SaveImage(chessSet[4], filePath + "/Knight.jpg");
            SaveImage(chessSet[5], filePath + "/Pawn.jpg");
        }

        private static System.Drawing.Image[] setPieceImages(System.Drawing.Image src, int row) {
            System.Drawing.Image[] allPieces = new System.Drawing.Image[5];
            row--;
            int width = src.Width / 5;
            int height = src.Height / 2;
            for (int piece = 0; piece < 5; piece++)
            {
                allPieces[piece] = new Bitmap(width, height);
                var graphics = Graphics.FromImage(allPieces[piece]);
                graphics.DrawImage(src, new Rectangle(0, 0,  width, height), new Rectangle(row * width, piece * height, width, height), GraphicsUnit.Pixel);
                graphics.Dispose();
            }
            return allPieces;
        }

        private static void SaveImage(System.Drawing.Image image, string fileName) {
            image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
