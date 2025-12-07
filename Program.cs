using core.utils;
using core;
using System;
using System.IO;

namespace Float64Converter
{
    internal class Program
    {
        // =====================================
        // GŁÓWNE MENU PROGRAMU
        // =====================================
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Symulator konwersji liczby dziesiętnej na IEEE 754 (double precision) ===");
                Console.WriteLine("1 - Konwersja (wynik binarny + szesnastkowy)");
                Console.WriteLine("2 - Konwersja (tryb KROK PO KROKU + szczegóły)");
                Console.WriteLine("3 - Wyświetl zawartość pliku output.txt");
                Console.WriteLine("4 - Wyczyść plik output.txt");
                Console.WriteLine("5 - Wyjście");
                Console.Write("Wybierz opcję (1-5): ");

                var key = Console.ReadKey(intercept: true);
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        RunStandardMode();
                        break;
                    case '2':
                        RunStepByStepMode();
                        break;
                    case '3':
                        DisplayOutputFile();
                        break;
                    case '4':
                        ClearOutputFile();
                        break;
                    case '5':
                        return; // koniec programu
                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // =====================================
        // TRYB 1: wynik binarny + szesnastkowy
        // =====================================
        private static void RunStandardMode()
        {
            Console.Clear();
            Console.WriteLine("== Tryb 1: wynik binarny + szesnastkowy ==\n");

            if (!ReadAndParseNumber(out double number, out string input, out NumberParser.EdgeCase edgeCase))
                return; // wracamy do menu, jeśli błąd

            // Użycie RĘCZNEJ konwersji z ConversionEngine (bez BitConverter!)
            string binary = ConversionEngine.ConvertToIEEE754Binary(number, edgeCase);

            // Podział na komponenty (znak, wykładnik, mantysa)
            string sign = binary.Substring(0, 1);
            string exponent = binary.Substring(1, 11);
            string mantissa = binary.Substring(12);

            // Konwersja binarna na szesnastkową (ręczna implementacja)
            string hex = BinaryToHex(binary);

            Console.WriteLine();
            Console.WriteLine($"Wejście tekstowe: {input}");
            Console.WriteLine($"Wartość double   : {number}");
            Console.WriteLine();
            Console.WriteLine("Zapis IEEE 754 (64 bity):");
            Console.WriteLine($"  {sign} | {exponent} | {mantissa}");
            Console.WriteLine($"Hex: 0x{hex}");

            string outputPath = SaveResultToFile("TRYB 1", input, number, binary, hex);

            Console.WriteLine();
            Console.WriteLine($"✓ Wynik zapisano do pliku:");
            Console.WriteLine($"  {outputPath}");
            Console.WriteLine("Wciśnij dowolny klawisz, aby wrócić do menu...");
            Console.ReadKey();
        }

        // =====================================
        // TRYB 2: KROK PO KROKU + szczegóły
        // =====================================
        private static void RunStepByStepMode()
        {
            Console.Clear();
            Console.WriteLine("== Tryb 2: KROK PO KROKU ==\n");

            if (!ReadAndParseNumber(out double number, out string input, out NumberParser.EdgeCase edgeCase))
                return; // wracamy do menu, jeśli błąd

            // Użycie RĘCZNEJ konwersji z ConversionEngine (bez BitConverter!)
            string binary = ConversionEngine.ConvertToIEEE754Binary(number, edgeCase); ;

            string sign = binary.Substring(0, 1);
            string exponent = binary.Substring(1, 11);
            string mantissa = binary.Substring(12);

            Console.WriteLine($"Wejście tekstowe: {input}");
            Console.WriteLine($"Wartość double   : {number}");
            Console.WriteLine();

            // KROK 1 – znak
            Console.WriteLine("Krok 1: bit znaku (Sign)");
            Console.WriteLine($"  Sign = {sign} (0 = liczba dodatnia, 1 = liczba ujemna)");
            Console.WriteLine("Wciśnij klawisz, aby przejść dalej...");
            Console.ReadKey();

            // KROK 2 – wykładnik
            Console.WriteLine();
            Console.WriteLine("Krok 2: wykładnik (Exponent, 11 bitów)");
            int exponentBiased = BinaryToInt(exponent);
            int exponentUnbiased = exponentBiased - 1023; // bias dla double
            Console.WriteLine($"  Bity exponenta: {exponent}");
            Console.WriteLine($"  Wartość całkowita exponenta (biased): {exponentBiased}");
            Console.WriteLine($"  Wartość po odjęciu biasu 1023 (unbiased): {exponentUnbiased}");
            Console.WriteLine("Wciśnij klawisz, aby przejść dalej...");
            Console.ReadKey();

            // KROK 3 – mantysa
            Console.WriteLine();
            Console.WriteLine("Krok 3: mantysa (Fraction, 52 bity)");
            Console.WriteLine($"  Mantysa: {mantissa}");
            Console.WriteLine("  Rzeczywista mantysa to 1. + ułamek zakodowany w tych bitach.");
            Console.WriteLine("Wciśnij klawisz, aby zobaczyć podsumowanie silnika konwersji...");
            Console.ReadKey();

            // PODSUMOWANIE z istniejącego ConversionEngine
            Console.WriteLine();
            Console.WriteLine("Podsumowanie z ConversionEngine.DisplayBinaryDetails:\n");
            ConversionEngine.DisplayBinaryDetails(number);

            string hex = BinaryToHex(binary);
            string outputPath = SaveResultToFile("TRYB 2", input, number, binary, hex);

            Console.WriteLine();
            Console.WriteLine($"✓ Wynik zapisano do pliku:");
            Console.WriteLine($"  {outputPath}");
            Console.WriteLine("Wciśnij dowolny klawisz, aby wrócić do menu...");
            Console.ReadKey();
        }

        // =====================================
        // Wyświetlenie zawartości pliku output.txt
        // =====================================
        private static void DisplayOutputFile()
        {
            Console.Clear();
            Console.WriteLine("== Zawartość pliku output.txt ==\n");

            string fullPath = GetOutputFilePath();
            Console.WriteLine($"Lokalizacja: {fullPath}\n");

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("⚠ Plik output.txt nie istnieje.");
                Console.WriteLine("  Wykonaj konwersję (opcja 1 lub 2), aby utworzyć plik.");
            }
            else
            {
                try
                {
                    string content = File.ReadAllText(fullPath);
                    
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.WriteLine("Plik jest pusty.");
                    }
                    else
                    {
                        Console.WriteLine(content);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠ Błąd odczytu pliku: {ex.Message}");
                }
            }

            Console.WriteLine("\nWciśnij dowolny klawisz, aby wrócić do menu...");
            Console.ReadKey();
        }

        // =====================================
        // Wyczyszczenie pliku output.txt (z potwierdzeniem)
        // =====================================
        private static void ClearOutputFile()
        {
            Console.Clear();
            Console.WriteLine("== Czyszczenie pliku output.txt ==\n");

            string fullPath = GetOutputFilePath();
            Console.WriteLine($"Lokalizacja: {fullPath}\n");

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("⚠ Plik output.txt nie istnieje - brak danych do wyczyszczenia.");
                Console.WriteLine("\nWciśnij dowolny klawisz, aby wrócić do menu...");
                Console.ReadKey();
                return;
            }

            // Potwierdzenie użytkownika
            Console.WriteLine("⚠ UWAGA: Ta operacja usunie całą zawartość pliku output.txt!");
            Console.Write("Czy na pewno chcesz kontynuować? (T/N): ");
            
            var key = Console.ReadKey(intercept: false);
            Console.WriteLine();
            Console.WriteLine();

            if (key.Key == ConsoleKey.T || key.Key == ConsoleKey.Y)
            {
                try
                {
                    // Wyczyszczenie pliku (nadpisanie pustym contentem)
                    File.WriteAllText(fullPath, string.Empty);
                    
                    Console.WriteLine("✓ Plik output.txt został pomyślnie wyczyszczony.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠ Błąd podczas czyszczenia pliku: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("✗ Operacja anulowana - plik pozostaje niezmieniony.");
            }

            Console.WriteLine("\nWciśnij dowolny klawisz, aby wrócić do menu...");
            Console.ReadKey();
        }

        // =====================================
        // Wspólne wczytywanie i parsowanie liczby
        // =====================================
        private static bool ReadAndParseNumber(out double number, out string input, out NumberParser.EdgeCase edgeCase)
        {
            edgeCase = NumberParser.EdgeCase.None;
            
            Console.Write("Podaj liczbę dziesiętną do konwersji: ");
            input = Console.ReadLine() ?? string.Empty;  // zabezpieczenie przed nullem

            if (string.IsNullOrWhiteSpace(input) || !NumberParser.TryParse(input, out number, out edgeCase))
            {
                Console.WriteLine("Nieprawidłowa liczba. Wciśnij dowolny klawisz, aby wrócić do menu...");
                Console.ReadKey();

                // wartość awaryjna, żeby spełnić wymaganie 'out'
                number = 0;

                return false;
            }

            Console.WriteLine($"Wprowadzona wartość: {input}");
            Console.WriteLine($"Rzeczywista wartość double: {number}");

            if (input.Trim() != number.ToString())
            {
                Console.WriteLine("⚠ Uwaga: wartość została zaokrąglona do najbliższej reprezentowalnej liczby double.");
            }

            return true;
        }

        // =====================================
        // Pobiera pełną ścieżkę do pliku output.txt (w katalogu roboczym aplikacji)
        // =====================================
        private static string GetOutputFilePath()
        {
            // Katalog roboczy aplikacji (bin/Debug/net8.0 lub bin/Release/net8.0)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDirectory, "output.txt");
        }

        // =====================================
        // Zapis wyników do pliku output.txt w katalogu roboczym aplikacji
        // =====================================
        private static string SaveResultToFile(string mode, string input, double number, string binary, string hex)
        {
            string fullPath = GetOutputFilePath();

            try
            {
                using (var writer = new StreamWriter(fullPath, append: true))
                {
                    writer.WriteLine($"[{DateTime.Now}] {mode}");
                    writer.WriteLine($"Wejście : {input}");
                    writer.WriteLine($"double  : {number}");
                    writer.WriteLine($"binary  : {binary}");
                    writer.WriteLine($"hex     : 0x{hex}");
                    writer.WriteLine(new string('-', 60));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Błąd zapisu do pliku: {ex.Message}");
            }

            return fullPath;
        }

        // =====================================
        // RĘCZNA konwersja ciągu binarnego na szesnastkowy
        // =====================================
        private static string BinaryToHex(string binary)
        {
            // Sprawdzenie długości (musi być wielokrotnością 4)
            if (binary.Length % 4 != 0)
            {
                binary = binary.PadLeft(((binary.Length / 4) + 1) * 4, '0');
            }

            string hex = "";
            
            // Konwersja po 4 bity (tetrada) na jeden znak hex
            for (int i = 0; i < binary.Length; i += 4)
            {
                string nibble = binary.Substring(i, 4);
                int value = BinaryToInt(nibble);
                
                // Konwersja 0-15 na znak hex (0-9, A-F)
                if (value < 10)
                    hex += (char)('0' + value);
                else
                    hex += (char)('A' + (value - 10));
            }

            return hex;
        }

        // =====================================
        // RĘCZNA konwersja ciągu binarnego na int
        // =====================================
        private static int BinaryToInt(string binary)
        {
            int result = 0;
            int multiplier = 1;

            // Iteracja od prawej do lewej (od najmniej znaczącego bitu)
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                if (binary[i] == '1')
                {
                    result += multiplier;
                }
                multiplier *= 2;
            }

            return result;
        }
    }
}