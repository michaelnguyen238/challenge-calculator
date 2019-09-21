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
            Console.WriteLine("Expected result: 7");
            calculate("//;;;\n1,5000;2");
            Console.WriteLine("Expected result: Error, delimiter is too long");
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
                string delimiter = inputString.Substring(2, delimEndIndex - 2);
                if (delimiter.Length > 1)
                {
                    Console.WriteLine("The delimiter is too long: " + delimiter);
                    return;
                }
                delimiters.Add(delimiter);
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
