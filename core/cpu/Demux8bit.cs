using core.cpu.registers;

namespace core.cpu
{
    // Demultiplekser 8-bitowy - kieruje dane do jednego z wielu rejestrów
    public class Demux8bit
    {
        // Wysyła dane do wybranego rejestru na podstawie selektora
        public void Send(byte data, Register8bit reg0, Register8bit reg1, bool selector, bool clock)
        {
            if (selector)
            {
                reg1.Write(data, clock);
            }
            else
            {
                reg0.Write(data, clock);
            }
        }
    }
}
