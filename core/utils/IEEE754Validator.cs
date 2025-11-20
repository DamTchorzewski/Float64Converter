namespace core.utils
{
    // Klasa pomocnicza do weryfikacji poprawno?ci konwersji IEEE 754
    // Umo?liwia rekonstrukcj? warto?ci dziesi?tnej z bitów oraz porównywanie z tolerancj? b??du
    public static class IEEE754Validator
    {
        // Rekonstruuje warto?? double z komponentów IEEE 754 (znak, wyk?adnik, mantysa)
        // Obs?uguje wszystkie przypadki specjalne: zero, infinity, NaN, subnormal, normal
        public static double ReconstructFromIEEE754(ulong signBit, ulong exponent, ulong mantissa)
        {
            // Obs?uga zera
            if (exponent == 0 && mantissa == 0)
            {
                return signBit == 1 ? -0.0 : 0.0;
            }

            // Obs?uga niesko?czono?ci
            if (exponent == 0x7FF && mantissa == 0)
            {
                return signBit == 1 ? double.NegativeInfinity : double.PositiveInfinity;
            }

            // Obs?uga NaN (Not a Number)
            if (exponent == 0x7FF && mantissa != 0)
            {
                return double.NaN;
            }

            double result;

            // Liczby zdenormalizowane (subnormal) - wyk?adnik = 0, mantysa ? 0
            if (exponent == 0)
            {
                // Rekonstrukcja mantysy bez ukrytego bitu (r?czne pot?gowanie)
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
                // Rzeczywisty wyk?adnik (z odj?ciem biasu 1023)
                int realExponent = (int)exponent - 1023;

                // Rekonstrukcja mantysy z ukrytym bitem 1 (r?czne pot?gowanie)
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

        // Porównuje dwie warto?ci double z konfigurowaln? tolerancj? b??du
        // U?ywa r?cznych implementacji operacji matematycznych
        public static bool AreEqual(double a, double b, double tolerance = 1e-10)
        {
            // Obs?uga warto?ci specjalnych (r?czne sprawdzanie)
            if (MathHelpers.IsNaN(a) && MathHelpers.IsNaN(b)) return true;
            if (MathHelpers.IsInfinity(a) && MathHelpers.IsInfinity(b)) return a == b;
            if (a == 0.0 && b == 0.0) return true;

            // Porównanie z tolerancj? (r?czne obliczenia)
            double diff = MathHelpers.Abs(a - b);
            double maxAbs = MathHelpers.Max(MathHelpers.Abs(a), MathHelpers.Abs(b));
            
            return diff < tolerance || MathHelpers.Abs((a - b) / maxAbs) < tolerance;
        }
    }
}
