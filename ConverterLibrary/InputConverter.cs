using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using MathUnitsLibrary;

namespace ConverterLibrary
{
    public class InputConverter : Converter
    {
        private const string _acceptableChars = "9876543210/.-+*";

        public override bool IsExpressionComplete(string sourceLine)
        {
            if (!IsCharsAcceptable(sourceLine, _acceptableChars))
            {
                return false;
            }

            List<string> splitedSource = Regex.Split(sourceLine, _separators).ToList();

            for (int i = 0; i < splitedSource.Count; i += 2)
            {
                if (splitedSource[i] == string.Empty)
                {
                    if (i == 0)
                    {
                        if (splitedSource[1] == "-")
                        {
                            splitedSource[0] = "-1";
                            splitedSource[1] = "*";
                        }
                        else
                        {
                            ServiceMessage = _wrongFirst;
                            return false;
                        }
                    }
                    else
                    {
                        ServiceMessage = _missingValue;
                        return false;
                    }
                }
            }

            return IsSourceConverted(splitedSource);
        }

        private bool IsSourceConverted(List<string> sections)
        {
            MathExpression = new List<MathMember>();

            sections.Insert(0, " ");

            for (int i = 1; i < sections.Count; i += 2)
            {
                if (!IsMathMemberAdded(sections[i], sections[i - 1]))
                {
                    ServiceMessage = _badValue;
                    return false;
                }
            }

            return true;
        }

        private bool IsMathMemberAdded(string preparedSection, string operation)
        {
            if (decimal.TryParse(preparedSection, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
            {
                MathExpression.Add(new MathMember(value, GetMathOperation(operation)));

                return true;
            }

            return false;
        }
    }
}
