using System;
using System.Collections.Generic;
using System.Linq;
using MathUnitsLibrary;

namespace ProcessorLibrary
{
    public abstract class Processor
    {
        public abstract string GetResult(List<MathMember> expression);

        protected void CheckExpression(List<MathMember> expression)
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

        protected decimal Calculate(MathOperation operation, decimal firstValue, decimal secondValue) => operation switch
        {
            MathOperation.Subtraction => firstValue - secondValue,
            MathOperation.Addition => firstValue + secondValue,
            MathOperation.Division => firstValue / secondValue,
            MathOperation.Multiplication => firstValue * secondValue,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Not expected operation value: {operation}"),
        };
    }
}
