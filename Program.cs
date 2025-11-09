using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Konwersja liczby dziesiętnej na format zmiennoprzecinkowy IEEE 754 (64-bit, double precision)");
            Console.WriteLine("Podaj liczbę do konwersji: ");

            string input = Console.ReadLine() ?? "0"; // Wpisanie w formie tekstu, jak użytkownik zostawia puste pole to zakładamy 0.
            bool ujemneZero = input.Trim() == "-0";  // Sprawdzenie czy 0 jest dodatnie czy ujemne.

            double liczba = double.Parse(input, CultureInfo.InvariantCulture);  // konwersja tekstu na liczbe double.

            long Bit = BitConverter.DoubleToInt64Bits(liczba); // zamiana liczby double na bity
            string bityIEEE754 = Convert.ToString(Bit, 2).PadLeft(64, '0'); //Konwersjca liczby 'Bit' na bity w systemie binarnym oraz dopełnienie zerami z lewej aż do uzyskania 64 bitów.
            Console.WriteLine();
            Console.WriteLine(bityIEEE754); // wyświetlenie w konsoli całej liczby 64 bitowej.
            Console.WriteLine();

            Console.WriteLine("z wykładnik   mantysa");  // wyświetlenie podziału

            for (int i = 0; i < 64; i++) // pętla służąca do oddzielnia poszczególnych sekcji bitów od siebie.
            {
                Console.Write(bityIEEE754[i]);
                if (i == 0 || i == 11) Console.Write(' ');  // wstawienie spacji po pierwszym i jedenastym bicie.
            }
            Console.WriteLine();


            int bitZnaku = (int)((Bit >> 63) & 1); // przesunięcie liczby o 63 bity w prawo i usunięcie wszystkich bitów oprócz bitu znaku.
            int wykladnik = (int)((Bit >> 52) & 0x7FF);  // przesunięcie liczby o 52 bity i pozostawienie 11stu bitów wykładnika.
            long mantysa = Bit & ((1L << 52) - 1);  // pozostawienie 52 bitów mantysy.
            int wykladnikBias = wykladnik - 1023;  // obliczenie wykładnika potęgi.

            Console.WriteLine();
            Console.WriteLine($"bit znaku = {bitZnaku}");  // wypisanie bitu znaku
            Console.WriteLine($"wykładnik = {bityIEEE754.Substring(1, 11)} = {wykladnikBias}");   // wypisanie wykładnika w bitach i dziesiętnie

            double mantysa2 = 1.0 + mantysa / Math.Pow(2.0, 52);  // obliczneie mantysy
            Console.WriteLine($"mantysa = 1,{bityIEEE754.Substring(12)} = {mantysa2:0.0000000000}"); // wyświetlenie mantysy

            Console.WriteLine("Jaka to liczba?: "); // tu się rozpoczyna część obsługująca liczby subnormalne, nieskończoności, NaN, liczby ujemne i zera.
            if (wykladnik == 0 && mantysa == 0) // jeżeli wszystkie bity wykładnika i mantysy to 0, wtedy wykonuje się kolejny if.
            {
                if (ujemneZero || bitZnaku == 1) // jeżeli bit znaku to 0, wtedy zero jest dodanie.
                    Console.WriteLine("Liczba to zero ujemne.");
                else  // jeżeli bit znaku jest inny wtedy zero jest ujemne.
                    Console.WriteLine("Liczba to zero dodatnie.");
                double wartosc = Math.Pow(-1, bitZnaku) * 0.0;  // "obliczenie" tego zera tak jakby było obliczane w IEEE754.
                string wartosc2 = (ujemneZero || bitZnaku == 1) ? "-0" : "0";
                Console.WriteLine($"Wartość konwersji IEEE754 = {wartosc2}"); // wyświetlenie wartości.
            }

            else if (wykladnik == 0 && mantysa != 0) // jeżeli poporzedni if był fałszywy, sprawdzamy czy wszystkie bity wykładnika to 0 i czy mantysa ma jakieś inne znaki niż 0.
            {
                Console.WriteLine("Liczba to liczba subnormalna.");
                double wartosc = Math.Pow(-1, bitZnaku) * mantysa * Math.Pow(2.0, -1074); // obliczenie wartości liczby subnormalnej ze wzoru.
                Console.WriteLine($"Wartość konwersji IEEE754 = {wartosc}");  // wyświetlenie wartości.
            }

            else if (wykladnik == 2047 && mantysa == 0) // jeżeli poprzedni if był fałszywy, sprawdzamy czy wszystkie bity wykładnika to 1 i czy mantysa jest zerowa.
            {
                if (bitZnaku == 0) // jeżeli bit znaku to 0, wtedy nieskończoność jest dodatnia.
                {
                    Console.WriteLine("Liczba to nieskończoność dodatnia.");
                    Console.WriteLine("Wartość konwersji IEEE754 = +Infinity");
                }
                else // jeżeli bit znaku jest inny, wtedy nieskończoność jest ujemna.
                {
                    Console.WriteLine("Liczba to nieskończoność ujemna.");
                    Console.WriteLine("Wartość konwersji IEEE754 = -Infinity");
                }
            }

            else if (wykladnik == 2047 && mantysa != 0)  // jeżeli poprzedni if był fałszywy, sprawdzamy czy wszystkie bity wykładnika to 1 i czy mantysa ma jakieś inne znaki niż 0.
            {
                Console.WriteLine("Liczba to liczba NaN (Not a number).");
            }
            else // jeżeli poprzedni if był fałszywy, sprawdzamy czy mamy doczynienia z normalną liczbą, którą liczby z normalnego podstawowego wzoru.
            {
                Console.WriteLine("Liczba to liczba normalna.");
                double wartosc = Math.Pow(-1, bitZnaku) * mantysa2 * Math.Pow(2.0, wykladnikBias); // obliczenie jej ze wzoru.
                Console.WriteLine($"Wartość konwersji IEEE754 = {wartosc}"); // wyświetlenie wartości.
            }

            Console.WriteLine();
            Console.WriteLine("Wciśnij ENTER aby zakończyć.");
            Console.ReadLine();
        }
    }
}