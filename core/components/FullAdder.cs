using core.gates;

namespace core.components
{
    // Pełny sumator - dodaje trzy bity (A, B, Carry-In) zwracając sumę i przeniesienie
    public class FullAdder
    {
        private readonly XorGate _xor = new();
        private readonly AndGate _and = new();
        private readonly OrGate _or = new();

        // Oblicza sumę trzech bitów wraz z przeniesieniem wyjściowym
        public (bool sum, bool carry) Compute(bool a, bool b, bool cin)
        {
            bool s1 = _xor.Compute(a, b);       // Suma częściowa
            bool sum = _xor.Compute(s1, cin);   // Suma końcowa

            bool c1 = _and.Compute(a, b);       // Przeniesienie z a i b
            bool c2 = _and.Compute(s1, cin);    // Przeniesienie z sumy częściowej i cin
            bool carry = _or.Compute(c1, c2);   // Przeniesienie wyjściowe

            return (sum, carry);
        }
    }
}