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
            calculate("20");
            Console.WriteLine("Expected result: 20");
            calculate("1,5000");
            Console.WriteLine("Expected result: 5001");
            calculate("5000,-1");
            Console.WriteLine("Expected result: 4999");
            calculate("--,10");
            Console.WriteLine("Expected result: 10");
            calculate("abc,def");
            Console.WriteLine("Expected result: 0");
            calculate("1,2,3,4,5,6,7,8,9,10,11,12");
            Console.WriteLine("Expected result: 78");
            calculate("1\n2,3");
            Console.WriteLine("Expected result: 6");
            calculate("1\n\n2,,3");
            Console.WriteLine("Expected result: 6");
            // End Unit Tests

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void calculate(string inputString)
        {
            int result = 0;
            char[] delimiters = { ',', '\n' };
            string[] strTokens = inputString.Split(delimiters);
            
            foreach (string str in strTokens)
            {
                int num;

                if (! int.TryParse(str, out num))
                {
                    num = 0;
                }

                result += num;
            }

            Console.WriteLine("Result: " + result);
        }
    }
}
