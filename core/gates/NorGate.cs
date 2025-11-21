namespace core.gates
{
    // Bramka NOR - negacja bramki OR, zwraca true tylko gdy oba wejścia są false
    public class NorGate
    {
        // Wykonuje operację logiczną NOR (NOT OR)
        public bool Compute(bool a, bool b)
        {
            return !(a || b);
        }
    }
}