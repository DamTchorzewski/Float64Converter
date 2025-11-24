namespace core.utils
{
    // R?czny parser liczb dziesi?tnych i notacji naukowej
    // Nie u?ywa double.TryParse() - ca?kowicie manualna implementacja parsowania
    public static class NumberParser
    {
        // Parsuje string na warto?? double
        // Obs?uguje: liczby ca?kowite, u?amki dziesi?tne, notacj? naukow? (e/E), liczby ujemne
        // Akceptuje zarówno kropk? jak i przecinek jako separator dziesi?tny
        // Zwraca true je?li parsowanie si? powiod?o, false w przeciwnym razie
        public static bool TryParse(string input, out double result)
        {
            result = 0.0;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim();

            // Wykrywanie warto?ci specjalnych: Infinity, -Infinity, NaN
            if (input.Equals("?", System.StringComparison.OrdinalIgnoreCase) ||
                input.Equals("Infinity", System.StringComparison.OrdinalIgnoreCase))
            {
                result = double.PositiveInfinity;
                return true;
            }
            if (input.Equals("-?", System.StringComparison.OrdinalIgnoreCase) ||
                input.Equals("-Infinity", System.StringComparison.OrdinalIgnoreCase))
            {
                result = double.NegativeInfinity;
                return true;
            }
            if (input.Equals("NaN", System.StringComparison.OrdinalIgnoreCase))
            {
                result = double.NaN;
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

            // Krok 2: Parsowanie cz??ci ca?kowitej i u?amkowej (przed wyk?adnikiem 'e'/'E')
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
                        // Cz??? ca?kowita
                        mantissa = mantissa * 10.0 + digit;
                    }
                    else
                    {
                        // Cz??? u?amkowa
                        mantissa += digit * fractionMultiplier;
                        fractionMultiplier /= 10.0;
                    }
                    index++;
                }
                else if ((c == '.' || c == ',') && !hasDot)
                {
                    // Separator dziesi?tny (obs?uga kropki i przecinka)
                    hasDot = true;
                    index++;
                }
                else if (c == 'e' || c == 'E')
                {
                    // Pocz?tek notacji naukowej
                    break;
                }
                else
                {
                    // Nieprawid?owy znak
                    return false;
                }
            }

            if (!hasDigits)
                return false;

            // Krok 3: Parsowanie wyk?adnika w notacji naukowej (opcjonalne)
            int exponent = 0;
            if (index < input.Length && (input[index] == 'e' || input[index] == 'E'))
            {
                index++;

                if (index >= input.Length)
                    return false;

                bool isNegativeExponent = false;

                // Sprawdzenie znaku wyk?adnika
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

                // Parsowanie cyfr wyk?adnika
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
                        // Nieprawid?owy znak w wyk?adniku
                        return false;
                    }
                }

                if (!hasExponentDigits)
                    return false;

                if (isNegativeExponent)
                    exponent = -exponent;
            }

            // Sprawdzenie, czy zosta?y jakie? nieprzetworzone znaki
            if (index < input.Length)
                return false;

            // Krok 4: Obliczenie ko?cowej warto?ci: mantissa × 10^exponent
            result = mantissa;

            if (exponent != 0)
            {
                // R?czne obliczenie 10^exponent (iteracyjne mno?enie/dzielenie)
                if (exponent > 0)
                {
                    // Mno?enie przez 10^exponent
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
