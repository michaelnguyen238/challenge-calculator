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
            calculate("10,20,30");
            Console.WriteLine("Expected result: 30");
            // End Unit Tests

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void calculate(string inputString)
        {
            int result = 0;
            int itr = 0;
            int maxNums = 2;
            string[] strTokens = inputString.Split(',');
            
            foreach (string str in strTokens)
            {
                int num;

                if (itr == maxNums)
                {
                    break;
                }
                else if (! int.TryParse(str, out num))
                {
                    num = 0;
                }

                result += num;
                itr++;
            }

            Console.WriteLine("Result: " + result);
        }
    }
}
