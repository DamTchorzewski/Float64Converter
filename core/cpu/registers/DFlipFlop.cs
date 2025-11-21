using core.gates;

namespace core.cpu.registers
{
    // Przerzutnik D (Data Flip-Flop) - zapamiętuje wartość wejścia D podczas zbocza zegara
    public class DFlipFlop
    {
        private bool _q = false; // Aktualny stan przerzutnika

        // Aktualizuje stan przerzutnika przy aktywnym sygnale zegara
        public void Clock(bool d, bool clock)
        {
            if (clock) // Reagujemy tylko na aktywne zbocze zegara
            {
                _q = d;
            }
        }

        // Zwraca aktualny stan wyjścia przerzutnika
        public bool Output => _q;
    }
}
