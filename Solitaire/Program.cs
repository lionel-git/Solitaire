using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Solitaire
{
    class Program
    {
        static void TestTriangle()
        {
            var board = new TriangleBoard();
            int N = 680;
            int count = 0;
            int detailed = 679;
            for (int i = 0; i < N; i++)
            {
                if (i == detailed)
                    Console.WriteLine($"==\n{board}");
                // 
                while (board.RandomMove())
                {
                    if (i == detailed)
                        Console.WriteLine($"==\n{board}");
                }
                if (board.Pawns <= 1)
                {
                    Console.WriteLine($"=== {i} =====");
                    Console.WriteLine(board);
                    count++;
                }
                board.Reset();
            }
            Console.WriteLine($"{count}/{N} {100.0 * (double)count / N}%");
        }

        static void TestEnglish(int randomSeed = 0)
        {
            var board = new EnglishBoard(randomSeed);
            int N = 10000000;
            int detailed = 2583218;
            int detailedSeed = 2;
            int minPawns = int.MaxValue;
            int posMinPawns = -1;

            int count = 0;
            for (int i = 0; i < N; i++)
            {
                if (i == detailed && randomSeed == detailedSeed)
                    Console.WriteLine($"==\n{board}");
                // 
                while (board.RandomMove())
                {
                    if (i == detailed && randomSeed == detailedSeed)
                        Console.WriteLine($"==\n{board}");
                }
                int pawns = board.Pawns;
                if (i == detailed && randomSeed == detailedSeed)
                    Console.WriteLine($"i={i} pawns={pawns}");

                if (pawns < minPawns)
                {

                    minPawns = pawns;
                    posMinPawns = i;
                }
                if (board.Pawns <= 1)
                {
                    Console.WriteLine($"Solution found: i={i} (seed={randomSeed})");
                    Console.WriteLine(board);
                    count++;
                }
                board.Reset();

                if (i%1000000==0)
                    Console.WriteLine($"i={i} ({randomSeed})");
            }
            Console.WriteLine($"Min: {minPawns} {posMinPawns} (seed={randomSeed})");
            Console.WriteLine($"{count}/{N} {100.0 * (double)count / N}%");
        }

        static void TestEnglish()
        {
            var tasks = new Task[8];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => TestEnglish(i + 1));
                Thread.Sleep(1000);
                if (tasks[i].Status != TaskStatus.Running)
                    Console.WriteLine("Pb task?");
            }
            Task.WaitAll(tasks);
        }


        static void Main(string[] args)
        {
            TestTriangle();
            //TestEnglish();
        }
    }
}
