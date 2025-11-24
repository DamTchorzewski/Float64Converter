namespace core.cpu
{
    // Multiplekser 8-bitowy - wybiera jedno z dwóch wejść na podstawie selektora
    public class Mux8bit
    {
        // Wybiera i zwraca jedno z dwóch wejść
        public byte Select(byte input0, byte input1, bool selector)
        {
            return selector ? input1 : input0;
        }
    }
}
