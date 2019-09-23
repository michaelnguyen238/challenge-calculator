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
            calculate("//[***]\n11***22***33");
            Console.WriteLine("Expected result: 66"); // With brackets
            calculate("//;\n2;5");
            Console.WriteLine("Expected result: 7"); // Without brackets (Single length delimiter only)
            calculate("//[;;;\n1,5000;2,3");
            Console.WriteLine("Expected result: 4"); // Without both brackets (; nor ;;; are recognized as delimiters, so the defaults are used)
            calculate("//[;;;,2,abc;2,3"); 
            Console.WriteLine("Expected result: 5"); // Does not follow format, no \n which results in bad tokens, { //[;;; } and { abc;2 }, so only 2 + 3 is evaluated
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
                    string delimiter = inputString.Substring(2, delimEndIndex - 2);
                    int leftBracketIndex = delimiter.IndexOf('[');
                    int rightBracketIndex = delimiter.IndexOf(']');
                    if (leftBracketIndex == 0 && rightBracketIndex >= 0)
                    {
                        delimiter = delimiter.Substring(1, rightBracketIndex - 1);
                    }
                    delimiters.Add(delimiter);
                }
            }

            string[] strTokens = inputString.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            List<int> negativeNums = new List<int>();

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
            }

            if (negativeNums.Count > 0)
            {
                throw new Exception("Negative numbers are not allowed: " + String.Join(", ", negativeNums.ToArray()));
            }

            Console.WriteLine("Result: " + result);
        }
    }
}
