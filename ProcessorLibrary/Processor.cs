using System;
using System.Collections.Generic;
using System.Linq;
using MathUnitsLibrary;

namespace ProcessorLibrary
{
    public class Processor
    {
        public double GetResult(List<MathMember> expression)
        {
            CheckExpression(expression);

            for (int priority = expression.Max(x => x.Priority); priority >= 0; priority--)
            {
                for (int i = 1; i < expression.Count; i++)
                {
                    if (expression[i].Priority == priority)
                    {
                        if (expression[i].Operation == MathOperation.Division && expression[i].Value == 0)
                        {
                            throw new DivideByZeroException();
                        }

                        expression[i - 1].Value = Calculate(expression[i].Operation, expression[i - 1].Value, expression[i].Value);
                        expression.Remove(expression[i]);

                        i--;
                    }
                }
            }

            return expression[0].Value;
        }

        private void CheckExpression(List<MathMember> expression)
        {
            if (expression == null || expression.Any(x => x == null))
            {
                throw new ArgumentNullException($"Math expression or any math member of the expression is null", nameof(expression));
            }

            if (expression[0].Operation != MathOperation.None)
            {
                throw new ArgumentException("The math operation of the first math member in the expression have to be None", nameof(expression));
            }

            if (expression.Where(x => x.Operation == MathOperation.None).Count() != 1)
            {
                throw new ArgumentException("Only the first math member in the expression can have None math operation parameter", nameof(expression));
            }
        }

        private double Calculate(MathOperation operation, double firstValue, double secondValue) => operation switch
        {
            MathOperation.Subtraction => firstValue - secondValue,
            MathOperation.Addition => firstValue + secondValue,
            MathOperation.Division => firstValue / secondValue,
            MathOperation.Multiplication => firstValue * secondValue,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Not expected operation value: {operation}"),
        };
    }
}
