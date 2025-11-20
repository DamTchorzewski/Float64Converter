using System;

namespace core
{
    // Silnik konwersji liczb na format IEEE 754 (64-bit double precision)
    public class ConversionEngine
    {
        // Konwertuje liczbę dziesiętną na 64-bitową reprezentację binarną IEEE 754
        public static string ConvertToIEEE754Binary(double value)
        {
            ulong bits = (ulong)BitConverter.DoubleToInt64Bits(value);
            return Convert.ToString((long)bits, 2).PadLeft(64, '0');
        }

        // Wyświetla w konsoli szczegółowy podział na znak, wykładnik i mantysę
        public static void DisplayBinaryDetails(double value)
        {
            string binary = ConvertToIEEE754Binary(value);

            Console.WriteLine($"Liczba dziesiętna: {value}");
            Console.WriteLine($"Binary (64-bit): {binary}");
            Console.WriteLine($"Sign: {binary[0]}");           // Bit znaku (1 bit)
            Console.WriteLine($"Exponent: {binary.Substring(1, 11)}");  // Wykładnik z biasem (11 bitów)
            Console.WriteLine($"Mantissa: {binary.Substring(12, 52)}"); // Mantysa (52 bity)
        }
    }
}
