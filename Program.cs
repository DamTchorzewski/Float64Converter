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
            // Informacja o rzeczywistej wartości (po zaokrągleniu przez typ double)
            Console.WriteLine($"Wprowadzona wartość: {input}");
            Console.WriteLine($"Rzeczywista wartość double: {number}");
            
            // Ostrzeżenie o zaokrągleniu, jeśli wystąpiło
            if (input.Trim() != number.ToString())
            {
                Console.WriteLine("⚠ UWAGA: Wartość została zaokrąglona do najbliższej reprezentowalnej liczby double.");
            }
            Console.WriteLine();
            
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
