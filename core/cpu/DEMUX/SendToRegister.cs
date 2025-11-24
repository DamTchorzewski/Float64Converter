using core.cpu.registers;

namespace core.cpu
{
    // Uproszczony demultiplekser do bezpośredniego zapisu danych do rejestru
    public class SimpleDemux
    {
        // Zapisuje dane bezpośrednio do wskazanego rejestru
        public void WriteToRegister(byte data, Register8bit dest, bool clock)
        {
            dest.Write(data, clock);
        }
    }
}
