using System;
using System.Collections.Generic;
using System.Linq;
using MathUnitsLibrary;

namespace ConverterLibrary
{
    public class InputConverter
    {
        public List<MathMember> MathExpression { get; protected set; }
        public string ServiceMessage { get; protected set; }

        protected Filter _filter;

        public InputConverter()
        {
            _filter = new Filter();
        }


        public bool IsExpressionComplete(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("IsExpressionComplete received a null argument!");
            }

            source = _filter.ExtraWhiteSpaces.Replace(source, string.Empty);

            foreach (var pattern in _filter.Patterns)
            {
                if (pattern.Key.IsMatch(source))
                {
                    ServiceMessage = pattern.Value;
                    return false;
                }
            }

            return IsSourceConverted(SplitSource(source));
        }

        protected List<string> SplitSource(string source)
        {
            List<string> sections = _filter.Separators.Split(source).ToList();

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
                if (!TryAddMathMember(sections[i], sections[i - 1]))
                {
                    ServiceMessage = _filter.BadValue;
                    return false;
                }
            }

            return true;
        }

        private bool TryAddMathMember(string preparedSection, string operation)
        {
            if (operation.Length == 2)
            {
                preparedSection = operation[1] + preparedSection;
            }

            if (double.TryParse(preparedSection, out double value))
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
