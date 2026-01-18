using core.components;
using core.gates;
using Xunit;

namespace Float64Converter.Tests
{
    public class GatesTests
    {
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, false)]
        [InlineData(true, false, false)]
        [InlineData(true, true, true)]
        public void AndGate_ComputesExpectedResult(bool a, bool b, bool expected)
        {
            var gate = new AndGate();

            Assert.Equal(expected, gate.Compute(a, b));
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, true)]
        public void OrGate_ComputesExpectedResult(bool a, bool b, bool expected)
        {
            var gate = new OrGate();

            Assert.Equal(expected, gate.Compute(a, b));
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public void XorGate_ComputesExpectedResult(bool a, bool b, bool expected)
        {
            var gate = new XorGate();

            Assert.Equal(expected, gate.Compute(a, b));
        }

        [Theory]
        [InlineData(false, false, false, false, false)]
        [InlineData(false, false, true, true, false)]
        [InlineData(false, true, false, true, false)]
        [InlineData(false, true, true, false, true)]
        [InlineData(true, false, false, true, false)]
        [InlineData(true, false, true, false, true)]
        [InlineData(true, true, false, false, true)]
        [InlineData(true, true, true, true, true)]
        public void FullAdder_ComputesSumAndCarry(bool a, bool b, bool cin, bool expectedSum, bool expectedCarry)
        {
            var adder = new FullAdder();

            var (sum, carry) = adder.Compute(a, b, cin);

            Assert.Equal(expectedSum, sum);
            Assert.Equal(expectedCarry, carry);
        }
    }
}
