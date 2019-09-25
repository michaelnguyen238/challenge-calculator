using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {

            // Unit Tests
            calculate("//;\n2;5");
            Console.WriteLine("Expected result: 7"); // Without brackets (Single length delimiter only)
            calculate("//[*][!!][r9r]\n11r9r22*33!!44");
            Console.WriteLine("Expected result: 110"); // Multiple delimiters with brackets
            calculate("//[***]\n11***22***33");
            Console.WriteLine("Expected result: 66"); // With brackets            
            calculate("//[;;;\n1,5000;2,3");
            Console.WriteLine("Expected result: 4"); // Without both brackets (; nor ;;; are recognized as delimiters, so the defaults are used)
            calculate("//[;;;,2,abc;2,3");
            Console.WriteLine("Expected result: 5"); // Does not follow format, no \n which results in bad tokens, { //[;;; } and { abc;2 }, so only 2 + 3 is evaluated
            calculate("//[*][!!][r9r]\n11r9r22*33!!44");
            Console.WriteLine("Expected result: 110"); // Multiple delimiters with brackets
            calculate("//[[*]][!!][r9r]\n11r9r22*33!!44");
            Console.WriteLine("Expected result: Error, nested brackets not allowed"); // Reject input strings that contain nested bracket delimiters
            // End Unit Tests

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void calculate(string inputString)
        {
            int result = 0;
            List<string> delimiters = new List<string>() { ",", "\n" };
            if (inputString.Length == 0)
            {
                Console.WriteLine("Nothing to calculate!");
                return;
            }

            // Check for custom delimiter, add it to delimiters if one exists
            if (inputString.Length >= 4 && inputString.Substring(0, 2) == "//")
            {
                int delimEndIndex = inputString.IndexOf('\n');
                if (delimEndIndex > -1)
                {
                    string fullDelimiter = inputString.Substring(2, delimEndIndex - 2);
                    string delimiter = fullDelimiter;
                    int leftBracketIndex = fullDelimiter.IndexOf('[');
                    int rightBracketIndex = fullDelimiter.IndexOf(']');

                    while (leftBracketIndex >= 0 && rightBracketIndex > leftBracketIndex)
                    {
                        delimiter = fullDelimiter.Substring(leftBracketIndex + 1, rightBracketIndex - leftBracketIndex - 1);
                        if (delimiter.Contains("["))
                        {
                            Console.WriteLine("Calculator does not support nested brackets");
                            return;
                        }
                        
                        delimiters.Add(delimiter);
                        leftBracketIndex = fullDelimiter.IndexOf('[', rightBracketIndex);
                        rightBracketIndex = fullDelimiter.IndexOf(']', rightBracketIndex + 1);
                    }

                    // Update inputString to remove delimiter section
                    inputString = inputString.Substring(delimEndIndex + 1, inputString.Length - delimEndIndex - 1);

                    // If there are no bracketed delimiters use the full delimiter
                    if (fullDelimiter == delimiter)
                    {
                        delimiters.Add(fullDelimiter);
                    }
                }
            }

            string[] strTokens = inputString.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            List<int> negativeNums = new List<int>();
            string displayFormula = "";

            foreach (string str in strTokens)
            {
                int num;

                if (!int.TryParse(str, out num) || num > 1000)
                {
                    num = 0;
                }
                else if (num < 0)
                {
                    negativeNums.Add(num);
                    continue;
                }

                result += num;
                displayFormula += num + "+";
            }

            if (negativeNums.Count > 0)
            {
                throw new Exception("Negative numbers are not allowed: " + String.Join(", ", negativeNums.ToArray()));
            }

            
            if (displayFormula.Length > 1)
            {
                Console.WriteLine("Result: " + displayFormula.Substring(0, displayFormula.Length - 1) + " = " + result);
            }
            else
            {
                Console.WriteLine("Result: " + result);
            }
            
        }
    }
}
