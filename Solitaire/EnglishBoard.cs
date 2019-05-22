using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class EnglishBoard : Board
    {
        static List<Point> _directions = new List<Point>() { new Point(1, 0), new Point(0, 1) };

        public EnglishBoard(IRandom random) : base(7, 7, _directions, random)
        {
        }

        protected override void ResetEmptyPoints()
        {
            _values[1, 3] = Status.Empty;
            _values[2, 3] = Status.Empty;
        }

        protected override bool IsInside(int i, int j)
        {
            return (i >= 2 && i <= 4 && j >= 0 && j <= 6) || (j >= 2 && j <= 4 && i >= 0 && i <= 6);
        }
    }
}
