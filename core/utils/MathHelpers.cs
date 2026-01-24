namespace core.utils
{
    // Ręczne implementacje podstawowych funkcji matematycznych
    // Wszystkie funkcje obliczane bez użycia System.Math - pełna implementacja manualna
    public static class MathHelpers
    {
        // Oblicza wartość bezwzględną liczby (bez System.Math.Abs)
        public static double Abs(double value)
        {
            return value < 0 ? -value : value;
        }

        // Oblicza potęgę 2^n dla wykładników całkowitych (bez System.Math.Pow)
        // Używa iteracyjnego mnożenia dla wykładników dodatnich, dzielenia dla ujemnych
        public static double Pow2(int exponent)
        {
            if (exponent == 0)
                return 1.0;

            if (exponent > 0)
            {
                // Dla dodatnich wykładników: iteracyjne mnożenie przez 2
                double result = 1.0;
                for (int i = 0; i < exponent; i++)
                {
                    result *= 2.0;
                }
                return result;
            }
            else
            {
                // Dla ujemnych wykładników: iteracyjne dzielenie przez 2
                double result = 1.0;
                for (int i = 0; i < -exponent; i++)
                {
                    result /= 2.0;
                }
                return result;
            }
        }

        // Sprawdza czy wartość jest NaN (Not a Number) bez użycia double.IsNaN()
        // NaN jest jedyną wartością, która nie jest równa samej sobie (value != value)
        public static bool IsNaN(double value)
        {
#pragma warning disable CS1718 
            return value != value;  // Jedyny sposób na wykrycie NaN bez double.IsNaN()
#pragma warning restore CS1718
        }

        // Sprawdza czy wartość jest nieskończonością (bez double.IsInfinity)
        // Porównuje z maksymalną skończoną wartością double (~1.79E+308)
        public static bool IsInfinity(double value)
        {
            // Infinity nie jest NaN
            if (IsNaN(value))
                return false;

            // Porównanie z największą możliwą wartością double (aproksymacja double.MaxValue)
            double maxValue = 1.7976931348623157E+308;

            return value > maxValue || value < -maxValue;
        }

        // Sprawdza czy wartość jest dodatnią nieskończonością (+∞)
        public static bool IsPositiveInfinity(double value)
        {
            return IsInfinity(value) && value > 0;
        }

        // Sprawdza czy wartość jest ujemną nieskończonością (-∞)
        public static bool IsNegativeInfinity(double value)
        {
            return IsInfinity(value) && value < 0;
        }

        // Zwraca większą z dwóch wartości (bez System.Math.Max)
        public static double Max(double a, double b)
        {
            return a > b ? a : b;
        }

        // Konwertuje liczbę na string binarny o określonej minimalnej długości (bez Convert.ToString)
        // Używa operacji bitowych (przesunięcia i AND) do ekstrakcji kolejnych bitów
        public static string ToBinaryString(ulong value, int minLength)
        {
            if (value == 0)
                return "".PadLeft(minLength, '0');

            string result = "";
            ulong temp = value;

            // Ekstrakcja bitów od najmniej znaczącego
            while (temp > 0)
            {
                result = (temp & 1) + result;
                temp >>= 1;
            }

            // Dopełnienie zerami z lewej strony do minimalnej długości
            while (result.Length < minLength)
            {
                result = "0" + result;
            }

            return result;
        }

        // Konwertuje liczbę long na string binarny (przeciążenie dla long)
        public static string ToBinaryString(long value, int minLength)
        {
            return ToBinaryString((ulong)value, minLength);
        }
    }
}
