using Chess.View;
using File_IO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_IO.Controllers
{
    class FileParsing
    {
        ChessPieces pieces;
        Board board = new Board();
        public void ParseFile(string fileName)
        {
            pieces = new ChessPieces();
            var lines = File.ReadLines($"../../{fileName}");
            foreach (var line in lines)
            {
                switch (line.Length)
                {
                    case 4:
                        Console.WriteLine(PlacePiece(line));
                        break;
                    case 5:
                        Console.WriteLine(MoveSinglePiece(line));
                        break;
                    case 6:
                        Console.WriteLine(CapturePiece(line));
                        break;
                    case 11:
                        Console.WriteLine(MoveTwoPieces(line));
                        break;
                }

            }
            ConsoleView consoleView = new ConsoleView();
            consoleView.DisplayBoard(board);
        }

        private string CapturePiece(string line)
        {
            string start = line[0].ToString().ToUpper() + line[1];
            string end = line[3].ToString().ToUpper() + line[4];

            int x1 = char.ToLower(line[0]) - 97;
            int y1 = int.Parse(line[1].ToString()) - 1;
            int x2 = char.ToLower(line[3]) - 97;
            int y2 = int.Parse(line[4].ToString()) - 1;
            PieceMove(x1, y1, x2, y2);
            //Add points?

            return $"Move {start} to {end} and captured piece at {end}";
        }

        private string PlacePiece(string line)
        {
            if (pieces.ChessPiece.TryGetValue(line[0].ToString(), out string piece))
            {
                if (pieces.PieceColor.TryGetValue(line[1].ToString(), out string color))
                {
                    ChessPiece chessPiece = new ChessPiece((Pieces)Enum.Parse(typeof (Pieces), line[0].ToString(), true),
                        (PieceColor)Enum.Parse(typeof (PieceColor), line[1].ToString(), true));
                    int x = char.ToLower(line[2]) - 97;
                    int y = int.Parse(line[3].ToString()) - 1;
                    board.SetPiece(x, y, chessPiece);

                    return $"Place {color} {piece} on {line[2]}{line[3]}";
                }
            }
            return "";
        }

        private string MoveTwoPieces(string line)
        {
            return $"Moving two pieces: {MoveSinglePiece(line.Substring(0, 5))} AND {MoveSinglePiece(line.Substring(6, 5))}";
        }

        private string MoveSinglePiece(string line)
        {
            int x1 = char.ToLower(line[0]) - 97;
            int y1 = int.Parse(line[1].ToString()) - 1;
            int x2 = char.ToLower(line[3]) - 97;
            int y2 = int.Parse(line[4].ToString()) - 1;
            PieceMove(x1, y1, x2, y2);

            return $"Move {line[0].ToString().ToUpper()}{line[1]} to " +
                $"{line[3].ToString().ToUpper()}{line[4]}";
        }
        private void PieceMove(int x1, int y1, int x2, int y2) {
            board.SetPiece(x2, y2, board.GetPiece(x1, y1));
            board.SetPiece(x1, y1, null);
        }
    }
}
