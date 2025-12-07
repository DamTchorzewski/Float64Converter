using core.cpu;
using core.utils;
using System;
using Xunit;

namespace Float64Converter.Tests
{
    public class Float64ConverterUtilsTests
    {
        private static (ulong sign, ulong exponent, ulong mantissa) GetBits(double value)
        {
            ulong bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            ulong sign = bits >> 63;
            ulong exponent = (bits >> 52) & 0x7FFUL;
            ulong mantissa = bits & 0xFFFFFFFFFFFFFUL;
            return (sign, exponent, mantissa);
        }

        [Theory]
        [InlineData(1.5)]
        [InlineData(0.0)]
        [InlineData(123456.789)]
        public void ConvertDecimalToIEEE64_WritesExpectedRegisters(double value)
        {
            var cpu = new CPU64bit();

            Float64ConverterUtils.ConvertDecimalToIEEE64(value, cpu);

            var (expectedSign, expectedExponent, expectedMantissa) = GetBits(value);

            Assert.Equal(expectedSign, cpu.RegSign.Read());
            Assert.Equal(expectedExponent, cpu.RegExponent.Read());
            Assert.Equal(expectedMantissa, cpu.RegMantissa.Read());
        }

        [Theory]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.Epsilon)]
        public void ConvertDecimalToIEEE64_HandlesSpecialCases(double value)
        {
            var cpu = new CPU64bit();

            Float64ConverterUtils.ConvertDecimalToIEEE64(value, cpu);

            var (expectedSign, expectedExponent, expectedMantissa) = GetBits(value);

            Assert.Equal(expectedSign, cpu.RegSign.Read());
            Assert.Equal(expectedExponent, cpu.RegExponent.Read());
            Assert.Equal(expectedMantissa, cpu.RegMantissa.Read());
        }
        
        [Theory]
        [InlineData(double.NaN)]
        public void ConvertDecimalToIEEE64_HandlesNaN(double value)
        {
            var cpu = new CPU64bit();

            Float64ConverterUtils.ConvertDecimalToIEEE64(value, cpu);

            var (expectedSign, expectedExponent, expectedMantissa) = GetBits(value);

            Assert.Equal(0UL, cpu.RegSign.Read());
            Assert.Equal(expectedExponent, cpu.RegExponent.Read());
            Assert.Equal(expectedMantissa, cpu.RegMantissa.Read());
        }
    }
}
