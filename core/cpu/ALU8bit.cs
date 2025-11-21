namespace core.cpu
{
    // Typ operacji obsługiwanych przez ALU 8-bitowe
    public enum ALUOperation
    {
        ADD,  // Dodawanie
        SUB,  // Odejmowanie
        AND,  // Koniunkcja logiczna
        OR,   // Alternatywa logiczna
        XOR,  // Alternatywa wykluczająca
        NOT   // Negacja
    }

    // Jednostka arytmetyczno-logiczna 8-bitowa
    public class ALU8bit
    {
        // Wynik ostatniej operacji ALU
        public byte Result { get; private set; }

        // Wykonuje operację arytmetyczną lub logiczną na dwóch 8-bitowych liczbach
        public void Execute(byte inputA, byte inputB, ALUOperation operation)
        {
            Result = operation switch
            {
                ALUOperation.ADD => (byte)(inputA + inputB),
                ALUOperation.SUB => (byte)(inputA - inputB),
                ALUOperation.AND => (byte)(inputA & inputB),
                ALUOperation.OR => (byte)(inputA | inputB),
                ALUOperation.XOR => (byte)(inputA ^ inputB),
                ALUOperation.NOT => (byte)(~inputA),
                _ => 0
            };
        }
    }
}
