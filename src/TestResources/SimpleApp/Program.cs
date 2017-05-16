using System;
using System.Threading;

namespace SimpleApp
{
    internal class Program
    {
        private static void Main()
        {
            var i = 0;
            Console.WriteLine("Start loop");
            do
            {
                Console.WriteLine("loop index = " + i);

                Thread.Sleep(1000);
                i++;
            } while (i < 5);
        }
    }
}