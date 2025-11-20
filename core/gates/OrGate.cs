namespace core.gates
{
    // Bramka OR - zwraca true gdy co najmniej jedno wejście jest true
    public class OrGate
    {
        // Wykonuje operację logiczną OR na dwóch bitach
        public bool Compute(bool a, bool b)
        {
            return a || b;
        }
    }
}