using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using MathUnitsLibrary;

namespace ConverterLibrary
{
    public class InputConverter
    {
        public List<MathMember> MathExpression { get; protected set; }
        public string ServiceMessage { get; protected set; }

        protected Filter _filter;

        protected string _source;

        public InputConverter()
        {
            _filter = new Filter();
        }


        public bool IsExpressionComplete(string source)
        {
            _source = source;

            if (!IsSourceAcceptable())
            {
                return false;
            }

            return IsSourceConverted(SplitSource());
        }

        protected bool IsSourceAcceptable()
        {
            if (string.IsNullOrEmpty(_source))
            {
                ServiceMessage = _filter.NullOrEmpty;
                return false;
            }

            if (!_filter.AcceptableChars.IsMatch(_source))
            {
                ServiceMessage = _filter.WrongSymbol;
                return false;
            }

            if (_filter.SpaceInsidePattern.IsMatch(_source))
            {
                ServiceMessage = _filter.SpaceInside;
                return false;
            }

            _source = _filter.AllWhiteSpaces.Replace(_source, string.Empty);

            foreach (var pattern in _filter.Patterns)
            {
                if (pattern.Key.IsMatch(_source))
                {
                    ServiceMessage = pattern.Value;
                    return false;
                }
            }

            return true;
        }

        protected List<string> SplitSource()
        {
            List<string> sections = _filter.Separators.Split(_source).ToList();

            for (int i = 0; i < sections.Count - 2; i += 2)
            {
                if (sections[i] == string.Empty)
                {
                    if (i == 0)
                    {
                        sections[0] = sections[1] + "1";
                        sections[1] = "*";
                    }
                    else
                    {
                        sections[i - 1] += sections[i + 1];
                        sections.RemoveAt(i + 1);
                        sections.RemoveAt(i);
                    }
                }
            }

            sections.Insert(0, " ");

            return sections;
        }

        protected virtual bool IsSourceConverted(List<string> sections)
        {
            MathExpression = new List<MathMember>();            

            for (int i = 1; i < sections.Count; i += 2)
            {
                if (!IsMathMemberAdded(sections[i], sections[i - 1]))
                {
                    ServiceMessage = _filter.BadValue;
                    return false;
                }
            }

            return true;
        }

        private bool IsMathMemberAdded(string preparedSection, string operation)
        {
            if (operation.Length == 2)
            {
                preparedSection = operation[1] + preparedSection;
            }

            if (decimal.TryParse(preparedSection, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
            {
                MathExpression.Add(new MathMember(value, GetMathOperation(operation[0])));

                return true;
            }

            return false;
        }

        protected MathOperation GetMathOperation(char operation) => operation switch
        {
            ' ' => MathOperation.None,
            '-' => MathOperation.Subtraction,
            '+' => MathOperation.Addition,
            '/' => MathOperation.Division,
            '*' => MathOperation.Multiplication,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Not expected operation value: {operation}"),
        };
    }
}
