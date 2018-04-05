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
        public double Left { get; set; }
        public double Right { get; set; }

        public Operator LeftOperator { get; set; }
        public Operator RightOperator { get; set; }


        public char OperatorChar { get; set; }

        public Utilities.Priority Priority { get; set; }
        public bool HasPriority { get; set; }

        public double Result { get; set; }


        public Operator(char op, double l, double r)
        {
            OperatorChar = op;
            HasPriority = OperatorChar == '*' || OperatorChar == '/';

            Left = l;
            Right = r;

        }

        public void UpdateLeft()
        {
            if (LeftOperator == null)
                return;

            if (!HasPriority)
                return;

            if (LeftOperator.HasPriority)
            {
                LeftOperator.Result = Result;
                LeftOperator.UpdateLeft();
            }
            else LeftOperator.Right = Result;
        }

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
                case '^':
                    Result = Math.Pow(Left, Right);
                    break;
            }

           
        }
        
    }
}
