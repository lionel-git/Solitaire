using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class RandomLCG64 : IRandom
    {
        private readonly ulong _a;
        private readonly ulong _c;

        private ulong _state = 0;
        private ulong _backupState = 0;

        public int InitialSeed { get; }

        public RandomLCG64(int seed = 0, ulong a = 6364136223846793005, ulong c = 1442695040888963407)
        {
            InitialSeed = seed;
            _a = a;
            _c = c;
            _state = 11_278_216_135_407_123_515^ (ulong)seed;
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
            var bitSelect = _state;
            double r = (double)bitSelect / 18_446_744_073_709_551_616.0;
            return (int)(r * (max - min));            
        }

        public void RestoreState()
        {
            _state = _backupState;
        }
    }
}
