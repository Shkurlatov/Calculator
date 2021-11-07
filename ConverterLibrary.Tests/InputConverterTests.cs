using System.Collections.Generic;
using Xunit;
using MathUnitsLibrary;

namespace ConverterLibrary.Tests
{
    public class InputConverterTests
    {
        [Theory]
        [MemberData(nameof(ExpressionFailedTestsData))]
        public void IsExpressionComplete_TakesVariousStrings_ReturnsFalse(string sourceLine, string message)
        {
            // arrange
            InputConverter converter = new InputConverter();

            // act
            bool isExpressionComplete = converter.IsExpressionComplete(sourceLine);

            // assert
            Assert.False(isExpressionComplete);
            Assert.Equal(message, converter.ServiceMessage);
        }

        [Theory]
        [MemberData(nameof(ExpressionCompleteTestsData))]
        public void IsExpressionComplete_TakesVariousStrings_ReturnsTrue(string sourceLine, List<MathMember> expression)
        {
            // arrange
            InputConverter converter = new InputConverter();

            // act
            bool isExpressionComplete = converter.IsExpressionComplete(sourceLine);

            // assert
            Assert.True(isExpressionComplete);
            Assert.Equal(expression, converter.MathExpression);
            Assert.Null(converter.ServiceMessage);
        }

        public static IEnumerable<object[]> ExpressionFailedTestsData()
        {
            yield return new object[] { null, "The string is null or empty" };
            yield return new object[] { "", "The string is null or empty" };
            yield return new object[] { "1 ( 2", "The string contains not acceptable symbols" };
            yield return new object[] { "1  2 + 12", "The number value contains the space inside" };
            yield return new object[] { "1 + 2.2.2", "Wrong number format, the parsing ended unsuccessefully" };
            yield return new object[] { "* 1 + 2", "Wrong math operator at the begining of the string" };
            yield return new object[] { "1 + 2 +", "The math operator at the end of the string" };
            yield return new object[] { "+ + 1 + 2", "Two math operators at the begining of the string" };
            yield return new object[] { "1 + * 2", "Operator of multiplication or division after another math operator" };
            yield return new object[] { "1 + + + 2", "Three math operators in a row" };
        }

        public static IEnumerable<object[]> ExpressionCompleteTestsData()
        {
            yield return new object[]
            {
                "2++2",
                new List<MathMember>
                {
                    new MathMember(2, MathOperation.None, 0),
                    new MathMember(2, MathOperation.Addition, 0),
                }
            };
            yield return new object[]
            {
                "2*+2",
                new List<MathMember>
                {
                    new MathMember(2, MathOperation.None, 0),
                    new MathMember(2, MathOperation.Multiplication, 0),
                }
            };
            yield return new object[]
            {
                "1.2 - 4.8 + 8.05 / 5.115 * 3",
                new List<MathMember>
                {
                    new MathMember(1.2m, MathOperation.None),
                    new MathMember(4.8m, MathOperation.Subtraction),
                    new MathMember(8.05m, MathOperation.Addition),
                    new MathMember(5.115m, MathOperation.Division),
                    new MathMember(3, MathOperation.Multiplication)
                }
            };
            yield return new object[]
            {
                "- 1 - 2 + 4 / 1 * 2 + 1",
                new List<MathMember>
                {
                    new MathMember(-1, MathOperation.None),
                    new MathMember(1, MathOperation.Multiplication),
                    new MathMember(2, MathOperation.Subtraction),
                    new MathMember(4, MathOperation.Addition),
                    new MathMember(1, MathOperation.Division),
                    new MathMember(2, MathOperation.Multiplication),
                    new MathMember(1, MathOperation.Addition)
                }
            };
            yield return new object[]
            {
                "4 - 2 / 2 + 8 / 4 * 5 * 3",
                new List<MathMember>
                {
                    new MathMember(4, MathOperation.None),
                    new MathMember(2, MathOperation.Subtraction),
                    new MathMember(2, MathOperation.Division),
                    new MathMember(8, MathOperation.Addition),
                    new MathMember(4, MathOperation.Division),
                    new MathMember(5, MathOperation.Multiplication),
                    new MathMember(3, MathOperation.Multiplication)
                }
            };
        }
    }
}
