namespace core.cpu
{
    // Magistrala 8-bitowa do przesyłania danych między komponentami
    public class Bus8bit
    {
        private byte _data;

        // Zapisuje dane na magistralę
        public void Write(byte value)
        {
            _data = value;
        }

        // Odczytuje dane z magistrali
        public byte Read() => _data;
    }
}
