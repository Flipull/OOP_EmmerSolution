
using LiquidContainerLibrary;
using System;

namespace OOP_EmmerConsoleApp
{
    class Program
    {
        static void FullListener()
        {
            Console.WriteLine($"is full");
        }
        static void OverflownListener(uint amount)
        {
            Console.WriteLine($"overflown: {amount}L");
        }
        static bool OverflowStoppingListener()
        {
            Console.WriteLine($"would overflow - stopped");
            return false;
        }
        static bool OverflowContinueingListener()
        {
            Console.WriteLine($"would overflow - continue");
            return true;
        }

        static void Main(string[] args)
        {

            var e1 = new Emmer();
            var e3 = new Emmer(15,35);
            var o1 = new Olievat(25);
            var r1 = new Regenton(5, Regenton.Size.Small);

            e1.Full += FullListener;
            e1.Overflows += OverflowContinueingListener;
            e1.Overflows += OverflowStoppingListener;
            e1.Overflown += OverflownListener;

            o1.Full += FullListener;
            o1.Overflows += OverflowContinueingListener;
            o1.Overflown += OverflownListener;

            r1.Full += FullListener;
            r1.Overflows += OverflowContinueingListener;
            r1.Overflown += OverflownListener;

            Console.WriteLine("e3->e1");
            e1.Fill(e3);

            Console.WriteLine("50L->o1");
            o1.Fill(50);
            Console.WriteLine("e1->o1");
        }
    }
}
