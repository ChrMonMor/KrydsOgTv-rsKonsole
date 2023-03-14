using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrydsOgTværsKonsole
{
    public class BoardFactory
    {
        /// <summary>
        /// 
        /// '@' = blocked space. Can not add letter inside of
        /// 
        /// </summary>


        public int BoardId { get; set; }
        public char[,] Board { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public BoardFactory() { }
        public BoardFactory(int boardId, int size)
        {
            BoardId = boardId;
            SizeX = size;
            SizeY = size;
            Board = new char[size, size]; 
        }
        public void StartBoardGeneration(string startingLongWord)
        {
            if (this.SizeX-2 < startingLongWord.Length)
            {
                return;
            }
            int differnt = this.SizeX - startingLongWord.Length;
            int k = 0;
            for (int i = 0; i < this.SizeX; i++)
            {
                if (i > differnt/2 || i < this.SizeX - differnt/2)
                {
                    this.Board[(this.SizeX - 1) / 2, i] = '@';
                }
                else
                {
                    this.Board[(this.SizeX - 1) / 2, i] = startingLongWord[k];
                    k++;
                }
            }
        }
        private void AddBlankAtCircularSymmetry(int x, int y)
        {
            this.Board[this.SizeX - x, this.SizeY - y] += '@'; 
        }
    }
}
