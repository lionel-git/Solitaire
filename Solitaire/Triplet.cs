using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Triplet
    {
        public Point[] Pos { get; set; }

        public Triplet(int i, int j, int di, int dj)
        {
            Pos = new Point[3];
            Pos[0] = new Point(i, j);
            Pos[1] = new Point(i + di, j + dj);
            Pos[2] = new Point(i + 2 * di, j + 2 * dj);
        }
    }
}
