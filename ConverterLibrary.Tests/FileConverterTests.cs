using System.Collections.Generic;
using Xunit;
using MathUnitsLibrary;

namespace ConverterLibrary.Tests
{
    public class FileConverterTests
    {
        [Theory]
        [MemberData(nameof(ExpressionFailedTestsData))]
        public void IsExpressionComplete_TakesVariousStrings_ReturnsFalse(string sourceLine, string message)
        {
            // arrange
            InputConverter converter = new FileConverter();

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
            InputConverter converter = new FileConverter();

            // act
            bool isExpressionComplete = converter.IsExpressionComplete(sourceLine);

            // assert
            Assert.True(isExpressionComplete);
            Assert.Equal(expression, converter.MathExpression);
            Assert.Null(converter.ServiceMessage);
        }

        [Theory]
        [MemberData(nameof(ExpressionUndefinedTestsData))]
        public void IsExpressionComplete_ConvertVariousNumberFormats_ReturnsLocalCultureResult(string sourceLine, string formatExample, List<MathMember> expression)
        {
            // arrange
            InputConverter converter = new FileConverter();
            string message = "Invalid expression format, the processing ended unsuccessefully";

            // act
            bool isExpressionComplete = converter.IsExpressionComplete(sourceLine);

            // assert
            if (decimal.TryParse(formatExample, out decimal value))
            {
                Assert.True(isExpressionComplete);
                Assert.Equal(expression, converter.MathExpression);
            }
            else
            {
                Assert.False(isExpressionComplete);
                Assert.Equal(message, converter.ServiceMessage);
            }
        }

        public static IEnumerable<object[]> ExpressionFailedTestsData()
        {
            yield return new object[] { "", "" };
            yield return new object[] { "   ", "" };
            yield return new object[] { "* 1 + 2", "Wrong math operator at the begining of the string" };
            yield return new object[] { "1 + 2 +", "The math operator at the end of the string" };
            yield return new object[] { "+ + 1 + 2", "Two math operators at the begining of the string" };
            yield return new object[] { "1 + * 2", "Operator of multiplication or division after another math operator" };
            yield return new object[] { "1 + + + 2", "Three math operators in a row" };
            yield return new object[] { ")1 + 2)", "Closing brace at the begining of the string" };
            yield return new object[] { "(1 + 2(", "Opening brace at the end of the string" };
            yield return new object[] { "(1 + 2 +)", "The math operator before the closing brace" };
            yield return new object[] { "(* 1 + 2)", "Operator of multiplication or division after the opening brace" };
            yield return new object[] { "(+ + 1 + 2)", "Two math operators after the opening brace" };
            yield return new object[] { "( )", "Missing value inside braces" };
            yield return new object[] { "(1 + 2) 3", "Missing math operator after the closing brace" };
            yield return new object[] { "1 (2 + 3)", "Missing math operator before the opening brace" };
        }

        public static IEnumerable<object[]> ExpressionCompleteTestsData()
        {
            yield return new object[]
            {
                "2++2",
                new List<MathMember>
                {
                    new MathMember(2, MathOperation.None, 0),
                    new MathMember(2, MathOperation.Addition, 0)
                }
            };
            yield return new object[]
            {
                "2*+2",
                new List<MathMember>
                {
                    new MathMember(2, MathOperation.None, 0),
                    new MathMember(2, MathOperation.Multiplication, 0)
                }
            };
            yield return new object[]
            {
                "(+2)+3",
                new List<MathMember>
                {
                    new MathMember(1, MathOperation.None, 0),
                    new MathMember(2, MathOperation.Multiplication, 1),
                    new MathMember(3, MathOperation.Addition, 0)
                }
            };
            yield return new object[]
            {
                "- (- 1 - (2 + 4) / 1 * (2 + 1))",
                new List<MathMember>
                {
                    new MathMember(-1, MathOperation.None, 0),
                    new MathMember(-1, MathOperation.Multiplication, 0),
                    new MathMember(1, MathOperation.Multiplication, 1),
                    new MathMember(2, MathOperation.Subtraction, 1),
                    new MathMember(4, MathOperation.Addition, 2),
                    new MathMember(1, MathOperation.Division, 1),
                    new MathMember(2, MathOperation.Multiplication, 1),
                    new MathMember(1, MathOperation.Addition, 2)
                }
            };
            yield return new object[]
            {
                "(((4 - 2) / 2) + (8 / 4 * (5 * 3)))",
                new List<MathMember>
                {
                    new MathMember(4, MathOperation.None, 0),
                    new MathMember(2, MathOperation.Subtraction, 3),
                    new MathMember(2, MathOperation.Division, 2),
                    new MathMember(8, MathOperation.Addition, 1),
                    new MathMember(4, MathOperation.Division, 2),
                    new MathMember(5, MathOperation.Multiplication, 2),
                    new MathMember(3, MathOperation.Multiplication, 3)
                }
            };
        }

        public static IEnumerable<object[]> ExpressionUndefinedTestsData()
        {
            yield return new object[]
            {
                "1 000.1 + 1 000.1",
                "1 000.1",
                new List<MathMember>
                {
                    new MathMember(1000.1m, MathOperation.None, 0),
                    new MathMember(1000.1m, MathOperation.Addition, 0)
                }
            };
            yield return new object[]
            {
                "1_000.1 + 1_000.1",
                "1_000.1",
                new List<MathMember>
                {
                    new MathMember(1000.1m, MathOperation.None, 0),
                    new MathMember(1000.1m, MathOperation.Addition, 0)
                }
            };
            yield return new object[]
            {
                "1 000,1 + 1 000,1",
                "1 000,1",
                new List<MathMember>
                {
                    new MathMember(1000.1m, MathOperation.None, 0),
                    new MathMember(1000.1m, MathOperation.Addition, 0)
                }
            };
            yield return new object[]
            {
                "1,000.1 + 1,000.1",
                "1,000.1",
                new List<MathMember>
                {
                    new MathMember(1000.1m, MathOperation.None, 0),
                    new MathMember(1000.1m, MathOperation.Addition, 0)
                }
            };
            yield return new object[]
            {
                "1000;1 + 1000;1",
                "1000;1",
                new List<MathMember>
                {
                    new MathMember(1000.1m, MathOperation.None, 0),
                    new MathMember(1000.1m, MathOperation.Addition, 0)
                }
            };
        }
    }
}
