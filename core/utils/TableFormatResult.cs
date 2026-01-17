using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.utils
{
    internal class TableFormatResult
    {
        public static void DisplayIEEE754Table(string sign, string exponent, string mantissa)
        {
            Console.WriteLine();
            
            Console.WriteLine("┌────────┬───────────────┬──────────────────────────────────────────────────────────┐");
            Console.WriteLine("│  ZNAK  │   WYKŁADNIK   │                        MANTYSA                           │");
            Console.WriteLine("├────────┼───────────────┼──────────────────────────────────────────────────────────┤");

            Console.WriteLine(
                $"│{sign.PadLeft(4).PadRight(8)}" +
                $"│ {exponent.PadRight(13)} " +
                $"│ {mantissa.PadRight(52)}     │"
            );

            Console.WriteLine("└────────┴───────────────┴──────────────────────────────────────────────────────────┘");
        }
    }
}
