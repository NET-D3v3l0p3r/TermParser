using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermParser
{
    public class Parantheses
    {
        public string Content { get; set; }
        public char[] ContentArray { get; set; }

        public string ParsedContent { get; private set; }
        public string FormattedContent { get; private set; }

        public List<Operator> Operators { get; private set; }
        public List<Parantheses> Paranthesis { get; private set; }

        public double Value { get; private set; }

        public Parantheses(string content)
        {
            Operators = new List<Operator>();
            Paranthesis = new List<Parantheses>();

            Content = content;
            ContentArray = Content.ToCharArray();
            Parse();
            ContentArray = ParsedContent.ToCharArray();
        }

        public void Parse()
        {
            int paranthesisCounter = 1;
            for (int i = 1; i < ContentArray.Length; i++)
            {
                char current = ContentArray[i];
                if (current == '(' && paranthesisCounter < 2)
                {
                    Paranthesis.Add(new Parantheses(Content.Substring(i, Content.Length - i)));
                    paranthesisCounter++;
                }
                else if(current == '(') paranthesisCounter++;


                if (current == ')')
                    paranthesisCounter--;



                if (paranthesisCounter == 0)
                    break;

                ParsedContent += current;


            }

            Formatting();
        }

        public void Resolve()
        {
            if(Paranthesis.Count == 0)
            {

                for (int i = 0; i < ContentArray.Length; i++)
                {
                    char current = ContentArray[i];

                    if (!char.IsDigit(current) && current != ',')
                    {
                        Operators.Add(new Operator(current,
                            Utilities.ExtractDoubleAtOperator(ContentArray, i, false),
                            Utilities.ExtractDoubleAtOperator(ContentArray, i, true)));

                        if (Operators.Count > 1)
                        {
                            Operators[Operators.Count - 1].LeftOperator = Operators[Operators.Count - 2];
                            Operators[Operators.Count - 2].RightOperator = Operators[Operators.Count - 1];

                        }
                    }
                }

                if (Operators.Count == 0)
                {
                    Value = double.Parse(FormattedContent);
                    return;
                }



                List<Operator> operatorBuffer = new List<Operator>(Operators);
                int priorityCounter = 0;


                for (int i = 0; i < Operators.Count; i++)
                {
                    switch (Operators[i].HasPriority)
                    {
                        case true:
                            Operators[i].Process();
                            if (i + 1 < Operators.Count)
                                Operators[i + 1].Left = Operators[i].Result;

                            if (i - 1 >= 0)
                            {
                                if (!Operators[i - 1].HasPriority)
                                    Operators[i - 1].Right = Operators[i].Result;
                                else
                                {
                                    Operators[i - 1].Result = Operators[i].Result;
                                    Operators[i].UpdateLeft();
                                }
                                
                            }

                            priorityCounter++;
                            operatorBuffer.Remove(Operators[i]);
                            break;
                    }
                }

                if (priorityCounter == Operators.Count)
                {
                    Value = Operators[Operators.Count - 1].Result;
                    return;
                }
                


                for (int i = 0; i < operatorBuffer.Count; i++)
                {
                    operatorBuffer[i].Process();
                    if (i + 1 >= operatorBuffer.Count)
                        break;
                    operatorBuffer[i + 1].Left = operatorBuffer[i].Result;
                }

                Value = operatorBuffer[operatorBuffer.Count - 1].Result;
                operatorBuffer.Clear();

                return;
            }

            for (int i = 0; i < Paranthesis.Count; i++)
                Paranthesis[i].Resolve();

            for (int i = 0; i < Paranthesis.Count; i++)
                FormattedContent = FormattedContent.Replace("[" + i + "]", Paranthesis[i].Value + "");
            
   
            ContentArray = FormattedContent.ToCharArray();
            Paranthesis.Clear();
            Resolve();

        }

        

    

        public string ReplaceFormat()
        {
            return "(" + ParsedContent + ")";
        }
        public void Formatting()
        {
            FormattedContent = ParsedContent;
            for (int i = 0; i < Paranthesis.Count; i++)
            {
                Paranthesis[i].Formatting();
                FormattedContent = FormattedContent.Replace(Paranthesis[i].ReplaceFormat(), "[" + i +  "]");
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(FormattedContent + Environment.NewLine);
            for (int i = 0; i < Paranthesis.Count; i++)
            {
                sb.Append("->" + Paranthesis[i] );
            }

            return sb.ToString();
        }





    }
}
