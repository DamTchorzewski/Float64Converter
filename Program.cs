using core;
using core.utils;

// Główna klasa aplikacji - punkt wejścia programu
// Symulator konwersji liczb dziesiętnych na format IEEE 754 (double precision)
class Program
{
    // Metoda startowa aplikacji konwertującej liczby na format IEEE 754
    // Używa ręcznego parsera liczb i ręcznej konwersji do IEEE 754
    static void Main(string[] args)
    {
        Console.WriteLine("Symulator konwersji liczby dziesiętnej na IEEE 754 (double precision)");
        Console.Write("Podaj liczbę dziesiętną do konwersji: ");

        string? input = Console.ReadLine();
        
        // Walidacja i parsowanie danych wejściowych
        // Użycie ręcznego parsera (bez double.TryParse) - pełna implementacja manualna
        if (!string.IsNullOrWhiteSpace(input) && NumberParser.TryParse(input, out double number))
        {
            // Wyświetlenie szczegółów konwersji IEEE 754
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
