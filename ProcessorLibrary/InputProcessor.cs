using System.Collections.Generic;
using System.Globalization;
using MathUnitsLibrary;

namespace ProcessorLibrary
{
    public class InputProcessor : Processor
    {
        public override string GetResult(List<MathMember> expression)
        {
            CheckExpression(expression);

            for (MathOperation operation = MathOperation.Multiplication; operation > MathOperation.None; operation -= 2)
            {
                for (int i = 0; i < expression.Count; i++)
                {
                    if (expression[i].Operation == operation || expression[i].Operation == operation - 1)
                    {
                        if (expression[i].Operation == MathOperation.Division && expression[i].Value == 0)
                        {
                            return "The result is not achievable, division by zero occured";
                        }

                        expression[i - 1].Value = Calculate(expression[i].Operation, expression[i - 1].Value, expression[i].Value);
                        expression.Remove(expression[i]);

                        i--;
                    }
                }
            }

            return expression[0].Value.ToString();
        }
    }
}
