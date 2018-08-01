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
        }

        private string CapturePiece(string line)
        {
            string start = line[0].ToString().ToUpper() + line[1];
            string end = line[3].ToString().ToUpper() + line[4];

            return $"Move {start} to {end} and captured piece at {end}";
        }

        private string PlacePiece(string line)
        {
            if (pieces.ChessPiece.TryGetValue(line[0].ToString(), out string piece))
            {
                if (pieces.PieceColor.TryGetValue(line[1].ToString(), out string color))
                {
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
            return $"Move {line[0].ToString().ToUpper()}{line[1]} to " +
                $"{line[3].ToString().ToUpper()}{line[4]}";
        }
    }
}
