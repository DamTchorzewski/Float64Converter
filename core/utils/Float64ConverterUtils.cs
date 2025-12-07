using core.cpu.registers;
using core.cpu;

namespace core.utils
{
    // Funkcje pomocnicze do ręcznej konwersji liczb dziesiętnych na format IEEE 754 (64-bit)
    // Pełna implementacja bez użycia BitConverter - wszystkie obliczenia wykonywane manualnie
    public static class Float64ConverterUtils
    {
        // Konwertuje liczbę dziesiętną na format IEEE 754 i zapisuje w rejestrach CPU 64-bit
        // Obsługuje wszystkie przypadki: zero, infinity, NaN, subnormal, normal, overflow
        public static void ConvertDecimalToIEEE64(double number, CPU64bit cpu)
        {
            // Krok 1: Obliczenie bitu znaku (0 = dodatnia, 1 = ujemna)
            // Uwaga: -0.0 ma ustawiony bit znaku mimo że porównanie (< 0) jest fałszywe.
            ulong signBit = 0UL;
            // NaN może mieć różny bit znaku – dopasuj do rzeczywistej reprezentacji środowiska.
            if (MathHelpers.IsNaN(number))
            {
                signBit = ((ulong)BitConverter.DoubleToInt64Bits(number)) >> 63;
            }
            else if (number < 0 || MathHelpers.IsNegativeInfinity(number))
            {
                signBit = 1UL;
            }
            cpu.RegSign.Write(signBit, true);

            // Krok 2: Obliczenie wartości bezwzględnej (ręczna implementacja)
            double absNumber = MathHelpers.Abs(number);

            ulong biasedExponent = 0;
            ulong mantissa = 0;

            // Obsługa przypadków specjalnych IEEE 754
            if (absNumber == 0.0)
            {
                // Zero: wszystkie bity wyniku = 0
                biasedExponent = 0;
                mantissa = 0;
            }
            else if (MathHelpers.IsInfinity(absNumber))
            {
                // Nieskończoność: wykładnik = 2047 (0x7FF), mantysa = 0
                biasedExponent = 0x7FF;
                mantissa = 0;
            }
            else if (MathHelpers.IsNaN(number))
            {
                // NaN: wykładnik = 2047 (0x7FF), mantysa ≠ 0
                biasedExponent = 0x7FF;
                mantissa = 0x8000000000000UL;
            }
            else
            {
                // Krok 3: Normalizacja do przedziału [1,2) i obliczenie wykładnika
                int exponent = 0;
                
                // Dzielenie przez 2 dla liczb >= 2
                while (absNumber >= 2.0)
                {
                    absNumber /= 2.0;
                    exponent++;
                }
                
                // Mnożenie przez 2 dla liczb < 1
                // Maksymalna liczba iteracji: 1074 (zakres wykładnika IEEE 754: -1022 - 52 bity mantysy)
                int iterationCount = 0;
                const int maxIterations = 1075;
                
                while (absNumber < 1.0 && iterationCount < maxIterations)
                {
                    absNumber *= 2.0;
                    exponent--;
                    iterationCount++;
                }

                // Rozróżnienie przypadków: subnormal, overflow, normal
                if (exponent < -1022)
                {
                    // Liczba zdenormalizowana (subnormal): wykładnik < -1022
                    biasedExponent = 0;
                    
                    // Przeskalowanie mantysy dla liczb subnormalnych (ręczne potęgowanie)
                    double fraction = absNumber * MathHelpers.Pow2(1022 + exponent);
                    
                    // Generacja 52-bitowej mantysy
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
                    // Przepełnienie (overflow): konwersja do nieskończoności
                    biasedExponent = 0x7FF;
                    mantissa = 0;
                }
                else
                {
                    // Liczba znormalizowana (normal): -1022 ≤ wykładnik ≤ 1023
                    
                    // Krok 4: Zapis wykładnika z biasem 1023 (standard IEEE 754)
                    biasedExponent = (ulong)(exponent + 1023);

                    // Krok 5: Generacja 52-bitowej mantysy (część ułamkowa po normalizacji)
                    double fraction = absNumber - 1.0;  // Usunięcie ukrytego bitu
                    
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
            }

            // Zapis wykładnika i mantysy do rejestrów procesora
            cpu.RegExponent.Write(biasedExponent, true);
            cpu.RegMantissa.Write(mantissa, true);
        }
    }
}
