namespace core.gates
{
    // Bramka NOT - neguje wartość wejściową
    public class NotGate
    {
        // Wykonuje operację negacji logicznej
        public bool Compute(bool a)
        {
            return !a;
        }
    }
}