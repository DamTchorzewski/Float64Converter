using System;

namespace core.accessibility
{
    internal static class AccessibilityColors
    {
        public static void Reset()
        {
            Console.ResetColor();
            Console.Clear();
        }

        public static void HighContrast()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        public static void LightMode()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
        }

        public static void ColorBlindFriendly()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
        }

        public static void LowLight()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }
    }
}
