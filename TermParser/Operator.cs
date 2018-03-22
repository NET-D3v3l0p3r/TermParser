using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermParser
{
    // (2*(1+1))*(2+3)
    public class Operator
    {
        public int Left { get; set; }
        public int Right { get; set; }

        public Operator LeftOperator { get; set; }
        public Operator RightOperator { get; set; }

        public char OperatorChar { get; set; }
        public bool HasPriority { get; set; }

        public int Result { get; private set; }

        public Operator(char op, int l, int r)
        {

            OperatorChar = op;
            HasPriority = OperatorChar == '*' || OperatorChar == '/';

            Left = l;
            Right = r;

        }

        #region dump
        //public void Solve()
        //{
        //    // (5 + 5 * 5 / 5 * 5 + 5)

        //    if (!HasPriority && RightOperator.HasPriority)
        //    {
        //        RightOperator.Solve();
        //    }
        //    else if (!HasPriority && !RightOperator.HasPriority)
        //    {
        //        Process();
        //        RightOperator.Left = Result;
        //        RightOperator.Solve();

        //    }

        //    if (HasPriority)
        //    {
        //        Process();

        //        RightOperator.Left = Result;
        //        RightOperator.Solve();
        //    }

        //    if(!HasPriority)
        //    {

        //    }

        //}

        #endregion

        public void Process()
        {
            switch (OperatorChar)
            {
                case '*':
                    Result = Left * Right;
                    break;

                case '/':
                    Result = Left / Right;
                    break;

                case '+':
                    Result = Left + Right;
                    break;

                case '-':
                    Result = Left - Right;
                    break;
            }

           
        }
        
    }
}
