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

        public int Value { get; private set; }

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

                    if (!char.IsDigit(current))
                    {
                        Operators.Add(new Operator(current, ExtractIntegerAtOperator(i, false), ExtractIntegerAtOperator(i, true)));
                        if(Operators.Count > 1)
                        {
                            Operators[Operators.Count - 1].LeftOperator = Operators[Operators.Count - 2];
                            Operators[Operators.Count - 2].RightOperator = Operators[Operators.Count - 1];

                        }
                    }
                }

                if (Operators.Count == 0)
                {

                    Value = int.Parse(FormattedContent);
                    return;
                }


                List<Operator> lastPriority = new List<Operator>(Operators);
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
                                if (!Operators[i - 1].HasPriority)
                                    Operators[i - 1].Right = Operators[i].Result;

                            priorityCounter++;
                            lastPriority.Remove(Operators[i]);
                            break;
                    }
                }
                if(priorityCounter == Operators.Count)
                {
                    Value = Operators[Operators.Count - 1].Result;
                    lastPriority.Clear();
                    return;
                }

                for (int i = 0; i < lastPriority.Count; i++)
                {
                    lastPriority[i].Process();
                    if (i + 1 >= lastPriority.Count)
                        break;
                    lastPriority[i + 1].Left = lastPriority[i].Result;
                }

                Value = lastPriority[lastPriority.Count - 1].Result;
                lastPriority.Clear();
                return;
            }

            for (int i = 0; i < Paranthesis.Count; i++)
                Paranthesis[i].Resolve();

            for (int i = 0; i < Paranthesis.Count; i++)
            {
                FormattedContent = FormattedContent.Replace("[" + i + "]", Paranthesis[i].Value + "");
            }
   
            ContentArray = FormattedContent.ToCharArray();
            Paranthesis.Clear();
            Resolve();

        }

        

        public int ExtractIntegerAtOperator(int index, bool right)
        {
            string extractedString = "";

            switch (right)
            {
                case true:
                    
                    for (int i = index + 1; i < ContentArray.Length; i++)
                    {
                        char currentChar = ContentArray[i];

                        if (!char.IsDigit(currentChar))
                            break;

                        extractedString += currentChar;
                    }


                    break;

                case false:

                    for (int i = index - 1; i >= 0; i--)
                    {
                        char currentChar = ContentArray[i];

                        if (!char.IsDigit(currentChar))
                            break;

                        extractedString += currentChar;
                    }

                    extractedString = new string(extractedString.Reverse<char>().ToArray());

                    break;
            }

            return int.Parse(extractedString);
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
