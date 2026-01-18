using core.utils;
using Xunit;

namespace Float64Converter.Tests
{
    public class NumberParserTests
    {
        [Theory]
        [InlineData("42", 42.0)]
        [InlineData("-13", -13.0)]
        [InlineData("3.5", 3.5)]
        [InlineData("3,5", 3.5)]
        [InlineData("1e2", 100.0)]
        [InlineData("-2.5E1", -25.0)]
        public void TryParse_ParsesValidNumbers(string input, double expected)
        {
            Assert.True(NumberParser.TryParse(input, out double result, out NumberParser.EdgeCase edgeCase));
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("1e")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("--1")]
        public void TryParse_ReturnsFalseForInvalidInput(string input)
        {
            Assert.False(NumberParser.TryParse(input, out double result, out NumberParser.EdgeCase edgeCase));
        }

        [Theory]
        [InlineData("Infinity", double.PositiveInfinity)]
        [InlineData("-Infinity", double.NegativeInfinity)]
        [InlineData("NaN", double.NaN)]
        public void TryParse_HandlesSpecialValues(string input, double expected)
        {
            Assert.True(NumberParser.TryParse(input, out double result, out NumberParser.EdgeCase edgeCase));

            if (double.IsNaN(expected))
            {
                Assert.True(double.IsNaN(result));
            }
            else
            {
                Assert.Equal(expected, result);
            }
        }

        [Theory]
        [InlineData("-0", NumberParser.EdgeCase.NegativeZero)]
        [InlineData("-0.0", NumberParser.EdgeCase.NegativeZero)]
        [InlineData("-0,00000", NumberParser.EdgeCase.NegativeZero)]
        [InlineData("-0.0000000000000000", NumberParser.EdgeCase.NegativeZero)]
        [InlineData("-0.000", NumberParser.EdgeCase.NegativeZero)]
        [InlineData("-0,00000000", NumberParser.EdgeCase.NegativeZero)]
        public void TryParse_HandlesNegativeZero(string input, NumberParser.EdgeCase expectedEdgeCase)
        {
            Assert.True(NumberParser.TryParse(input, out double result, out NumberParser.EdgeCase edgeCase));
            Assert.Equal(expectedEdgeCase, edgeCase);
        }
    }
}
