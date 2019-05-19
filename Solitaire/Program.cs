using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new TriangleBoard();
            int N = 680;
            int count = 0;
            for (int i = 0; i < N; i++)
            {
               // 
                while (board.RandomMove())
                {
                    if (i==679)
                      Console.WriteLine($"==\n{board}");
                  //  Console.WriteLine(board.Pawns);
                }
                if (board.Pawns <= 1)
                {
                    Console.WriteLine($"=== {i} =====");
                    Console.WriteLine(board);
                    count++;
                }
                board.Reset();
            }
            Console.WriteLine($"{count}/{N} {100.0*(double)count/N}%");
        }
    }
}
