using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab9
{
    internal class Program
    {
        static Semaphore sem = new Semaphore(3, 13);
        static Random random = new Random();
        static int[] getRandArr(int Len)
        {
            int[] arr = new int[Len];
            for (int i = 0; i < Len; i++)
                arr[i] = random.Next(0, 100);
            return arr;
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                var th = new Thread(OutputArrTh) { Name = $"Reader[{i}]" };
                th.Start(getRandArr(random.Next(3, 10)));
            }
            Console.ReadKey(true);

        }
        static void OutputArrTh(object arr)
        {
            sem.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} [{Thread.CurrentThread.ManagedThreadId}]: {String.Join(" ", ((int[])arr))}");
            Thread.Sleep(random.Next(2000, 5000));
            sem.Release();
        }
    }
}
