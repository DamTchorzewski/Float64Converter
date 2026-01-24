namespace core.utils
{
    // Klasa pomocnicza do weryfikacji poprawności konwersji IEEE 754
    // Umożliwia rekonstrukcję wartości dziesiętnej z bitów oraz porównywanie z tolerancją błędu
    public static class IEEE754Validator
    {
        // Rekonstruuje wartość double z komponentów IEEE 754 (znak, wykładnik, mantysa)
        // Obsługuje wszystkie przypadki specjalne: zero, infinity, NaN, subnormal, normal
        public static double ReconstructFromIEEE754(ulong signBit, ulong exponent, ulong mantissa)
        {
            // Obsługa zera
            if (exponent == 0 && mantissa == 0)
            {
                return signBit == 1 ? -0.0 : 0.0;
            }

            // Obsługa nieskończoności
            if (exponent == 0x7FF && mantissa == 0)
            {
                return signBit == 1 ? double.NegativeInfinity : double.PositiveInfinity;
            }

            // Obsługa NaN (Not a Number)
            if (exponent == 0x7FF && mantissa != 0)
            {
                return double.NaN;
            }

            double result;

            // Liczby zdenormalizowane (subnormal) - wykładnik = 0, mantysa ≠ 0
            if (exponent == 0)
            {
                // Rekonstrukcja mantysy bez ukrytego bitu (ręczne potęgowanie)
                double fraction = 0.0;
                for (int i = 0; i < 52; i++)
                {
                    if ((mantissa & (1UL << (51 - i))) != 0)
                    {
                        fraction += MathHelpers.Pow2(-(i + 1));
                    }
                }
                result = fraction * MathHelpers.Pow2(-1022);
            }
            else // Liczby znormalizowane
            {
                // Rzeczywisty wykładnik (z odjęciem biasu 1023)
                int realExponent = (int)exponent - 1023;

                // Rekonstrukcja mantysy z ukrytym bitem 1 (ręczne potęgowanie)
                double fraction = 1.0;
                for (int i = 0; i < 52; i++)
                {
                    if ((mantissa & (1UL << (51 - i))) != 0)
                    {
                        fraction += MathHelpers.Pow2(-(i + 1));
                    }
                }

                result = fraction * MathHelpers.Pow2(realExponent);
            }

            // Zastosowanie znaku
            return signBit == 1 ? -result : result;
        }

        // Porównuje dwie wartości double z konfigurowalną tolerancją błędu
        // Używa ręcznych implementacji operacji matematycznych
        public static bool AreEqual(double a, double b, double tolerance = 1e-10)
        {
            // Obsługa wartości specjalnych (ręczne sprawdzanie)
            if (MathHelpers.IsNaN(a) && MathHelpers.IsNaN(b)) return true;
            if (MathHelpers.IsInfinity(a) && MathHelpers.IsInfinity(b)) return a == b;
            if (a == 0.0 && b == 0.0) return true;

            // Porównanie z tolerancją (ręczne obliczenia)
            double diff = MathHelpers.Abs(a - b);
            double maxAbs = MathHelpers.Max(MathHelpers.Abs(a), MathHelpers.Abs(b));
            
            return diff < tolerance || MathHelpers.Abs((a - b) / maxAbs) < tolerance;
        }
    }
}
