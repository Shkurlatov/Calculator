﻿using ConverterLibrary;
using ProcessorLibrary;

namespace CalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UserConsole console = new UserConsole();
            Converter converter;
            Processor processor;

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
                processor = new FileProcessor();

                string[] content = fileHandling.ReadContent(args);
                string[] results = new string[content.Length];

                for (int i = 0; i < content.Length; i++)
                {
                    results[i] = CalculationResult(content[i]);
                }

                console.PrintFileResults(fileHandling.IsResultSaved(results), results);
            }

            void ProcessUserInput()
            {
                converter = new InputConverter();
                processor = new InputProcessor();

                string input = console.GetInput();

                while (input != console.ExitKey)
                {
                    console.PrintInputResult(CalculationResult(input));

                    input = console.GetInput();
                }
            }

            string CalculationResult(string line)
            {
                if (converter.IsExpressionComplete(line))
                {
                    return line + " = " + processor.GetResult(converter.MathExpression);
                }
                else
                {
                    return line + " = " + converter.ServiceMessage;
                }
            }
        }
    }
}
