using System;

namespace CalculatorApp
{
    class UserConsole
    {
        public readonly string ExitKey = "exit";

        public string GetInput()
        {
            Console.WriteLine("Please, enter your math expression or type \"" + ExitKey + "\" to end the program");

            return Console.ReadLine();
        }

        public void PrintInputResult(string result)
        {
            Console.WriteLine(result + "\n");
        }

        public void PrintFileResults(bool isResultsSaved, string[] results)
        {
            if (!isResultsSaved)
            {
                Console.WriteLine("\nFailed to save the file with the processing results\n");
            }
            else
            {
                Console.WriteLine("\nThe file with the processing results has been succesefully saved\n");
            }

            Console.WriteLine("The processing results:\n");

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
