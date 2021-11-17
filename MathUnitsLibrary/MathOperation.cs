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
        Multiplication,
        [Priority(2)]
        Exponentiation
    }

    public static class OperationAttributes
    {
        public static T Get<T>(Enum enumValue) where T : Attribute
        {
            return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<T>();
        }
    }
}
