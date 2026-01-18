using System;
using core.cpu;
using core.utils;

namespace core
{
    // Silnik konwersji i wyświetlania liczb w formacie IEEE 754 (64-bit double precision)
    // Używa ręcznej konwersji binarnej bez BitConverter i Convert.ToString
    public class ConversionEngine
    {
        // Konwertuje liczbę dziesiętną na 64-bitową reprezentację binarną IEEE 754
        // Zwraca string z pełną reprezentacją binarną (znak + wykładnik + mantysa)
        public static string ConvertToIEEE754Binary(double value, NumberParser.EdgeCase edgeCase)
        {
            if (edgeCase == NumberParser.EdgeCase.NegativeZero)
            {
                // Specjalny przypadek -0.0
                // bit znaku = 1, wykładnik = 0, mantysa = 0
                ulong negativeZero64Bits = 1UL << 63;
                // Zwróć 64‑bitowy string binarny
                return MathHelpers.ToBinaryString((long)negativeZero64Bits, 64);
            }
            // Użycie ręcznej konwersji (bez BitConverter)
            CPU64bit cpu = new CPU64bit();
            Float64ConverterUtils.ConvertDecimalToIEEE64(value, cpu);

            // Odczyt wartości z rejestrów procesora
            ulong signBit = cpu.RegSign.Read();
            ulong exponent = cpu.RegExponent.Read();
            ulong mantissa = cpu.RegMantissa.Read();

            // Złożenie komponentów w jeden 64-bitowy ciąg
            ulong fullBits = (signBit << 63) | (exponent << 52) | mantissa;
            
            // Ręczna konwersja na string binarny (bez Convert.ToString)
            return MathHelpers.ToBinaryString((long)fullBits, 64);
        }

        // Wyświetla w konsoli szczegółowy podział liczby na komponenty IEEE 754
        // Pokazuje: liczbę dziesiętną, reprezentację binarną, znak, wykładnik, mantysę
        // Dodatkowo weryfikuje poprawność konwersji przez rekonstrukcję wartości
        public static void DisplayBinaryDetails(double value)
        {
            // Konwersja do IEEE 754
            CPU64bit cpu = new CPU64bit();
            Float64ConverterUtils.ConvertDecimalToIEEE64(value, cpu);

            // Odczyt komponentów z rejestrów
            ulong signBit = cpu.RegSign.Read();
            ulong exponent = cpu.RegExponent.Read();
            ulong mantissa = cpu.RegMantissa.Read();

            // Konwersja każdego komponentu na string binarny (ręczna implementacja)
            string signBinary = signBit.ToString();
            string exponentBinary = MathHelpers.ToBinaryString((long)exponent, 11);
            string mantissaBinary = MathHelpers.ToBinaryString((long)mantissa, 52);
            string fullBinary = signBinary + exponentBinary + mantissaBinary;

            // Wyświetlenie wyników
            Console.WriteLine($"Liczba dziesiętna: {value}");
            Console.WriteLine($"Binary (64-bit): {fullBinary}");
            Console.WriteLine($"Sign: {signBinary}");
            Console.WriteLine($"Exponent: {exponentBinary}");
            Console.WriteLine($"Mantissa: {mantissaBinary}");

            // Weryfikacja poprawności przez rekonstrukcję wartości
            double reconstructed = IEEE754Validator.ReconstructFromIEEE754(signBit, exponent, mantissa);
            bool isCorrect = IEEE754Validator.AreEqual(value, reconstructed);
            
            Console.WriteLine($"\nWeryfikacja:");
            Console.WriteLine($"  Wartość oryginalna:      {value}");
            Console.WriteLine($"  Wartość zrekonstruowana: {reconstructed}");
            Console.WriteLine($"  Status: {(isCorrect ? "✓ POPRAWNE" : "✗ BŁĘDNE")}");
        }
    }
}
