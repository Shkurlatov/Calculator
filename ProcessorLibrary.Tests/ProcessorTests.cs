using System;
using System.Collections.Generic;
using Xunit;
using MathUnitsLibrary;

namespace ProcessorLibrary.Tests
{
    public class ProcessorTests
    {
        [Theory]
        [MemberData(nameof(GetResultTestsData))]
        public void GetResult_TakesVariousExpressions_ReturnsResult(double expected, List<MathMember> expression)
        {
            // arrange
            Processor processor = new Processor();

            // act
            double result = processor.GetResult(expression);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(DivideByZeroTestsData))]
        public void GetResult_TakesVariousExpressions_DivideByZeroHappens(List<MathMember> expression)
        {
            // arrange
            Processor processor = new Processor();

            // act
            void act() => processor.GetResult(expression);

            // assert
            Exception exception = Assert.Throws<DivideByZeroException>(act);
        }

        public static IEnumerable<object[]> GetResultTestsData()
        {
            yield return new object[]
            {
                1.1214076246334317d,
                new List<MathMember>
                {
                    new MathMember(1.2d, MathOperation.None),
                    new MathMember(4.8d, MathOperation.Subtraction),
                    new MathMember(8.05d, MathOperation.Addition),
                    new MathMember(5.115d, MathOperation.Division),
                    new MathMember(3, MathOperation.Multiplication)
                }
            };
            yield return new object[]
            {
                6d,
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
                33d,
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
                1.1214076246334317d,
                new List<MathMember>
                {
                    new MathMember(1.2d, MathOperation.None, 0),
                    new MathMember(4.8d, MathOperation.Subtraction, 0),
                    new MathMember(8.05d, MathOperation.Addition, 0),
                    new MathMember(5.115d, MathOperation.Division, 0),
                    new MathMember(3, MathOperation.Multiplication, 0)
                }
            };
            yield return new object[]
            {
                19d,
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
                31d,
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
                0d,
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

        public static IEnumerable<object[]> DivideByZeroTestsData()
        {
            yield return new object[]
            {
                new List<MathMember>
                {
                    new MathMember(2, MathOperation.None),
                    new MathMember(0, MathOperation.Division)
                }
            };
            yield return new object[]
            {
                new List<MathMember>
                {
                    new MathMember(2, MathOperation.None, 0),
                    new MathMember(2, MathOperation.Division, 1),
                    new MathMember(2, MathOperation.Subtraction, 2)
                }
            };
            yield return new object[]
            {
                new List<MathMember>
                {
                    new MathMember(1, MathOperation.None, 0),
                    new MathMember(16, MathOperation.Division, 0),
                    new MathMember(2, MathOperation.Subtraction, 1),
                    new MathMember(2, MathOperation.Multiplication, 2),
                    new MathMember(3, MathOperation.Exponentiation, 2)
                }
            };
        }
    }
}
