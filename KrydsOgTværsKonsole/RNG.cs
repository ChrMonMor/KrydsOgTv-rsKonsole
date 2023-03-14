using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrydsOgTværsKonsole
{
    public class RNG
    {
        private static readonly Random Random = new Random();

        public static int Roll(int min, int max)
        {
            return Random.Next(min, max+1);
        }
        public static int NewRandom()
        {
            return Random.Next();
        }
    }
}
