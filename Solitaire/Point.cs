using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Point
    {
        public int i { get; set; }
        public int j { get; set; }

        public Point(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
    }
}
