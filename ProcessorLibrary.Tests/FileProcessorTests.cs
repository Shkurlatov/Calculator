using System.Collections.Generic;
using Xunit;
using MathUnitsLibrary;

namespace ProcessorLibrary.Tests
{
    public class FileProcessorTests
    {
        [Theory]
        [MemberData(nameof(GetResultTestsData))]
        public void GetResult_TakesVariousExpressions_ReturnsResult(string expected, List<MathMember> expression)
        {
            // arrange
            Processor processor = new FileProcessor();

            // act
            string result = processor.GetResult(expression);

            // assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> GetResultTestsData()
        {
            yield return new object[]
            {
                1.1214076246334310850439882697m.ToString(),
                new List<MathMember>
                {
                    new MathMember(1.2m, MathOperation.None, 0),
                    new MathMember(4.8m, MathOperation.Subtraction, 0),
                    new MathMember(8.05m, MathOperation.Addition, 0),
                    new MathMember(5.115m, MathOperation.Division, 0),
                    new MathMember(3, MathOperation.Multiplication, 0)
                }
            };
            yield return new object[]
            {
                19m.ToString(),
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
                31m.ToString(),
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
                "The result is not achievable, division by zero occured",
                new List<MathMember>
                {
                    new MathMember(2, MathOperation.None, 0),
                    new MathMember(2, MathOperation.Division, 0),
                    new MathMember(2, MathOperation.Subtraction, 1)
                }
            };
        }
    }
}
