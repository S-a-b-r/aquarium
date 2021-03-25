using System;

namespace aquarium
{
    class ConsoleEx
    {
        public static void Print (int x, int y, string s, ConsoleColor c = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(s);
        }
    }
}
