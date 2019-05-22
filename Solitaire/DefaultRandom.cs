using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Solitaire
{
    public class DefaultRandom : IRandom
    {
        private Random _random;
        private BinaryFormatter _formatter = new BinaryFormatter();
        private MemoryStream _saveStream = new MemoryStream();

        public DefaultRandom(int seed)
        {
            _random = new Random(seed);
        }

        public void BackupState()
        {
            _saveStream.Seek(0, SeekOrigin.Begin);
            _formatter.Serialize(_saveStream, _random);
        }

        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

        public void RestoreState()
        {
            _saveStream.Seek(0, SeekOrigin.Begin);
            _random = (Random)_formatter.Deserialize(_saveStream);
        }
    }
}
