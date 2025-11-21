namespace core.cpu.registers
{
    // Rejestr 8-bitowy z sygnałem zapisu
    public class Register8bit
    {
        private byte _value;

        // Zapisuje wartość do rejestru gdy writeEnable jest aktywny
        public void Write(byte value, bool writeEnable)
        {
            if (writeEnable)
            {
                _value = value;
            }
        }

        // Odczytuje wartość zapisaną w rejestrze
        public byte Read() => _value;
    }
}
