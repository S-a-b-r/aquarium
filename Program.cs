using System;

namespace aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Aquarium();
            while (true)
            {
                while (Console.KeyAvailable)
                { 
                    var ck = Console.ReadKey(true);
                    if (ck.Key == ConsoleKey.Spacebar)
                        a.AddFood();
                    if (ck.Key == ConsoleKey.F)
                        a.AddFish();
                }

                a.Update();
                a.Draw();
                System.Threading.Thread.Sleep(200);
            }
        }
    }
}
