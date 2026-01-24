namespace core.utils
{
    // Ręczny parser liczb dziesiętnych i notacji naukowej
    // Nie używa double.TryParse() - całkowicie manualna implementacja parsowania
    public static class NumberParser
    {
        public enum EdgeCase
        {
            None,
            NegativeZero,
            PositiveInfinity,
            NegativeInfinity,
            NaN
        }
        // Parsuje string na wartość double
        // Obsługuje: liczby całkowite, ułamki dziesiętne, notację naukową (e/E), liczby ujemne
        // Akceptuje zarówno kropkę jak i przecinek jako separator dziesiętny
        // Zwraca true jeśli parsowanie się powiodło, false w przeciwnym razie
        public static bool TryParse(string input, out double result, out EdgeCase edgeCase)
        {
            result = 0.0;
            edgeCase = EdgeCase.None;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim();

            // Wykrywanie wartości specjalnych: Infinity, -Infinity, NaN
            if (input.Equals("∞", System.StringComparison.OrdinalIgnoreCase) ||
                input.Equals("Infinity", System.StringComparison.OrdinalIgnoreCase))
            {
                result = double.PositiveInfinity;
                edgeCase = EdgeCase.PositiveInfinity;
                return true;
            }
            if (input.Equals("-∞", System.StringComparison.OrdinalIgnoreCase) ||
                input.Equals("-Infinity", System.StringComparison.OrdinalIgnoreCase))
            {
                result = double.NegativeInfinity;
                edgeCase = EdgeCase.NegativeInfinity;
                return true;
            }
            if (input.Equals("NaN", System.StringComparison.OrdinalIgnoreCase))
            {
                result = double.NaN;
                edgeCase = EdgeCase.NaN;
                return true;
            }
            // Ujednolicone wykrywanie wszystkich wariantów -0 (z kropką/przecinkiem i dowolną liczbą zer)
            // Przykłady dopasowania: "-0", "-0.0", "-0.00", "-0,000"
            if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^-0([.,]0+)?$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                result = -0.0;
                edgeCase = EdgeCase.NegativeZero;
                return true;
            }

            int index = 0;
            bool isNegative = false;

            // Krok 1: Sprawdzenie znaku liczby
            if (input[index] == '-')
            {
                isNegative = true;
                index++;
            }
            else if (input[index] == '+')
            {
                index++;
            }

            if (index >= input.Length)
                return false;

            // Krok 2: Parsowanie części całkowitej i ułamkowej (przed wykładnikiem 'e'/'E')
            double mantissa = 0.0;
            bool hasDot = false;
            double fractionMultiplier = 0.1;
            bool hasDigits = false;

            while (index < input.Length)
            {
                char c = input[index];

                if (c >= '0' && c <= '9')
                {
                    hasDigits = true;
                    int digit = c - '0';

                    if (!hasDot)
                    {
                        // Część całkowita
                        mantissa = mantissa * 10.0 + digit;
                    }
                    else
                    {
                        // Część ułamkowa
                        mantissa += digit * fractionMultiplier;
                        fractionMultiplier /= 10.0;
                    }
                    index++;
                }
                else if ((c == '.' || c == ',') && !hasDot)
                {
                    // Separator dziesiętny (obsługa kropki i przecinka)
                    hasDot = true;
                    index++;
                }
                else if (c == 'e' || c == 'E')
                {
                    // Początek notacji naukowej
                    break;
                }
                else
                {
                    // Nieprawidłowy znak
                    return false;
                }
            }

            if (!hasDigits)
                return false;

            // Krok 3: Parsowanie wykładnika w notacji naukowej (opcjonalne)
            int exponent = 0;
            if (index < input.Length && (input[index] == 'e' || input[index] == 'E'))
            {
                index++;

                if (index >= input.Length)
                    return false;

                bool isNegativeExponent = false;

                // Sprawdzenie znaku wykładnika
                if (input[index] == '-')
                {
                    isNegativeExponent = true;
                    index++;
                }
                else if (input[index] == '+')
                {
                    index++;
                }

                if (index >= input.Length)
                    return false;

                // Parsowanie cyfr wykładnika
                bool hasExponentDigits = false;
                while (index < input.Length)
                {
                    char c = input[index];
                    if (c >= '0' && c <= '9')
                    {
                        hasExponentDigits = true;
                        int digit = c - '0';
                        exponent = exponent * 10 + digit;
                        index++;
                    }
                    else
                    {
                        // Nieprawidłowy znak w wykładniku
                        return false;
                    }
                }

                if (!hasExponentDigits)
                    return false;

                if (isNegativeExponent)
                    exponent = -exponent;
            }

            // Sprawdzenie, czy zostały jakieś nieprzetworzone znaki
            if (index < input.Length)
                return false;

            // Krok 4: Obliczenie końcowej wartości: mantissa × 10^exponent
            result = mantissa;

            if (exponent != 0)
            {
                // Ręczne obliczenie 10^exponent (iteracyjne mnożenie/dzielenie)
                if (exponent > 0)
                {
                    // Mnożenie przez 10^exponent
                    for (int i = 0; i < exponent; i++)
                    {
                        result *= 10.0;
                        
                        // Sprawdzenie overflow do infinity
                        if (MathHelpers.IsInfinity(result))
                            break;
                    }
                }
                else
                {
                    // Dzielenie przez 10^(-exponent)
                    for (int i = 0; i < -exponent; i++)
                    {
                        result /= 10.0;
                        
                        // Sprawdzenie underflow do zera
                        if (result == 0.0)
                            break;
                    }
                }
            }

            // Krok 5: Zastosowanie znaku
            if (isNegative)
                result = -result;

            return true;
        }
    }
}
