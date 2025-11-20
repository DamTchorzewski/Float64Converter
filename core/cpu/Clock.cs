namespace core.cpu
{
	// Generator sygnału zegarowego dla synchronizacji komponentów CPU
	public class Clock
	{
		private bool _state = false;

		// Zmienia stan zegara na przeciwny (symuluje takt zegara)
		public void Tick()
		{
			_state = !_state; // Zmiana stanu zegara
		}

		// Zwraca aktualny stan sygnału zegarowego
		public bool State => _state;
	}
}
