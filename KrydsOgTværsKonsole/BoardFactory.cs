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

            AddWordVertically(topWord + '@', this.SizeX / 2, differnt);

            AddWordVertically('@' + middleWord + '@', this.SizeX / 4, this.SizeY / 2);

            AddWordVertically('@' + bottomWord, this.SizeX / 2 - newWordsLength, this.SizeY - differnt - 1);

            AnalyseOpptions();

            FillIllegalSpace();

        }
        private void AddBlankAtCircularSymmetry(int x, int y)
        {
            this.Board[this.SizeX - x - 1, this.SizeY - y - 1] = '@';
        }
        private void FillIllegalSpace()
        {
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
                ourWords = ourWords.Append(sH).ToArray();
                ourWords = ourWords.Append(sV).ToArray();
            }
            int[] targets = new int[] { };
            for (int i = 0; i < ourWords.Count(); i++)
            {
                for (int j = 0; j < ourWords[i].Length; j++)
                {
                    if (!char.IsLetter(ourWords[i][j]))
                    {
                        continue;
                    }
                    if (ourWords[i].Contains(ourWords[i][j]+"__"))
                    {
                        if (j + 3 < ourWords[i].Length)
                        {
                            if (ourWords[i].Contains(ourWords[i][j] + "__" + ourWords[i][j + 3]))
                            {
                                continue;
                            }
                            targets = targets.Append(i).ToArray();
                        }
                    }
                    if (ourWords[i].Contains("__" + ourWords[i][j]))
                    {
                        if (j - 3 > 0)
                        {
                            if (ourWords[i].Contains(ourWords[i][j - 3] + "__" + ourWords[i][j]))
                            {
                                continue;
                            }
                            
                        }
                        targets = targets.Append(i).ToArray();
                    }
                }
            }
            foreach (int index in targets)
            {
                // %2 is horizontal lines
                if (index % 2 == 0)
                {
                    for (int i = 0; i < this.SizeX - 1; i++)
                    {
                        if (this.Board[index/2, i] == '\0')
                        {
                            this.Board[index / 2, i] = '@';
                            AddBlankAtCircularSymmetry(index / 2, i);
                            this.Board[index / 2, i+1] = '@';
                            AddBlankAtCircularSymmetry(index / 2, i+1);
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < this.SizeX - 1; i++)
                    {
                        if (this.Board[i, index / 2] == '_')
                        {
                            this.Board[i, index / 2] = '@';
                            AddBlankAtCircularSymmetry(i, index / 2);
                            this.Board[i + 1, index / 2] = '@';
                            AddBlankAtCircularSymmetry(i + 1, index / 2);
                            break;
                        }
                    }
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
