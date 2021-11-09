using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConverterLibrary
{
    public class Filter
    {
        public Dictionary<Regex, string> Patterns { get; private set; }

        public Regex ExtraWhiteSpaces { get; private set; }

        public readonly Regex Separators = new Regex(@"([*/+-])");

        public readonly string BadValue = "Invalid expression format, the processing ended unsuccessefully";
        public readonly string IncorrectBraces = "Incorrect ration of opening and closing braces";

        public Filter()
        {
            Patterns = new Dictionary<Regex, string>
            {
                { new Regex (@"^\s*$"), "" },
                { new Regex (@"^[*/]"), "Wrong math operator at the begining of the string" },
                { new Regex (@"[*/+-]$"), "The math operator at the end of the string" },
                { new Regex (@"^([*/+-]){2}"), "Two math operators at the begining of the string" },
                { new Regex (@"([*/+-])([*/])"), "Operator of multiplication or division after another math operator" },
                { new Regex (@"([*/+-]){3}"), "Three math operators in a row" }
            };

            ExtraWhiteSpaces = new Regex(@"(?<=[*/+-])\s*|\s*(?=[*/+-])");
        }

        public void AddBracePatterns()
        {
            Patterns.Add(new Regex(@"^\)"), "Closing brace at the begining of the string");
            Patterns.Add(new Regex(@"\($"), "Opening brace at the end of the string");
            Patterns.Add(new Regex(@"([*/+-])(\))"), "The math operator before the closing brace");
            Patterns.Add(new Regex(@"(\()([*/])"), "Operator of multiplication or division after the opening brace");
            Patterns.Add(new Regex(@"(\()([*/+-]){2}"), "Two math operators after the opening brace");
            Patterns.Add(new Regex(@"(\()(\))"), "Missing value inside braces");
            Patterns.Add(new Regex(@"(\))([^)*/+-])"), "Missing math operator after the closing brace");
            Patterns.Add(new Regex(@"([^(*/+-])(\()"), "Missing math operator before the opening brace");

            ExtraWhiteSpaces = new Regex(@"(?<=[()*/+-])\s*|\s*(?=[()*/+-])");
        }
    }
}
