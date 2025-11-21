using core.cpu.registers;

namespace core.cpu
{
    // Procesor 64-bitowy dedykowany do operacji na komponentach liczb zmiennoprzecinkowych IEEE 754
    public class CPU64bit
    {
        public Register64bit RegSign { get; } = new();      // Rejestr bitu znaku
        public Register64bit RegExponent { get; } = new();  // Rejestr wykładnika
        public Register64bit RegMantissa { get; } = new();  // Rejestr mantysy
        public ALU64bit ALU { get; } = new();               // Jednostka arytmetyczno-logiczna 64-bit

        // Wykonuje operację ALU na dwóch rejestrach źródłowych i zapisuje wynik do rejestru docelowego
        public void ExecuteOperation(Register64bit src1, Register64bit src2, Register64bit dest, ALU64Operation op)
        {
            ALU.Execute(src1.Read(), src2.Read(), op);
            dest.Write(ALU.Result, true);
        }
    }
}
