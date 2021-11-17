using System;
using System.Linq;
using System.Reflection;

namespace MathUnitsLibrary
{
    public enum MathOperation
    {
        [Priority(0)]
        None,
        [Priority(0)]
        Subtraction,
        [Priority(0)]
        Addition,
        [Priority(1)]
        Division,
        [Priority(1)]
        DivisionNegative,
        [Priority(1)]
        Multiplication,
        [Priority(1)]
        MultiplicationNegative,
        [Priority(2)]
        Exponentiation,
        [Priority(2)]
        ExponentiationNegative
    }

    public static class OperationAttributes
    {
        public static T Get<T>(Enum enumValue) where T : Attribute
        {
            return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<T>();
        }
    }
}
