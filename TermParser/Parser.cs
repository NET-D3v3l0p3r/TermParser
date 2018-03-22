using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermParser
{
    public class Parser
    {
        public string Term { get; private set; }
        private Parantheses _internalCalculator;

        public Parser(string term)
        {
            Term = "(" + term + ")";
            _internalCalculator = new Parantheses(Term);
        }

        public int Solve()
        {
            _internalCalculator.Resolve();
            return _internalCalculator.Value;
        }
    }
}
