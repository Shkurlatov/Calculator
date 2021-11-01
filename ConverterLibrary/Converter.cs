using System;
using System.Collections.Generic;
using MathUnitsLibrary;

namespace ConverterLibrary
{
    public abstract class Converter
    {
        public List<MathMember> MathExpression { get; protected set; }
        public string ServiceMessage { get; protected set; }

        protected const string _nullOrEmpty = "The string is null or empty";
        protected const string _wrongSymbol = "The string contains not acceptable symbols";
        protected const string _wrongFirst = "Wrong math operator at the begining of the string";
        protected const string _missingValue = "Missing value after math operation";
        protected const string _badValue = "Wrong number format, the parsing ended unsuccessefully";
        protected const string _wrongAfterBrace = "Wrong math operator after the brace";
        protected const string _braceInsteadValue = "The close brace instead of the value";
        protected const string _incorrectBraces = "Incorrect ration of opening and closing braces";
        protected const string _separators = @"(\*|\/|\+|\-)";


        public abstract bool IsExpressionComplete(string sourceLine);

        protected bool IsCharsAcceptable(string line, string acceptables)
        {
            if (string.IsNullOrEmpty(line))
            {
                ServiceMessage = _nullOrEmpty;
                return false;
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (!acceptables.Contains(line[i]))
                {
                    ServiceMessage = _wrongSymbol;
                    return false;
                }
            }

            return true;
        }

        protected MathOperation GetMathOperation(string operation) => operation switch
        {
            " " => MathOperation.None,
            "-" => MathOperation.Subtraction,
            "+" => MathOperation.Addition,
            "/" => MathOperation.Division,
            "*" => MathOperation.Multiplication,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Not expected operation value: {operation}"),
        };
    }
}
