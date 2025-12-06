using core;
using System;
using System.IO;
using Xunit;

namespace Float64Converter.Tests
{
    public class ConversionEngineTests
    {
        [Theory]
        [InlineData(0.0)]
        [InlineData(-123.75)]
        [InlineData(Math.PI)]
        public void ConvertToIEEE754Binary_MatchesBitPattern(double value)
        {
            string binary = ConversionEngine.ConvertToIEEE754Binary(value);

            ulong bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            string expected = Convert.ToString((long)bits, 2).PadLeft(64, '0');

            Assert.Equal(expected, binary);
        }

        [Fact]
        public void DisplayBinaryDetails_WritesExpectedContent()
        {
            using var writer = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(writer);

            try
            {
                ConversionEngine.DisplayBinaryDetails(6.75);
            }
            finally
            {
                Console.SetOut(originalOut);
            }

            string output = writer.ToString();
            Assert.Contains("Liczba dziesiętna: 6.75", output);
            Assert.Contains("Sign:", output);
            Assert.Contains("Exponent:", output);
            Assert.Contains("Mantissa:", output);
            Assert.Contains("Status:", output);
        }
    }
}
