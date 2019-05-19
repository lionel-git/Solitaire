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
                board.SaveRandomState();
                while (board.RandomMove()) {}
                if (board.Pawns <= 1)
                {
                    Console.WriteLine($"=== Found at test {i} (seed={board.Seed}) =====");
                    board.Reset();
                    board.RestoreRandomState();
                    Console.WriteLine(board);
                    while (board.RandomMove())
                    {
                        Console.WriteLine($"==\n{board}");
                    }
                    count++;
                }
                board.Reset();
                if (i % 1000000 == 0)
                    Console.WriteLine($"i={i} ({board.Seed})");
            }
            Console.WriteLine($"{count}/{N} {100.0 * (double)count / N}%");
        }

        static void TestTriangle()
        {
            var board = new TriangleBoard();
            TestBoard(board, 1000);
        }

        static void TestEnglish()
        {
            var sw = new Stopwatch();
            sw.Restart();
            int N = 8000000;
            var tasks = new Task[Environment.ProcessorCount];
            for (int i = 0; i < tasks.Length; i++)
            {
                var board = new EnglishBoard(i + 1);
                tasks[i] = Task.Run(() => TestBoard(board, N/tasks.Length));
                Thread.Sleep(100);
            }
            Task.WaitAll(tasks);
            Console.WriteLine($"Elapsed: {sw.Elapsed} ({N}, {tasks.Length})");
        }

        static void Test()
        {
            var board = new TriangleBoard();

            board.SaveRandomState();
            for (int i = 0; i < 5; i++)
                board.RandomMove();
            board.RestoreRandomState();
            board.SaveRandomState();
            for (int i = 0; i < 7; i++)
                board.RandomMove();
            board.RestoreRandomState();

            board.Reset();
            TestBoard(board, 1000);
        }

        static void Main(string[] args)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
            //Test();
            //TestTriangle();
            TestEnglish();
        }
    }
}
