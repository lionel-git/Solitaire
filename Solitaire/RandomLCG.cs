using System;

namespace Solitaire
{
    public class RandomLCG : IRandom
    {
        private readonly uint _a;
        private readonly uint _c;

        private uint _state = 0;
        private uint _backupState = 0;

        public RandomLCG(uint seed, uint a = 1664525, uint c = 1013904223)
        {
            _a = a;
            _c = c;
            _state = seed;
        }

        public void BackupState()
        {
            _backupState = _state;
        }

        public int Next(int min, int max)
        {
            _state = _a * _state + _c;
            return (int)(((UInt64)(max - min) * (UInt64)(_state)) >> 32);
        }

        public void RestoreState()
        {
            _state = _backupState;
        }
    }
}
