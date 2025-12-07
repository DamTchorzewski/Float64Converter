using core.utils;
using System;
using Xunit;

namespace Float64Converter.Tests
{
    public class IEEE754ValidatorTests
    {
        [Fact]
        public void ReconstructFromIEEE754_RecreatesOriginalValue()
        {
            double value = -13.25;
            ulong bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            ulong sign = bits >> 63;
            ulong exponent = (bits >> 52) & 0x7FFUL;
            ulong mantissa = bits & 0xFFFFFFFFFFFFFUL;

            double reconstructed = IEEE754Validator.ReconstructFromIEEE754(sign, exponent, mantissa);

            Assert.Equal(value, reconstructed);
        }

        [Fact]
        public void ReconstructFromIEEE754_HandlesSubnormal()
        {
            double value = double.Epsilon;
            ulong bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            ulong sign = bits >> 63;
            ulong exponent = (bits >> 52) & 0x7FFUL;
            ulong mantissa = bits & 0xFFFFFFFFFFFFFUL;

            double reconstructed = IEEE754Validator.ReconstructFromIEEE754(sign, exponent, mantissa);

            Assert.Equal(value, reconstructed);
        }

        [Fact]
        public void ReconstructFromIEEE754_ReturnsInfinityAndNaN()
        {
            double posInf = IEEE754Validator.ReconstructFromIEEE754(0, 0x7FF, 0);
            double negInf = IEEE754Validator.ReconstructFromIEEE754(1, 0x7FF, 0);
            double nan = IEEE754Validator.ReconstructFromIEEE754(0, 0x7FF, 1);

            Assert.Equal(double.PositiveInfinity, posInf);
            Assert.Equal(double.NegativeInfinity, negInf);
            Assert.True(double.IsNaN(nan));
        }

        [Fact]
        public void AreEqual_UsesToleranceAndHandlesSpecialValues()
        {
            Assert.True(IEEE754Validator.AreEqual(1.00000000001, 1.00000000002));
            Assert.False(IEEE754Validator.AreEqual(1.0, 1.1));
            Assert.True(IEEE754Validator.AreEqual(double.NaN, double.NaN));
            Assert.True(IEEE754Validator.AreEqual(double.PositiveInfinity, double.PositiveInfinity));
            Assert.False(IEEE754Validator.AreEqual(double.PositiveInfinity, double.NegativeInfinity));
        }
    }
}
