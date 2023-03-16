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
            string middleWord = WordListFactory.WordsWithLetterArray(WordListFactory.WordListDansk(newWordsLength,newWordsLength), startingLongWord[startingLongWord.Length/2]).First(x => x[newWordsLength/2] == startingLongWord[startingLongWord.Length/2]);

            AddWordVertically('@'+topWord, this.SizeX / 2 - 1, differnt);

            AddWordVertically('@' + middleWord + '@', this.SizeX / 4, this.SizeY / 2);

            AddWordVertically(bottomWord+'@', this.SizeX / 2 - newWordsLength + 1, this.SizeY - differnt - 1);

            AnalyseOpptions();

            FillIllegalSpace();

            int tk = 0;

        }
        private void AddBlankAtCircularSymmetry(int x, int y)
        {
            this.Board[this.SizeX - x - 1, this.SizeY - y - 1] = '@';
        }
        private void FillIllegalSpace()
        {
            for (int i = 0; i < this.SizeX; i++)
            {
                if (this.Board[i, 2] == '@' || Char.IsLetter(this.Board[i, 2]))
                {
                    this.Board[i, 1] = '@';
                    this.Board[i, 0] = '@';
                }
                if (this.Board[i, 1] == '@' || Char.IsLetter(this.Board[i, 1]))
                {
                    this.Board[i, 0] = '@';
                }
                if (this.Board[i, this.SizeY - 3] == '@' || Char.IsLetter(this.Board[this.SizeY - 3, 2]))
                {
                    this.Board[i, this.SizeY - 2] = '@';
                    this.Board[i, this.SizeY - 1] = '@';
                }
                if (this.Board[i, this.SizeY - 2] == '@' || Char.IsLetter(this.Board[this.SizeY - 2, 2]))
                {
                    this.Board[i, this.SizeY - 1] = '@';
                }
            }

            for (int i = 0; i < this.SizeY; i++)
            {
                if (this.Board[1, i] == '@' || Char.IsLetter(this.Board[1, i]))
                {
                    this.Board[0, i] = '@';
                }
                if (this.Board[2, i] == '@' || Char.IsLetter(this.Board[2, i]))
                {
                    this.Board[1, i] = '@';
                    this.Board[0, i] = '@';
                }
                if (this.Board[this.SizeX - 2, i] == '@' || Char.IsLetter(this.Board[this.SizeX - 2, i]))
                {
                    this.Board[this.SizeX - 1, i] = '@';
                }
                if (this.Board[this.SizeX - 3, i] == '@' || Char.IsLetter(this.Board[this.SizeX - 3, i]))
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
        private void RotateArrayCounterClockwise()
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

                    newRow = width - (col + 1);
                    newCol = row;

                    dst[newCol, newRow] = this.Board[col, row];
                }
            }

            this.Board = dst;
        }

        private void AnalyseOpptions()
        {
            string[] allWords = WordListFactory.WordListDansk(0, 20);
            string[] ourWords = new string[] { }; 
            for (int i = 0; i < this.SizeX; i++)
            {
                string sH = "";
                string sV = "";
                for (int j = 0; j < this.SizeY; j++)
                {
                    if (this.Board[i, j] == '\0')
                    {
                        sH += '_';
                    }
                    else 
                    { 
                        sH += this.Board[i, j];
                    }
                    if (this.Board[j, i] == '\0')
                    {
                        sV += '_';
                    }
                    else
                    {
                        sV += this.Board[j, i];
                    }
                }
                if (sH.Contains('@'))
                {
                    foreach (string item in sH.Split('@'))
                    {
                        ourWords = ourWords.Append(item).ToArray();
                    }
                }
                else
                {
                    ourWords = ourWords.Append(sH).ToArray();
                }
                if (sV.Contains('@'))
                {
                    foreach (string item in sV.Split('@'))
                    {
                        ourWords = ourWords.Append(item).ToArray();
                    }
                }
                else
                {
                    ourWords = ourWords.Append(sV).ToArray();
                }
            }
            ourWords = ourWords.Where(x => x.Count(char.IsLetter) > 1).ToArray();
            ourWords = ourWords.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            int tk = 0;
        }
    }
}
