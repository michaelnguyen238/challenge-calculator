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
            calculate("1,5000");
            Console.WriteLine("Expected result: 1");
            calculate("5000,\n1,4,");
            Console.WriteLine("Expected result: 5");
            calculate("2,1001,6");
            Console.WriteLine("Expected result: 8");
            // End Unit Tests

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void calculate(string inputString)
        {
            int result = 0;
            char[] delimiters = { ',', '\n' };
            string[] strTokens = inputString.Split(delimiters);
            List<int> negativeNums = new List<int>(); 
            
            foreach (string str in strTokens)
            {
                int num;

                if (! int.TryParse(str, out num) || num > 1000)
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
