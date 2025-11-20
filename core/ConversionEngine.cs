using System;
using core.cpu;
using core.utils;

namespace core
{
    // Silnik konwersji liczb na format IEEE 754 (64-bit double precision)
    public class ConversionEngine
    {
        // Konwertuje liczbę dziesiętną na 64-bitową reprezentację binarną IEEE 754
        public static string ConvertToIEEE754Binary(double value)
        {
            // Użycie ręcznej konwersji zamiast BitConverter
            CPU64bit cpu = new CPU64bit();
            Float64ConverterUtils.ConvertDecimalToIEEE64(value, cpu);

            // Odczyt wartości z rejestrów
            ulong signBit = cpu.RegSign.Read();
            ulong exponent = cpu.RegExponent.Read();
            ulong mantissa = cpu.RegMantissa.Read();

            // Złożenie wszystkich części w jeden 64-bitowy ciąg
            ulong fullBits = (signBit << 63) | (exponent << 52) | mantissa;
            
            return Convert.ToString((long)fullBits, 2).PadLeft(64, '0');
        }

        // Wyświetla w konsoli szczegółowy podział na znak, wykładnik i mantysę
        public static void DisplayBinaryDetails(double value)
        {
            // Użycie ręcznej konwersji
            CPU64bit cpu = new CPU64bit();
            Float64ConverterUtils.ConvertDecimalToIEEE64(value, cpu);

            ulong signBit = cpu.RegSign.Read();
            ulong exponent = cpu.RegExponent.Read();
            ulong mantissa = cpu.RegMantissa.Read();

            // Konwersja każdej części na binarny string
            string signBinary = signBit.ToString();
            string exponentBinary = Convert.ToString((long)exponent, 2).PadLeft(11, '0');
            string mantissaBinary = Convert.ToString((long)mantissa, 2).PadLeft(52, '0');
            string fullBinary = signBinary + exponentBinary + mantissaBinary;

            Console.WriteLine($"Liczba dziesiętna: {value}");
            Console.WriteLine($"Binary (64-bit): {fullBinary}");
            Console.WriteLine($"Sign: {signBinary}");
            Console.WriteLine($"Exponent: {exponentBinary}");
            Console.WriteLine($"Mantissa: {mantissaBinary}");

            // Weryfikacja poprawności poprzez rekonstrukcję
            double reconstructed = IEEE754Validator.ReconstructFromIEEE754(signBit, exponent, mantissa);
            bool isCorrect = IEEE754Validator.AreEqual(value, reconstructed);
            
            Console.WriteLine($"\nWeryfikacja:");
            Console.WriteLine($"  Wartość oryginalna:      {value}");
            Console.WriteLine($"  Wartość zrekonstruowana: {reconstructed}");
            Console.WriteLine($"  Status: {(isCorrect ? "✓ POPRAWNE" : "✗ BŁĘDNE")}");
        }
    }
}
