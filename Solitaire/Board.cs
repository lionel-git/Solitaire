using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public enum Status { Empty, Pawn, Invalid };

    public abstract class Board
    {
        protected Status[,] _values;

        private int _pawns;
        public int Pawns => _pawns;

        protected List<Triplet> _triplets;

        public IRandom Random { get; set; }

        protected abstract void ResetEmptyPoints();

        protected abstract bool IsInside(int i, int j);

        /// <summary>
        /// Count pawns on the board
        /// </summary>
        private void CountPawns()
        {
            _pawns = 0;
            for (int i = 0; i < _values.GetLength(0); i++)
                for (int j = 0; j < _values.GetLength(1); j++)
                    if (_values[i, j] == Status.Pawn)
                        _pawns++;            
        }

        protected Board(int rows, int columns, List<Point> directions, IRandom random)
        {
            _values = new Status[rows, columns];
            Reset();
            CountPawns();
            InitTriplets(directions);
            Random = random;
            Console.WriteLine($"Init board with {random.GetType()} (seed={random.InitialSeed})");
        }

        private void ResetAllPawns()
        {
            for (int i = 0; i < _values.GetLength(0); i++)
                for (int j = 0; j < _values.GetLength(1); j++)
                {
                    if (IsInside(i, j))
                        _values[i, j] = Status.Pawn;
                    else
                        _values[i, j] = Status.Invalid;
                }
        }

        public void Reset()
        {
            ResetAllPawns();
            ResetEmptyPoints();
            CountPawns();
        }

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
            _pawns--;
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
            //if (_pawns <= 3)
            //{
            //    // Check if pawns are contiguous
            //    int min_i = int.MaxValue;
            //    int max_i = int.MinValue;
            //    int min_j = int.MaxValue;
            //    int max_j = int.MinValue;
            //    for (int i = 0; i < _values.GetLength(0); i++)
            //        for (int j = 0; j < _values.GetLength(1); j++)
            //            if (_values[i, j] == Status.Pawn)
            //            {
            //                min_i = Math.Min(min_i, i);
            //                min_j = Math.Min(min_j, j);
            //                max_i = Math.Max(max_i, i);
            //                max_j = Math.Max(max_j, j);
            //            }                
            //    int di = max_i - min_i;
            //    int dj = max_j - min_j;
            //    int min = Math.Min(di, dj);
            //    int max = Math.Max(di, dj);
            //    if (!(
            //        (Pawns == 2 && (min == 0 && max == 1)) ||
            //        (Pawns == 3 && ((min == 0 && max == 3) || (min == 1 && max == 2)))
            //        ))
            //        return false;
            //}

            var perm = new int[_triplets.Count];
            for (int i = 0; i < perm.Length; i++)
                perm[i] = i;

            int remaining = perm.Length;
            bool found = false;
            do
            {
                int p = Random.Next(0, remaining); //
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
