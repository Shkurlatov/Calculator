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

        public void PrintFileResult(string fileSavingInfo, string[] results)
        {
            Console.WriteLine(fileSavingInfo);

            Console.WriteLine("The processing results:\n");

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
