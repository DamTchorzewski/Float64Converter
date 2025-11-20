using core.gates;

namespace core.components
{
    // Półsumator - podstawowy układ dodający dwa bity bez przeniesienia wejściowego
    public class HalfAdder
    {
        private readonly XorGate _xor = new();
        private readonly AndGate _and = new();

        // Oblicza sumę i przeniesienie dla dwóch bitów
        public (bool sum, bool carry) Compute(bool a, bool b)
        {
            bool sum = _xor.Compute(a, b);      // Suma bez przeniesienia
            bool carry = _and.Compute(a, b);    // Przeniesienie

            return (sum, carry);
        }
    }
}