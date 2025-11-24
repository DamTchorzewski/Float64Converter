namespace core.gates
{
    // Bramka XOR - zwraca true gdy wejścia mają różne wartości
    public class XorGate
    {
        // Wykonuje operację alternatywy wykluczającej (XOR)
        public bool Compute(bool a, bool b)
        {
            return a ^ b;
        }
    }
}