using System.Collections.Generic;
using System.Linq;
using MathUnitsLibrary;

namespace ConverterLibrary
{
    public class FileConverter : InputConverter
    {
        public FileConverter()
        {
            _filter.AddBracePatterns();
        }


        protected override bool IsSourceConverted(List<string> sections)
        {
            MathExpression = new List<MathMember>();

            int priorityLayer = 0;
            int nextPriorityLayer = 0;

            for (int i = 1; i < sections.Count; i += 2)
            {
                if (sections[i].Last() == '(')
                {
                    sections[i] += sections[i + 1] + "1";
                    sections[i + 1] = "*";
                }

                int indent = 0;
                int length = sections[i].Length;

                while (sections[i][length - 1] == ')')
                {
                    length--;
                    nextPriorityLayer--;
                }

                while (sections[i][indent] == '(')
                {
                    indent++;
                    length--;
                    nextPriorityLayer++;
                }

                if (!TryAddMathMember(sections[i].Substring(indent, length), sections[i - 1], priorityLayer))
                {
                    ServiceMessage = _filter.BadValue;
                    return false;
                }

                priorityLayer = nextPriorityLayer;
            }

            if (priorityLayer != 0)
            {
                ServiceMessage = _filter.IncorrectBraces;
                return false;
            }

            return true;
        }

        private bool TryAddMathMember(string preparedSection, string operation, int priority)
        {
            if (double.TryParse(preparedSection, out double value))
            {
                MathExpression.Add(new MathMember(value, GetMathOperation(operation[0]), priority));

                if (operation.Length == 2 && operation[1] == '-')
                {
                    MathExpression.Last().MakeNegative();
                }

                return true;
            }

            return false;
        }
    }
}
