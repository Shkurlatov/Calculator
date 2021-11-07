using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConverterLibrary
{
    public class Filter
    {
        public Dictionary<Regex, string> Patterns { get; private set; }

        public Regex AcceptableChars { get; private set; }

        public readonly Regex SpaceInsidePattern = new Regex (@"(?<=[0-9]|\.)\p{Zs}+(?=[0-9]|\.)");
        public readonly Regex AllWhiteSpaces = new Regex(@"\s+");
        public readonly Regex Separators = new Regex(@"([*/+-])");

        public readonly string NullOrEmpty = "The string is null or empty";
        public readonly string WrongSymbol = "The string contains not acceptable symbols";
        public readonly string SpaceInside = "The number value contains the space inside";
        public readonly string BadValue = "Wrong number format, the parsing ended unsuccessefully";
        public readonly string IncorrectBraces = "Incorrect ration of opening and closing braces";

        public Filter()
        {
            Patterns = new Dictionary<Regex, string>
            {
                { new Regex (@"^[*/]"), "Wrong math operator at the begining of the string" },
                { new Regex (@"[*/+-]$"), "The math operator at the end of the string" },
                { new Regex (@"^([*/+-]){2}"), "Two math operators at the begining of the string" },
                { new Regex (@"([*/+-])([*/])"), "Operator of multiplication or division after another math operator" },
                { new Regex (@"([*/+-]){3}"), "Three math operators in a row" }
            };

            AcceptableChars = new Regex(@"(^[0-9.*/+\- ]+$)");
        }
        
        public void AddBracePatterns()
        {
            Patterns.Add(new Regex(@"^\)"), "Closing brace at the begining of the string");
            Patterns.Add(new Regex(@"\($"), "Opening brace at the end of the string");
            Patterns.Add(new Regex(@"([*/+-])(\))"), "The math operator before the closing brace");
            Patterns.Add(new Regex(@"(\()([*/])"), "Operator of multiplication or division after the opening brace");
            Patterns.Add(new Regex(@"(\()([*/+-]){2}"), "Two math operators after the opening brace");
            Patterns.Add(new Regex(@"(\()(\))"), "Missing value inside braces");
            Patterns.Add(new Regex(@"(\))([0-9]|\.|\()"), "Missing math operator after the closing brace");
            Patterns.Add(new Regex(@"([0-9]|\.|\))(\()"), "Missing math operator before the opening brace");

            AcceptableChars = new Regex(@"(^[()0-9.*/+\- ]+$)");
        }
    }
}
