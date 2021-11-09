using System.Collections.Generic;
using Xunit;
using MathUnitsLibrary;

namespace ProcessorLibrary.Tests
{
    public class ProcessorTests
    {
        [Theory]
        [MemberData(nameof(GetResultTestsData))]
        public void GetResult_TakesVariousExpressions_ReturnsResult(string expected, List<MathMember> expression)
        {
            // arrange
            Processor processor = new Processor();

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
                    new MathMember(1.2m, MathOperation.None),
                    new MathMember(4.8m, MathOperation.Subtraction),
                    new MathMember(8.05m, MathOperation.Addition),
                    new MathMember(5.115m, MathOperation.Division),
                    new MathMember(3, MathOperation.Multiplication)
                }
            };
            yield return new object[]
            {
                6m.ToString(),
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
                33m.ToString(),
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
            yield return new object[]
            {
                "The result is not achievable, division by zero occured",
                new List<MathMember>
                {
                    new MathMember(2, MathOperation.None),
                    new MathMember(0, MathOperation.Division)
                }
            };
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
                    new MathMember(2, MathOperation.Division, 1),
                    new MathMember(2, MathOperation.Subtraction, 2)
                }
            };
        }
    }
}
