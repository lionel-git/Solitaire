using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public enum Status { Empty, Pawn, Invalid };

    public abstract class Board
    {
        protected Status[,] _values;

        protected List<Triplet> _triplets;

        private Random _random = new Random(0);

        /// <summary>
        /// Count pawns on the board
        /// </summary>
        public int Pawns
        {
            get
            {
                int pawns = 0;
                for (int i = 0; i < _values.GetLength(0); i++)
                    for (int j = 0; j < _values.GetLength(1); j++)
                        if (_values[i, j] == Status.Pawn)
                            pawns++;
                return pawns;
            }
        }

        protected Board(int rows, int columns, List<Point> directions)
        {
            _values = new Status[rows, columns];
            Reset();
            InitTriplets(directions);
        }

        public abstract void Reset();

        protected abstract bool IsInside(int i, int j);
    
        private void InitTriplets(List<Point> directions)
        {
            _triplets = new List<Triplet>();
            foreach (var direction in directions)
                InitTripletDirection(direction.i, direction.j);
        }

        private void InitTripletDirection(int di, int dj)
        {
            for (int i = 0; i < _values.GetLength(0); i++)
                for (int j = 0; j < _values.GetLength(1); j++)
                    if (IsInside(i, j) && IsInside(i + 2 * di, j + 2 * dj))
                    {
                        _triplets.Add(new Triplet(         i,          j,  di,  dj));
                        _triplets.Add(new Triplet(i + 2 * di, j + 2 * dj, -di, -dj));
                    }
        }

        private Status GetStatus(Triplet t, int n)
        {
            return _values[t.Pos[n].i, t.Pos[n].j];
        }

        private void DoMove(Triplet t)
        {
            _values[t.Pos[0].i, t.Pos[0].j] = Status.Empty;
            _values[t.Pos[1].i, t.Pos[1].j] = Status.Empty;
            _values[t.Pos[2].i, t.Pos[2].j] = Status.Pawn;
        }

        private bool TryMove(Triplet t)
        {
            if (GetStatus(t, 0) == Status.Pawn && GetStatus(t, 1) == Status.Pawn && GetStatus(t, 2) == Status.Empty)
            {
                DoMove(t);
                return true;
            }
            else
                return false;
        }

        public bool RandomMove()
        {
            var perm = new int[_triplets.Count];
            for (int i = 0; i < perm.Length; i++)
                perm[i] = i;

            int remaining = perm.Length;
            bool found = false;
            do
            {
                int p = _random.Next(0, remaining); //
                found = TryMove(_triplets[perm[p]]);
                if (!found)
                {
                    // exchange remainig-1 and p
                    int tmp = perm[remaining - 1];
                    perm[remaining - 1] = perm[p];
                    perm[p] = tmp;
                    remaining--;
                }
            }
            while (!found && remaining > 0);
            return found;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = _values.GetLength(0)-1; i >=0 ; i--)
            {
                for (int j = 0; j < _values.GetLength(1); j++)
                {
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
                if (i>0)
                 sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
