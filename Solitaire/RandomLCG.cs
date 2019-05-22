using System;

namespace Solitaire
{
    public class RandomLCG : IRandom
    {
        private readonly uint _a;
        private readonly uint _c;

        private uint _state = 0;
        private uint _backupState = 0;

        public int InitialSeed { get; }

        public RandomLCG(int seed = 0, uint a = 1664525, uint c = 1013904223)
        {
            InitialSeed = seed;
            _a = a;
            _c = c;
            _state = 4_147_235_895 ^ (uint)seed;
            for (int i = 0; i < 100; i++)
                Next(0, 0);
        }

        public void BackupState()
        {
            _backupState = _state;
        }

        public int Next(int min, int max)
        {
            _state = _a * _state + _c;
            var bitSelect = _state << 1;
            double r = (double)bitSelect / 4_294_967_296.0;
            return (int)(r * (max - min));
        }

        public void RestoreState()
        {
            _state = _backupState;
        }
    }
}
