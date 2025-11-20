using core.cpu.registers;
using core.cpu;

namespace core.utils
{
    // Funkcje pomocnicze do konwersji liczb dziesiętnych na format IEEE 754 (64-bit)
    public static class Float64ConverterUtils
    {
        // Konwertuje liczbę dziesiętną na format IEEE 754 i zapisuje w rejestrach CPU 64-bit
        public static void ConvertDecimalToIEEE64(double number, CPU64bit cpu)
        {
            // 1. Bit znaku (0 = dodatnia, 1 = ujemna)
            ulong signBit = number < 0 ? 1UL : 0UL;
            cpu.RegSign.Write(signBit, true);

            // 2. Wartość bezwzględna liczby
            double absNumber = System.Math.Abs(number);

            ulong biasedExponent = 0;
            ulong mantissa = 0;

            if (absNumber == 0.0)
            {
                // Zero: all bits are zero
                biasedExponent = 0;
                mantissa = 0;
            }
            else if (double.IsInfinity(absNumber))
            {
                // Nieskończoność
                biasedExponent = 0x7FF;
                mantissa = 0;
            }
            else if (double.IsNaN(number))
            {
                // NaN
                biasedExponent = 0x7FF;
                mantissa = 0x8000000000000UL; // Ustawienie przynajmniej jednego bitu mantysy dla NaN
            }
            else
            {
                // 3. Normalizacja do przedziału [1,2) i obliczenie wykładnika
                int exponent = 0;
                while (absNumber >= 2.0)  // Dzielenie przez 2 dla liczb >= 2
                {
                    absNumber /= 2.0;
                    exponent++;
                }
                while (absNumber < 1.0)  // Mnożenie przez 2 dla liczb < 1
                {
                    absNumber *= 2.0;
                    exponent--;
                }

                if (exponent < -1022)
                {
                    // Subnormal number (liczba zdenormalizowana)
                    biasedExponent = 0;
                    // Dla liczb subnormalnych wykładnik jest stały (-1022), mantysa przechowuje wartość
                    // Przesunięcie mantysy odpowiednio do rzeczywistego wykładnika
                    double fraction = absNumber * System.Math.Pow(2, 1022 + exponent);
                    for (int i = 0; i < 52; i++)
                    {
                        fraction *= 2;
                        if (fraction >= 1.0)
                        {
                            mantissa |= (1UL << (51 - i));
                            fraction -= 1.0;
                        }
                    }
                }
                else if (exponent > 1023)
                {
                    // Overflow to infinity (przepełnienie do nieskończoności)
                    biasedExponent = 0x7FF;
                    mantissa = 0;
                }
                else
                {
                    // Normal number (liczba znormalizowana)
                    // 4. Zapis wykładnika z biasem 1023 (standard IEEE 754)
                    biasedExponent = (ulong)(exponent + 1023);

                    // 5. Generacja mantysy 52-bitowej (część ułamkowa po normalizacji)
                    double fraction = absNumber - 1.0;  // Usunięcie bitu ukrytego
                    for (int i = 0; i < 52; i++)
                    {
                        fraction *= 2;
                        if (fraction >= 1.0)
                        {
                            mantissa |= (1UL << (51 - i));  // Ustawienie odpowiedniego bitu
                            fraction -= 1.0;
                        }
                    }
                }
            }

            cpu.RegExponent.Write(biasedExponent, true);
            cpu.RegMantissa.Write(mantissa, true);
        }
    }
}
