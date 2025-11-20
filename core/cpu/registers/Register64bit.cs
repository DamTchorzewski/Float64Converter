namespace core.cpu.registers
{
    // Rejestr 64-bitowy do przechowywania danych bez znaku
    public class Register64bit
    {
        private ulong _value;

        // Zapisuje wartość do rejestru gdy writeEnable jest aktywny
        public void Write(ulong value, bool writeEnable)
        {
            if (writeEnable)
                _value = value;
        }

        // Odczytuje wartość zapisaną w rejestrze
        public ulong Read() => _value;
    }
}
