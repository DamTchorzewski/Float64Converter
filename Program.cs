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
                Console.WriteLine("3 - Wyjście");
                Console.Write("Wybierz opcję (1-3): ");

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

            if (!ReadAndParseNumber(out double number, out string input))
                return; // wracamy do menu, jeśli błąd

            // Pobranie surowych bitów IEEE 754
            long raw = BitConverter.DoubleToInt64Bits(number);
            ulong bits = (ulong)raw;

            // Ciąg 64 bitów jako tekst
            string binary = Convert.ToString(raw, 2).PadLeft(64, '0');

            string sign = binary.Substring(0, 1);
            string exponent = binary.Substring(1, 11);
            string mantissa = binary.Substring(12);

            string hex = bits.ToString("X16");

            Console.WriteLine();
            Console.WriteLine($"Wejście tekstowe: {input}");
            Console.WriteLine($"Wartość double   : {number}");
            Console.WriteLine();
            Console.WriteLine("Zapis IEEE 754 (64 bity):");
            Console.WriteLine($"  {sign} | {exponent} | {mantissa}");
            Console.WriteLine($"Hex: 0x{hex}");

            SaveResultToFile("TRYB 1", input, number, binary, hex);

            Console.WriteLine();
            Console.WriteLine("Wynik zapisano do pliku output.txt");
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

            if (!ReadAndParseNumber(out double number, out string input))
                return; // wracamy do menu, jeśli błąd

            long raw = BitConverter.DoubleToInt64Bits(number);
            ulong bits = (ulong)raw;
            string binary = Convert.ToString(raw, 2).PadLeft(64, '0');

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
            int exponentValue = Convert.ToInt32(exponent, 2);
            int exponentUnbiased = exponentValue - 1023; // bias dla double
            Console.WriteLine($"  Bity exponenta: {exponent}");
            Console.WriteLine($"  Wartość całkowita exponenta (biased): {exponentValue}");
            Console.WriteLine($"  Wartość po odjęciu biasu 1023       : {exponentUnbiased}");
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

            string hex = bits.ToString("X16");
            SaveResultToFile("TRYB 2", input, number, binary, hex);

            Console.WriteLine();
            Console.WriteLine("Wynik zapisano do pliku output.txt");
            Console.WriteLine("Wciśnij dowolny klawisz, aby wrócić do menu...");
            Console.ReadKey();
        }

        // =====================================
        // Wspólne wczytywanie i parsowanie liczby
        // =====================================
        private static bool ReadAndParseNumber(out double number, out string input)
        {
            Console.Write("Podaj liczbę dziesiętną do konwersji: ");
            input = Console.ReadLine() ?? string.Empty;  // zabezpieczenie przed nullem

            if (string.IsNullOrWhiteSpace(input) || !NumberParser.TryParse(input, out number))
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
        // Zapis wyników do pliku output.txt
        // =====================================
        private static void SaveResultToFile(string mode, string input, double number, string binary, string hex)
        {
            string path = "output.txt";

            using (var writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine($"[{DateTime.Now}] {mode}");
                writer.WriteLine($"Wejście : {input}");
                writer.WriteLine($"double  : {number}");
                writer.WriteLine($"binary  : {binary}");
                writer.WriteLine($"hex     : 0x{hex}");
                writer.WriteLine(new string('-', 60));
            }
        }
    }
}
