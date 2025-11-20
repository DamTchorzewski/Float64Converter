namespace core.gates
{
	// Bramka NAND - negacja bramki AND, zwraca false tylko gdy oba wejścia są true
	public class NandGate
	{
		// Wykonuje operację logiczną NAND (NOT AND)
		public bool Compute(bool a, bool b)
		{
			return !(a && b);
		}
	}
}