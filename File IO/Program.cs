using File_IO.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_IO
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParsing fileParsing = new FileParsing();
            fileParsing.ParseFile(args[0]);
        }
    }
}
