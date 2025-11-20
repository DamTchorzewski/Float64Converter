using core;

// Główna klasa aplikacji - punkt wejścia programu
class Program
{
    // Metoda startowa aplikacji konwertującej liczby na format IEEE 754
    static void Main(string[] args)
    {
        Console.WriteLine("Symulator konwersji liczby dziesiętnej na IEEE 754 (double precision)");
        Console.Write("Podaj liczbę dziesiętną do konwersji: ");

        string? input = Console.ReadLine();
        // Walidacja i parsowanie danych wejściowych
        if (double.TryParse(input, out double number))
        {
            ConversionEngine.DisplayBinaryDetails(number);
        }
        else
        {
            Console.WriteLine("Nieprawidłowa liczba.");
        }

        Console.WriteLine("\nNaciśnij dowolny klawisz aby zakończyć...");
        Console.ReadKey();
    }
}
