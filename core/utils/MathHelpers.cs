namespace core.utils
{
    // R?czne implementacje podstawowych funkcji matematycznych
    // Wszystkie funkcje obliczane bez u?ycia System.Math - pe?na implementacja manualna
    public static class MathHelpers
    {
        // Oblicza warto?? bezwzgl?dn? liczby (bez System.Math.Abs)
        public static double Abs(double value)
        {
            return value < 0 ? -value : value;
        }

        // Oblicza pot?g? 2^n dla wyk?adników ca?kowitych (bez System.Math.Pow)
        // U?ywa iteracyjnego mno?enia dla wyk?adników dodatnich, dzielenia dla ujemnych
        public static double Pow2(int exponent)
        {
            if (exponent == 0)
                return 1.0;

            if (exponent > 0)
            {
                // Dla dodatnich wyk?adników: iteracyjne mno?enie przez 2
                double result = 1.0;
                for (int i = 0; i < exponent; i++)
                {
                    result *= 2.0;
                }
                return result;
            }
            else
            {
                // Dla ujemnych wyk?adników: iteracyjne dzielenie przez 2
                double result = 1.0;
                for (int i = 0; i < -exponent; i++)
                {
                    result /= 2.0;
                }
                return result;
            }
        }

        // Sprawdza czy warto?? jest NaN (Not a Number) bez u?ycia double.IsNaN()
        // NaN jest jedyn? warto?ci?, która nie jest równa samej sobie (value != value)
        public static bool IsNaN(double value)
        {
#pragma warning disable CS1718 // Comparison made to same variable
            return value != value;  // Jedyny sposób na wykrycie NaN bez double.IsNaN()
#pragma warning restore CS1718
        }

        // Sprawdza czy warto?? jest niesko?czono?ci? (bez double.IsInfinity)
        // Porównuje z maksymaln? sko?czon? warto?ci? double (~1.79E+308)
        public static bool IsInfinity(double value)
        {
            // Infinity nie jest NaN
            if (IsNaN(value))
                return false;

            // Porównanie z najwi?ksz? mo?liw? warto?ci? double (aproksymacja double.MaxValue)
            double maxValue = 1.7976931348623157E+308;
            
            return value > maxValue || value < -maxValue;
        }

        // Sprawdza czy warto?? jest dodatni? niesko?czono?ci? (+?)
        public static bool IsPositiveInfinity(double value)
        {
            return IsInfinity(value) && value > 0;
        }

        // Sprawdza czy warto?? jest ujemn? niesko?czono?ci? (-?)
        public static bool IsNegativeInfinity(double value)
        {
            return IsInfinity(value) && value < 0;
        }

        // Zwraca wi?ksz? z dwóch warto?ci (bez System.Math.Max)
        public static double Max(double a, double b)
        {
            return a > b ? a : b;
        }

        // Konwertuje liczb? na string binarny o okre?lonej minimalnej d?ugo?ci (bez Convert.ToString)
        // U?ywa operacji bitowych (przesuni?cia i AND) do ekstrakcji kolejnych bitów
        public static string ToBinaryString(ulong value, int minLength)
        {
            if (value == 0)
                return "".PadLeft(minLength, '0');

            string result = "";
            ulong temp = value;

            // Ekstrakcja bitów od najmniej znacz?cego
            while (temp > 0)
            {
                result = (temp & 1) + result;
                temp >>= 1;
            }

            // Dope?nienie zerami z lewej strony do minimalnej d?ugo?ci
            while (result.Length < minLength)
            {
                result = "0" + result;
            }

            return result;
        }

        // Konwertuje liczb? long na string binarny (przeci??enie dla long)
        public static string ToBinaryString(long value, int minLength)
        {
            return ToBinaryString((ulong)value, minLength);
        }
    }
}
