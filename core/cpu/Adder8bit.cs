using core.components;

namespace core.cpu
{
    // Sumator 8-bitowy zbudowany z 8 pełnych sumatorów połączonych szeregowo
    public class Adder8bit
    {
        private readonly FullAdder[] _adders = new FullAdder[8];

        // Konstruktor inicjalizujący tablicę 8 pełnych sumatorów
        public Adder8bit()
        {
            for (int i = 0; i < 8; i++)
                _adders[i] = new FullAdder();
        }

        // Dodaje dwie liczby 8-bitowe bit po bicie z propagacją przeniesienia
        public (byte result, bool carryOut) Add(byte a, byte b)
        {
            bool carry = false;
            byte result = 0;

            // Iteracja przez wszystkie bity od najmniej znaczącego
            for (int i = 0; i < 8; i++)
            {
                bool bitA = ((a >> i) & 1) == 1;  // Wyodrębnienie i-tego bitu z a
                bool bitB = ((b >> i) & 1) == 1;  // Wyodrębnienie i-tego bitu z b

                var (sum, cOut) = _adders[i].Compute(bitA, bitB, carry);

                if (sum) result |= (byte)(1 << i); // Ustawienie bitu wyniku
                carry = cOut;                       // Propagacja przeniesienia
            }

            return (result, carry);
        }
    }
}