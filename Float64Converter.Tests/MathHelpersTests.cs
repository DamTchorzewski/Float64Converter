using core.utils;
using Xunit;

namespace Float64Converter.Tests
{
    public class MathHelpersTests
    {
        [Theory]
        [InlineData(-5.0, 5.0)]
        [InlineData(0.0, 0.0)]
        [InlineData(7.25, 7.25)]
        public void Abs_ReturnsAbsoluteValue(double input, double expected)
        {
            Assert.Equal(expected, MathHelpers.Abs(input));
        }

        [Theory]
        [InlineData(0, 1.0)]
        [InlineData(1, 2.0)]
        [InlineData(3, 8.0)]
        [InlineData(-1, 0.5)]
        [InlineData(-3, 0.125)]
        public void Pow2_ComputesExpectedPower(int exponent, double expected)
        {
            Assert.Equal(expected, MathHelpers.Pow2(exponent));
        }

        [Theory]
        [InlineData(double.NaN, true)]
        [InlineData(1.0, false)]
        public void IsNaN_DetectsNan(double value, bool expected)
        {
            Assert.Equal(expected, MathHelpers.IsNaN(value));
        }

        [Theory]
        [InlineData(double.PositiveInfinity, true)]
        [InlineData(double.NegativeInfinity, true)]
        [InlineData(1.7976931348623157E+308, false)]
        [InlineData(-1.7976931348623157E+308, false)]
        public void IsInfinity_DetectsInfinity(double value, bool expected)
        {
            Assert.Equal(expected, MathHelpers.IsInfinity(value));
        }

        [Theory]
        [InlineData(double.PositiveInfinity, true)]
        [InlineData(5.0, false)]
        public void IsPositiveInfinity_RecognizesPositiveInfinity(double value, bool expected)
        {
            Assert.Equal(expected, MathHelpers.IsPositiveInfinity(value));
        }

        [Theory]
        [InlineData(double.NegativeInfinity, true)]
        [InlineData(-5.0, false)]
        public void IsNegativeInfinity_RecognizesNegativeInfinity(double value, bool expected)
        {
            Assert.Equal(expected, MathHelpers.IsNegativeInfinity(value));
        }

        [Theory]
        [InlineData(5.0, 3.0, 5.0)]
        [InlineData(-2.0, -5.0, -2.0)]
        public void Max_ReturnsLargerValue(double a, double b, double expected)
        {
            Assert.Equal(expected, MathHelpers.Max(a, b));
        }

        [Theory]
        [InlineData(0UL, 4, "0000")]
        [InlineData(5UL, 4, "0101")]
        [InlineData(5UL, 8, "00000101")]
        [InlineData(255UL, 4, "11111111")]
        public void ToBinaryString_PadsAndFormatsCorrectly(ulong value, int minLength, string expected)
        {
            Assert.Equal(expected, MathHelpers.ToBinaryString(value, minLength));
        }
    }
}
