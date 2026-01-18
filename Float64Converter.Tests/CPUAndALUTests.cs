using core.cpu;
using core.cpu.registers;
using Xunit;

namespace Float64Converter.Tests
{
    public class CPUAndALUTests
    {
        [Theory]
        [InlineData(5UL, 3UL, ALU64Operation.ADD, 8UL)]
        [InlineData(5UL, 3UL, ALU64Operation.SUB, 2UL)]
        [InlineData(5UL, 3UL, ALU64Operation.XOR, 6UL)]
        [InlineData(5UL, 3UL, ALU64Operation.AND, 1UL)]
        [InlineData(5UL, 3UL, ALU64Operation.OR, 7UL)]
        public void ALU_ExecutesOperations(ulong a, ulong b, ALU64Operation operation, ulong expected)
        {
            var alu = new ALU64bit();

            alu.Execute(a, b, operation);

            Assert.Equal(expected, alu.Result);
        }

        [Fact]
        public void CPU_ExecuteOperation_WritesResultToDestinationRegister()
        {
            var cpu = new CPU64bit();
            var source1 = new Register64bit();
            var source2 = new Register64bit();
            var destination = new Register64bit();

            source1.Write(10, true);
            source2.Write(20, true);

            cpu.ExecuteOperation(source1, source2, destination, ALU64Operation.ADD);

            Assert.Equal(30UL, destination.Read());
            Assert.Equal(30UL, cpu.ALU.Result);
        }
    }
}
