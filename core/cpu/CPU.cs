using core.cpu.registers;

namespace core.cpu
{
    // Procesor 8-bitowy z czterema rejestrami ogólnego przeznaczenia i ALU
    public class CPU8bit
    {
        public Register8bit RegA { get; } = new();  // Rejestr A
        public Register8bit RegB { get; } = new();  // Rejestr B
        public Register8bit RegC { get; } = new();  // Rejestr C
        public Register8bit RegD { get; } = new();  // Rejestr D

        public ALU8bit ALU { get; } = new();        // Jednostka arytmetyczno-logiczna
        public Clock Clock { get; } = new();         // Generator zegara

        // Wykonuje operację ALU na dwóch rejestrach źródłowych i zapisuje wynik do rejestru docelowego
        public void ExecuteOperation(Register8bit src1, Register8bit src2, Register8bit dest, ALUOperation op)
        {
            byte a = src1.Read();
            byte b = src2.Read();

            ALU.Execute(a, b, op);

            // Bezpośredni zapis wyniku do rejestru docelowego
            dest.Write(ALU.Result, true);

            Clock.Tick();
        }
    }
}
