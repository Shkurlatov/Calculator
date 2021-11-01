using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using MathUnitsLibrary;

namespace ConverterLibrary
{
    public class FileConverter : Converter
    {
        private const string _acceptableChars = "9876543210/.-+*)(";


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
                else
                {
                    if (splitedSource[i].Last() == '(')
                    {
                        if (splitedSource[i + 1] != null && splitedSource[i + 1] == "-")
                        {
                            splitedSource[i] = splitedSource[i] + "-1";
                            splitedSource[i + 1] = "*";
                        }
                        else
                        {
                            ServiceMessage = _wrongAfterBrace;
                            return false;
                        }
                    }
                }
            }

            return IsSourceConverted(splitedSource);
        }

        private bool IsSourceConverted(List<string> sections)
        {
            MathExpression = new List<MathMember>();

            int priorityLayer = 0;
            int nextPriorityLayer = 0;

            sections.Insert(0, " ");

            for (int i = 1; i < sections.Count; i += 2)
            {
                int indent = 0;
                int length = sections[i].Length;

                while (sections[i][indent] == '(')
                {
                    indent++;
                    length--;
                    nextPriorityLayer++;
                }

                while (sections[i][length - 1] == ')')
                {
                    length--;
                    nextPriorityLayer--;

                    if (indent == length)
                    {
                        ServiceMessage = _braceInsteadValue;
                        return false;
                    }
                }

                if (!IsMathMemberAdded(sections[i].Substring(indent, length), sections[i - 1], priorityLayer))
                {
                    ServiceMessage = _badValue;
                    return false;
                }

                priorityLayer = nextPriorityLayer;
            }

            if (priorityLayer != 0)
            {
                ServiceMessage = _incorrectBraces;
                return false;
            }

            return true;
        }

        private bool IsMathMemberAdded(string preparedSection, string operation, int priority)
        {
            if (decimal.TryParse(preparedSection, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
            {
                MathExpression.Add(new MathMember(value, GetMathOperation(operation), priority));

                return true;
            }

            return false;
        }
    }
}
