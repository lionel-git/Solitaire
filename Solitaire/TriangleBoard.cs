﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class TriangleBoard : Board
    {
        static List<Point> _directions = new List<Point>() { new Point(1, 0), new Point(0, 1), new Point(1, -1) };

        public TriangleBoard(IRandom random) : base(5, 5, _directions, random)
        {
        }

        protected override void ResetEmptyPoints()
        {
            _values[2, 2] = Status.Empty;
        }

        protected override bool IsInside(int i, int j)
        {
            return i >= 0 && j >= 0 && i + j < 5;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = _values.GetLength(0) - 1; i >= 0; i--)
            {
                sb.Append(' ', i);
                for (int j = 0; j < _values.GetLength(1); j++)
                {
                    sb.Append(' ');
                    switch (_values[i, j])
                    {
                        case Status.Invalid:
                            sb.Append(" ");
                            break;
                        case Status.Empty:
                            sb.Append(".");
                            break;
                        case Status.Pawn:
                            sb.Append("o");
                            break;
                    }
                }
                if (i > 0)
                    sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
