using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Interfaces;

namespace Chess.View
{
    class ConsoleView : IView
    {
        public void DisplayBoard(Board board) {
            Console.WriteLine(board.ToString());
        }
    }
}
