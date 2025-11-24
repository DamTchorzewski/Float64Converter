using core.gates;

namespace core.cpu.registers
{
    // Przerzutnik SR (Set-Reset) - podstawowy element pamięci 1 bitu
    public class SRLatch
    {
        private readonly NandGate _nand = new();

        private bool _q = true;      // Wyjście Q
        private bool _notQ = false;  // Wyjście zanegowane ~Q

        // Oblicza nowy stan przerzutnika na podstawie sygnałów Set i Reset
        public (bool Q, bool notQ) Compute(bool S, bool R)
        {
            _q = _nand.Compute(S, _notQ);
            _notQ = _nand.Compute(R, _q);

            return (_q, _notQ);
        }

        public bool Output => _q;
    }
}