using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KrydsOgTværsKonsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            testBoardFactory();
        }
        public static void testCrossword()
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
        public static void testBoardFactory()
        {
            BoardFactory boardFactory = new BoardFactory(1,15);
            boardFactory.StartBoardGeneration(WordListFactory.WordListDansk(11, 11).ElementAt(RNG.Roll(0, WordListFactory.WordListDansk(11, 11).Length)));

            for (int i = 0; i < boardFactory.Board.GetLength(0); i++)
            {
                for (int j = 0; j < boardFactory.Board.GetLength(1); j++)
                {
                    Console.Write(boardFactory.Board[i, j] + "|");
                }

                Console.WriteLine();
            }
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
