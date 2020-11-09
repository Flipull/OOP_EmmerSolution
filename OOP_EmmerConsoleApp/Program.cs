
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

            e1.isFull += FullListener;
            e1.willOverflow += OverflowContinueingListener;
            e1.willOverflow += OverflowStoppingListener;
            e1.hasOverflown += OverflownListener;

            o1.isFull += FullListener;
            o1.willOverflow += OverflowContinueingListener;
            o1.hasOverflown += OverflownListener;

            r1.isFull += FullListener;
            r1.willOverflow += OverflowContinueingListener;
            r1.hasOverflown += OverflownListener;

            Console.WriteLine("e3->e1");
            e1.Fill(e3);

            Console.WriteLine("50L->o1");
            o1.Fill(50);
            Console.WriteLine("e1->o1");
            o1.Fill(e1);
            Console.WriteLine("e3->o1");
            o1.Fill(e3);
            Console.WriteLine("o1->r1");
            r1.Fill(o1);
        }
    }
}
