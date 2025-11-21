namespace core.cpu
{
    // Typ operacji obsługiwanych przez ALU 64-bitowe
    public enum ALU64Operation { ADD, SUB, XOR, AND, OR }

    // Jednostka arytmetyczno-logiczna 64-bitowa do operacji na liczbach bez znaku
    public class ALU64bit
    {
        // Wynik ostatniej operacji ALU
        public ulong Result { get; private set; }

        // Wykonuje operację arytmetyczną lub logiczną na dwóch 64-bitowych liczbach
        public void Execute(ulong a, ulong b, ALU64Operation op)
        {
            Result = op switch
            {
                ALU64Operation.ADD => a + b,
                ALU64Operation.SUB => a - b,
                ALU64Operation.XOR => a ^ b,
                ALU64Operation.AND => a & b,
                ALU64Operation.OR => a | b,
                _ => 0
            };
        }
    }
}
