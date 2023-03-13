using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KrydsOgTværsKonsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] arg = WordListFactory.WordListDansk(2, 6);
            CrosswordFactory crosswordFactory = new CrosswordFactory();
            crosswordFactory.NewCrossBoard();
            for (int i = 0; i < crosswordFactory.CrosswordBoard.GetLength(0); i++)
            {
                for (int j = 0; j < crosswordFactory.CrosswordBoard.GetLength(1); j++)
                {
                    Console.Write(crosswordFactory.CrosswordBoard[i, j]+"|");
                }
                
                Console.WriteLine();
            }
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
