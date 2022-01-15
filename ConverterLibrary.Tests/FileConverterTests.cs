using System.Collections.Generic;
using Xunit;
using MathUnitsLibrary;
using System.Globalization;

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
        [MemberData(nameof(ExpressionHasNegativeMembersTestsData))]
        public void IsExpressionComplete_TakesStringsWithNegativeDoubleOperations_MakesMembersNegative(string sourceLine, List<MathMember> expression, bool[] isNegative)
        {
            // arrange
            InputConverter converter = new FileConverter();
            for (int i = 0; i < expression.Count; i++)
            {
                if (isNegative[i])
                {
                    expression[i].MakeNegative();
                }
            }

            // act
            bool isExpressionComplete = converter.IsExpressionComplete(sourceLine);

            // assert
            Assert.True(isExpressionComplete);
            Assert.Equal(expression, converter.MathExpression);
            Assert.Null(converter.ServiceMessage);
        }

        [Theory]
        [MemberData(nameof(WrongNumberFormatTestsData))]
        public void IsExpressionComplete_ConvertVariousNumberFormats_ReturnsFalse(string sourceLine, string cultureInfo)
        {
            // arrange
            InputConverter converter = new FileConverter();
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(cultureInfo);
            string message = "Invalid expression format, the processing ended unsuccessefully";

            // act
            bool isExpressionComplete = converter.IsExpressionComplete(sourceLine);

            // assert
            Assert.False(isExpressionComplete);
            Assert.Equal(message, converter.ServiceMessage);
        }

        [Theory]
        [MemberData(nameof(RightNumberFormatTestsData))]
        public void IsExpressionComplete_ConvertVariousNumberFormats_ReturnsTrue(string sourceLine, string cultureInfo)
        {
            // arrange
            InputConverter converter = new FileConverter();
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(cultureInfo);
            List<MathMember> expression = new List<MathMember>
                {
                    new MathMember(1000.10d, MathOperation.None),
                    new MathMember(1000.10d, MathOperation.Addition),
                };

            // act
            bool isExpressionComplete = converter.IsExpressionComplete(sourceLine);

            // assert
            Assert.True(isExpressionComplete);
            Assert.Equal(expression, converter.MathExpression);
        }

        public static IEnumerable<object[]> ExpressionFailedTestsData()
        {
            yield return new object[] { "", "" };
            yield return new object[] { "   ", "" };
            yield return new object[] { "* 1 + 2", "Wrong math operator at the begining of the string" };
            yield return new object[] { "1 + 2 +", "The math operator at the end of the string" };
            yield return new object[] { "+ + 1 + 2", "Two math operators at the begining of the string" };
            yield return new object[] { "1 + * 2", "Two not acceptable math operators in a row" };
            yield return new object[] { "1 + + + 2", "Three math operators in a row" };
            yield return new object[] { ")1 + 2)", "Closing brace at the begining of the string" };
            yield return new object[] { "(1 + 2(", "Opening brace at the end of the string" };
            yield return new object[] { "(1 + 2 +)", "The math operator before the closing brace" };
            yield return new object[] { "(* 1 + 2)", "Wrong math operator after the opening brace" };
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
            yield return new object[]
            {
                "16 - 2 * 2 ^ (1 + 2)",
                new List<MathMember>
                {
                    new MathMember(16, MathOperation.None, 0),
                    new MathMember(2, MathOperation.Subtraction, 0),
                    new MathMember(2, MathOperation.Multiplication, 0),
                    new MathMember(1, MathOperation.Exponentiation, 0),
                    new MathMember(2, MathOperation.Addition, 1)
                }
            };
        }

        public static IEnumerable<object[]> ExpressionHasNegativeMembersTestsData()
        {
            yield return new object[]
            {
                "2^-(3-4)+2",
                new List<MathMember>
                {
                    new MathMember(2, MathOperation.None, 0),
                    new MathMember(3, MathOperation.Exponentiation, 0),
                    new MathMember(4, MathOperation.Subtraction, 1),
                    new MathMember(2, MathOperation.Addition, 0)
                },
                new bool[] { false, true, false, false }
            };
            yield return new object[]
            {
                "16 - 2 * - 2 ^ - (1 + 2)",
                new List<MathMember>
                {
                    new MathMember(16, MathOperation.None, 0),
                    new MathMember(2, MathOperation.Subtraction, 0),
                    new MathMember(2, MathOperation.Multiplication, 0),
                    new MathMember(1, MathOperation.Exponentiation, 0),
                    new MathMember(2, MathOperation.Addition, 1)
                },
                new bool[] { false, false, true, true, false }
            };
        }

        public static IEnumerable<object[]> WrongNumberFormatTestsData()
        {
            yield return new object[]
            {
                "1 000,10 + 1 000,10",
                "en-US"
            };
            yield return new object[]
            {
                "1,000.10 + 1,000.10",
                "es-ES"
            };
            yield return new object[]
            {
                "1.000,10 + 1.000,10",
                "fr-FR"
            };
        }

        public static IEnumerable<object[]> RightNumberFormatTestsData()
        {
            yield return new object[]
            {
                "1,000.10 + 1,000.10",
                "en-US"
            };
            yield return new object[]
            {
                "1.000,10 + 1.000,10",
                "es-ES"
            };
            yield return new object[]
            {
                "1 000,10 + 1 000,10",
                "fr-FR"
            };
        }
    }
}
