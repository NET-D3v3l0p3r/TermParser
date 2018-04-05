using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermParser
{
    class Program
    {
        static void Main(string[] args)
        {
 
            Parser p = new Parser(Console.ReadLine());
            Console.WriteLine(p.Solve());

            Console.Read();

        }
    }
}
