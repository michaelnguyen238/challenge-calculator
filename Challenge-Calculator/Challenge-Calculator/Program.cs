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
            Tuple<string, bool, int> arguments = defineArguments();
            displayHelp(arguments.Item1);

            bool continueCalculating = true;
            string inputString = "";

            while (continueCalculating)
            {
                Console.Write("Enter formula: ");
                inputString = Console.ReadLine();                
                if (inputString != null)
                {
                    inputString = inputString.Replace("\\n", "\n");
                    if (inputString.ToLower() == "help")
                    {
                        displayHelp(arguments.Item1);
                    }
                    else if (inputString.ToLower() == "args")
                    {
                        arguments = defineArguments();
                    }
                    else
                    {
                        calculate(inputString, arguments);
                    }
                }                                    
            }            
        }

        static void displayHelp(string alternateDelim)
        {
            Console.WriteLine("Enter a string of the format:");
            Console.WriteLine("(Use default delimiters of ',' and user-defined delimiter)    {numbers}");
            Console.WriteLine("(Single length delimiter)                //{delimiter}\\n{numbers}");
            Console.WriteLine("(Any length delimiter)                   //[{delimiter}]\\n{numbers}");
            Console.WriteLine("(Multiple delimiters of any length)      //[{delimiter1}][{delimiter2}]...\\n{numbers}\n");
            Console.WriteLine("Enter 'help' instead of a formula to display these instructions again. Enter 'args' to change arguments.\nUse ctrl+c to exit.\n");
        }

        static Tuple<string,bool,int> defineArguments()
        {
            string altDelimiter = "";
            bool allowNegatives = false;
            int upperBound;
            // Define arguments
            Console.Write("Enter an alternate delimiter (single character) [\\n]: ");
            altDelimiter = Console.ReadLine();
            if (altDelimiter == "" || altDelimiter.Length > 1)
            {
                altDelimiter = "\n";
            }
            Console.Write("Allow negatives? [y/N]: ");
            if (Console.ReadLine().ToLower() == "y")
            {
                allowNegatives = true;
            }
            Console.Write("Define number limit [1000]: ");
            if (!int.TryParse(Console.ReadLine(), out upperBound))
            {
                upperBound = 1000;
            }

            return new Tuple<string, bool, int>(altDelimiter, allowNegatives, upperBound);
        }

        static void calculate(string inputString, Tuple<string,bool,int> arguments)
        {
            string altDelimiter = arguments.Item1;
            bool allowNegatives = arguments.Item2;
            int upperBound = arguments.Item3;

            int result = 0;
            List<string> delimiters = new List<string>() { ",", altDelimiter };
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

                if (!int.TryParse(str, out num) || num > upperBound)
                {
                    num = 0;
                }
                else if (!allowNegatives && num < 0)
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
