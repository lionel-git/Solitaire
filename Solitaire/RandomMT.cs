using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Solitaire
{
    // cf http://www.prowaretech.com/Computer/DotNet/Mersenne
    class RandomMT : IRandom
    {
        const int MERS_N = 624;
        const int MERS_M = 397;
        const int MERS_R = 31;
        const int MERS_U = 11;
        const int MERS_S = 7;
        const int MERS_T = 15;
        const int MERS_L = 18;
        const uint MERS_A = 0x9908B0DF;
        const uint MERS_B = 0x9D2C5680;
        const uint MERS_C = 0xEFC60000;

        uint[] mt = new uint[MERS_N];          // state vector
        uint mti;                            // index into mt

        uint[] mt_b = new uint[MERS_N];          // state vector
        uint mti_b;                            // index into mt

        public int InitialSeed { get; }

        public RandomMT(int seed)
        {
            InitialSeed = seed;
            mt[0] = (uint)seed;
            for (mti = 1; mti < MERS_N; mti++)
            {
                mt[mti] = (1812433253U * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + mti);
            }
        }

        public void BackupState()
        {
            mt.CopyTo(mt_b, 0);
            mti_b = mti;
        }

        public int Next(int min, int max)
        {
            var r = (double)BRandom() / 4_294_967_296.0;
            return min + (int)(r * (max - min));
        }

        public void RestoreState()
        {
            mt_b.CopyTo(mt, 0);
            mti = mti_b;
        }

        private uint BRandom()
        {
            // generate 32 random bits
            uint y;

            if (mti >= MERS_N)
            {
                const uint LOWER_MASK = 2147483647;
                const uint UPPER_MASK = 0x80000000;
                uint[] mag01 = { 0, MERS_A };

                int kk;
                for (kk = 0; kk < MERS_N - MERS_M; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + MERS_M] ^ (y >> 1) ^ mag01[y & 1];
                }

                for (; kk < MERS_N - 1; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + (MERS_M - MERS_N)] ^ (y >> 1) ^ mag01[y & 1];
                }

                y = (mt[MERS_N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
                mt[MERS_N - 1] = mt[MERS_M - 1] ^ (y >> 1) ^ mag01[y & 1];
                mti = 0;
            }

            y = mt[mti++];

            // Tempering (May be omitted):
            y ^= y >> MERS_U;
            y ^= (y << MERS_S) & MERS_B;
            y ^= (y << MERS_T) & MERS_C;
            y ^= y >> MERS_L;
            return y;
        }
    }
}
