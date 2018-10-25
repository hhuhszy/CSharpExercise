using System.Collections.Generic;
using Xunit;

namespace DemoLibrary.Test
{
    public class DemoLibraryTests
    {
        [Theory]
        [MemberData(nameof(AddShouldCalcGetData),parameters:10)]
        public void AddShouldCalc(double x, double y, double expected)
        {
            //Arrange
            //static class
            //Act
            var actual = Calculator.Add(x, y);
            //Assert
            Assert.Equal(expected, actual);

        }

        public static IEnumerable<object[]> AddShouldCalcGetData(int num)
        {
            var result = new List<object[]>();
            for (int i = 0; i < num; i++)
            {
                result.Add(new object[] { i, i + 6, 4 * i });
            }
            return result;
        }


        [Fact]
        public void ExampleLoadTextFileShouldWork()
        {
            var str = Examples.ExampleLoadTextFile("this is a valid path");

            Assert.True(str.Length > 0);
        }

        [Fact]
        public void ExampleLoadTextFileShouldThrowException()
        {
            Assert.Throws<System.IO.FileNotFoundException>(() =>
            {
                Examples.ExampleLoadTextFile("");
            });
        }
    }
}
