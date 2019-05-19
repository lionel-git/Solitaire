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
            var board = new Board();
            int N = 398;
            int count = 0;
            for (int i = 0; i < N; i++)
            {
               // 
                while (board.RandomMove())
                {
                    if (i==397)
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
            Console.WriteLine($"{count}/{N}");
        }
    }
}
