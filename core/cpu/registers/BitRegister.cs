namespace core.cpu.registers
{
    // Rejestr 1-bitowy oparty na przerzutniku D
    public class BitRegister
    {
        private readonly DFlipFlop _flipFlop = new();

        // Zapisuje bit do rejestru przy aktywnym sygnale zegara
        public void Write(bool bit, bool clock)
        {
            _flipFlop.Clock(bit, clock);
        }

        // Odczytuje wartość zapisaną w rejestrze
        public bool Read() => _flipFlop.Output;
    }
}
