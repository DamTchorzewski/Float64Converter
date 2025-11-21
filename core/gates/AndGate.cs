namespace core.gates
{
    // Bramka AND - zwraca true tylko gdy oba wejścia są true
    public class AndGate
    {
        // Wykonuje operację logiczną AND na dwóch bitach
        public bool Compute(bool a, bool b)
        {
            return a && b;
        }
    }
}