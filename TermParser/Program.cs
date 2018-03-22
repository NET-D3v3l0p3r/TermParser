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
            Parser p = new Parser("2*(3+1)/4-1*(1+15)+15");
            Console.WriteLine(p.Solve());

            Console.Read();

        }
    }
}
