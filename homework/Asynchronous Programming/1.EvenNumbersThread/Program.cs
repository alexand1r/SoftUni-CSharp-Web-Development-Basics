namespace _1.EvenNumbersThread
{
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            var range = Console.ReadLine().Split();
            int min = int.Parse(range[0]);
            int max = int.Parse(range[1]);

            Thread evens = new Thread(() => PrintEvenNumbers(min, max));
            evens.Start();
            evens.Join();
            Console.WriteLine("Finished");
        }

        private static void PrintEvenNumbers(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                if (i % 2 == 0)
                    Console.WriteLine(i);
            }
        }
    }
}
