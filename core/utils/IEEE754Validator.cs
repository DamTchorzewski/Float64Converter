namespace core.utils
{
    // Klasa pomocnicza do weryfikacji poprawno?ci konwersji IEEE 754
    public static class IEEE754Validator
    {
        // Weryfikuje poprawno?? konwersji poprzez rekonstrukcj? warto?ci dziesi?tnej
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

            // Obs?uga NaN
            if (exponent == 0x7FF && mantissa != 0)
            {
                return double.NaN;
            }

            double result;

            // Liczby zdenormalizowane (subnormal)
            if (exponent == 0)
            {
                // Rekonstrukcja mantysy bez ukrytego bitu
                double fraction = 0.0;
                for (int i = 0; i < 52; i++)
                {
                    if ((mantissa & (1UL << (51 - i))) != 0)
                    {
                        fraction += System.Math.Pow(2, -(i + 1));
                    }
                }
                result = fraction * System.Math.Pow(2, -1022);
            }
            else // Liczby znormalizowane
            {
                // Rzeczywisty wyk?adnik
                int realExponent = (int)exponent - 1023;

                // Rekonstrukcja mantysy (z ukrytym bitem 1)
                double fraction = 1.0;
                for (int i = 0; i < 52; i++)
                {
                    if ((mantissa & (1UL << (51 - i))) != 0)
                    {
                        fraction += System.Math.Pow(2, -(i + 1));
                    }
                }

                result = fraction * System.Math.Pow(2, realExponent);
            }

            // Zastosowanie znaku
            return signBit == 1 ? -result : result;
        }

        // Porównuje dwie warto?ci double z tolerancj? b??du
        public static bool AreEqual(double a, double b, double tolerance = 1e-10)
        {
            // Obs?uga specjalnych warto?ci
            if (double.IsNaN(a) && double.IsNaN(b)) return true;
            if (double.IsInfinity(a) && double.IsInfinity(b)) return a == b;
            if (a == 0.0 && b == 0.0) return true;

            // Porównanie z tolerancj?
            return System.Math.Abs(a - b) < tolerance || 
                   System.Math.Abs((a - b) / System.Math.Max(System.Math.Abs(a), System.Math.Abs(b))) < tolerance;
        }
    }
}
