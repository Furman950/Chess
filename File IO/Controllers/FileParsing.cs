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
                        PlacePiece(line);
                        break;
                    case 5:
                        MoveSinglePiece(line);
                        break;
                    case 11:
                        MoveTwoPieces(line);
                        break;
                }
                
            }
        }

        private void PlacePiece(string line)
        {
            if (pieces.ChessPiece.TryGetValue(line[0].ToString(), out string piece))
            {
                if (pieces.PieceColor.TryGetValue(line[1].ToString(), out string color))
                {
                    Console.WriteLine($"Place {color} {piece} on {line[2]}{line[3]}");
                }
            }
        }

        private void MoveTwoPieces(string line)
        {
            Console.WriteLine("----------------------------------");
            MoveSinglePiece(line.Substring(0, 5));
            MoveSinglePiece(line.Substring(4, 5));
            Console.WriteLine("----------------------------------");
        }

        private void MoveSinglePiece(string line)
        {
            Console.WriteLine($"Move {line[0].ToString().ToUpper()}{line[1]} to " +
                $"{line[3].ToString().ToUpper()}{line[4]}" );
        }

        
    }
}
