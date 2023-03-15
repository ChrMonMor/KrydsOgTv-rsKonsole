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
            this.Board[1, 1] = '@';
            if (this.SizeX-2 < startingLongWord.Length)
            {
                return;
            }
            int differnt = this.SizeX - startingLongWord.Length;
            if (startingLongWord.Length % 2 == 0)
            {
                differnt++;
            }
            int k = 0;
            for (int i = 0; i < this.SizeX; i++)
            {
                if (i > differnt/2 - 1  && i < this.SizeX - (differnt/2))
                {
                    this.Board[this.SizeX / 2, i] = startingLongWord[k];
                    k++;
                }
            }
            int startingPoint = startingLongWord.Length / 4;

            int newWordsLength = this.SizeX / 2;

            string topWord = WordListFactory.WordsWithLetterArray(WordListFactory.WordListDansk(newWordsLength, newWordsLength),startingLongWord[startingPoint]).First(x => x.First() == startingLongWord[startingPoint]);
            string bottomWord = WordListFactory.WordsWithLetterArray(WordListFactory.WordListDansk(newWordsLength, newWordsLength), startingLongWord[startingLongWord.Length - startingPoint - 1]).First(x => x.Last() == startingLongWord[startingLongWord.Length - startingPoint - 1]);

            FillIllegalSpace();

            AddWordVertically(topWord, this.SizeX / 2, differnt);

            AddWordVertically(bottomWord, this.SizeX / 2 - newWordsLength + 1, this.SizeY - differnt - 1);

            RotateArrayClockwise();
        }
        private void AddBlankAtCircularSymmetry(int x, int y)
        {
            this.Board[this.SizeX - x, this.SizeY - y] += '@'; 
        }
        private void FillIllegalSpace()
        {
            for (int i = 0; i < this.SizeX; i++)
            {
                if (this.Board[i, 2] == '@')
                {
                    this.Board[i, 1] = '@';
                    this.Board[i, 0] = '@';
                }
                if (this.Board[i, 1] == '@')
                {
                    this.Board[i, 0] = '@';
                }
                if (this.Board[i, this.SizeY - 3] == '@')
                {
                    this.Board[i, this.SizeY - 2] = '@';
                    this.Board[i, this.SizeY - 1] = '@';
                }
                if (this.Board[i, this.SizeY - 2] == '@')
                {
                    this.Board[i, this.SizeY - 1] = '@';
                }
            }

            for (int i = 0; i < this.SizeY; i++)
            {
                if (this.Board[1, i] == '@')
                {
                    this.Board[0, i] = '@';
                }
                if (this.Board[2, i] == '@')
                {
                    this.Board[1, i] = '@';
                    this.Board[0, i] = '@';
                }
                if (this.Board[this.SizeX - 2, i] == '@')
                {
                    this.Board[this.SizeX - 1, i] = '@';
                }
                if (this.Board[this.SizeX - 3, i] == '@')
                {
                    this.Board[this.SizeX - 2, i] = '@';
                    this.Board[this.SizeX - 1, i] = '@';
                }
            }
        }
        private void AddWordVertically(string word, int xStart, int yStart)
        {
            for (int i = 0; i < word.Length; i++)
            {
                this.Board[xStart + i, yStart] = word[i];
            }
        }
        private void AddWordHorizontally(string word, int xStart, int yStart)
        {
            for (int i = 0; i < word.Length; i++)
            {
                this.Board[xStart, yStart + i] = word[i];
            }
        }
        private void RotateArrayClockwise()
        {
            int width;
            int height;
            char[,] dst;

            width = this.Board.GetUpperBound(0) + 1;
            height = this.Board.GetUpperBound(1) + 1;
            dst = new char[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int newRow;
                    int newCol;

                    newRow = col;
                    newCol = height - (row + 1);

                    dst[newCol, newRow] = this.Board[col, row];
                }
            }

            this.Board = dst;
        }
    }
}
