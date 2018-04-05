using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermParser
{
    public static class Utilities
    {
        public static double ExtractDoubleAtOperator(char[] array, int index, bool right)
        {
            string extractedString = "";

            switch (right)
            {
                case true:

                    for (int i = index + 1; i < array.Length; i++)
                    {
                        char currentChar = array[i];

                        if (!char.IsDigit(currentChar) && currentChar != ',')
                            break;

                        extractedString += currentChar;
                    }


                    break;

                case false:

                    for (int i = index - 1; i >= 0; i--)
                    {
                        char currentChar = array[i];

                        if (!char.IsDigit(currentChar) && currentChar != ',')
                            break;

                        extractedString += currentChar;
                    }

                    extractedString = new string(extractedString.Reverse<char>().ToArray());

                    break;
            }

            return double.Parse(extractedString);
        }

        public enum Priority
        {
            High,
            VeryHigh
        }
    }
}
