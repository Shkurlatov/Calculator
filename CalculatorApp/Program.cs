using ConverterLibrary;
using ProcessorLibrary;
using System;

namespace CalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UserConsole console = new UserConsole();
            Processor processor = new Processor();
            InputConverter converter;

            if (args.Length > 0)
            {
                ProcessFileContent();
            }
            else
            {
                ProcessUserInput();
            }

            void ProcessFileContent()
            {
                FileHandling fileHandling = new FileHandling();
                converter = new FileConverter();

                string[] content = fileHandling.ReadContent(args);
                string[] result = new string[content.Length];

                for (int i = 0; i < content.Length; i++)
                {
                    result[i] = CalculationResult(content[i]);
                }

                console.PrintFileResult(fileHandling.WriteResult(result), result);
            }

            void ProcessUserInput()
            {
                converter = new InputConverter();

                string input = console.GetInput();

                while (input != console.ExitKey)
                {
                    console.PrintInputResult(CalculationResult(input));

                    input = console.GetInput();
                }
            }

            string CalculationResult(string line)
            {
                string calculationResult = line + " = ";

                bool isExpressionComplete = converter.IsExpressionComplete(line);

                if (isExpressionComplete)
                {
                    var mathExpression = converter.MathExpression;
                    
                    try
                    {
                        return calculationResult += processor.GetResult(mathExpression).ToString();
                    }
                    catch (DivideByZeroException)
                    {
                        return calculationResult += "The result is not achievable, division by zero occured";
                    }
                }
                else
                {
                    return calculationResult += converter.ServiceMessage;
                }
            }
        }
    }
}
