using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File_IO.Models;

namespace Chess.Interfaces
{
    interface IView
    {
        /// <summary>
        /// Displays the current Chess Board State. The implementation must use null to show that there's no piece on a
        /// specific board
        /// </summary>
        /// <param name="board">The board being displayed</param>
        void DisplayBoard(Board board);

        
    }
}
