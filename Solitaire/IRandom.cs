using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    interface IRandom
    {
        int Next(int min, int max);
        void BackupState();
        void RestoreState();
    }
}
