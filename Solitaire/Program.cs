using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Solitaire
{
    class Program
    {
        static void TestBoard(Board board, int N)
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                if (board.TrySolve())
                {
                    count++;
                    Console.WriteLine($"Found at i={i} ({board.Random.InitialSeed})");
                }
                board.Reset();
                if (i % 1000000 == 0)
                    Console.WriteLine($"i={i} ({board.Random.InitialSeed})");
            }
            Console.WriteLine($"{count}/{N} {100.0 * (double)count / N}%");
        }

        static void TestTriangle()
        {
            var board = new TriangleBoard(new RandomLCG());
            TestBoard(board, 1000);
        }

        static void TestEnglish()
        {
            var sw = new Stopwatch();
            sw.Restart();
            int N = 16_000_000;
            var tasks = new Task[Environment.ProcessorCount];
            for (int i = 0; i < tasks.Length; i++)
            {
                var board = new EnglishBoard(new RandomLCG(i + 1));
                tasks[i] = Task.Run(() => TestBoard(board, N / tasks.Length));
                Thread.Sleep(100);
            }
            Task.WaitAll(tasks);
            Console.WriteLine($"Elapsed: {sw.Elapsed} ({N}, {tasks.Length})");
        }

        static void TestRandom()
        {
            var lcg1 = new RandomLCG(1);
            var lcg2 = new RandomLCG(2);
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{lcg1.Next(0, 100)}");
                Console.WriteLine($" {lcg2.Next(0, 100)}");
            }
        }

        static void Main(string[] args)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
            // TestRandom();

            //TestTriangle();
            TestEnglish();
        }
    }
}
