using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrydsOgTværsKonsole
{
    public class CrosswordFactory
    {
        public string[] WordArray { get; set; }
        public char[,] CrosswordBoard { get; set; }
        public int MaxCount { get; set; }
        public int CurrentCount { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public CrosswordFactory() 
        {
            WordArray = WordListFactory.WordListDansk(3, 7);
            CrosswordBoard = new char[,] { };
            MaxCount = 20;
            CurrentCount = 0;
            Min = 3; Max = 7;
        }
        public CrosswordFactory(string[] wordArray, char[,] crosswordBoard, int maxCount, int currentCount, int min, int max)
        {
            WordArray = wordArray;
            CrosswordBoard = crosswordBoard;
            MaxCount = maxCount;
            CurrentCount = currentCount;
            Min= min; Max = max;
        }
        public CrosswordFactory(int min, int max) 
        {
            WordArray = WordListFactory.WordListDansk(min, max);
            CrosswordBoard = new char[,] { };
            MaxCount = (min+1)*max;
            CurrentCount = 0;
            Min = min; Max = max;
        }

        public void NewCrossBoard()
        {

            int randomWordIndex = RNG.Roll(0,this.WordArray.Length);

            string startingWord = this.WordArray[randomWordIndex];

            //delete this whene done
            Console.WriteLine(startingWord);

            int wordDifferents = this.Max - startingWord.Length;

            this.CrosswordBoard = new char[(wordDifferents * 2)+startingWord.Length, (this.Max * 2)-1];
            char[,] newBoard = this.CrosswordBoard;

            for (int i = 0; i < startingWord.Length; i++)
            {
                newBoard[i+ wordDifferents, this.Max-1] = startingWord[i];
            }
            for (int i = 0; i < this.Max; i++)
            {
                FindNextWord(newBoard, startingWord, i, wordDifferents);
            }
        }
        private void FindNextWord(char[,] newBoard, string startingWord, int currentIndex, int wordDifferents)
        {
            if (currentIndex >= startingWord.Length)
            {
                return;
            }
            if (this.CurrentCount >= this.MaxCount)
            {
                return;
            }
            char letter = startingWord[currentIndex];
            string[] wordsWithLetter = WordListFactory.WordsWithLetterArray(this.WordArray, startingWord[currentIndex]);
            for (int i = 0; i < wordsWithLetter.Length; i++)
            {
                char[,] oldBoard = newBoard;

                int[] letterIndexes = Letterindexes(wordsWithLetter[i], letter);

                if (letterIndexes.Count() > 1)
                {

                }
                else
                {
                    oldBoard = InsertWordIntoBoardHorizontal(newBoard, wordsWithLetter[i], letterIndexes[0], currentIndex, wordDifferents);
                }

                if (FoundFlawInBoard(oldBoard)) continue;
                else
                {
                    this.CrosswordBoard = oldBoard;
                    this.CurrentCount++;
                    for (int k = 0; k < this.CrosswordBoard.GetLength(0); k++)
                    {
                        for (int j = 0; j < this.CrosswordBoard.GetLength(1); j++)
                        {
                            Console.Write(this.CrosswordBoard[k, j] + "|");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("__________________________");
                    return;
                }
            }

        }
        private int[] Letterindexes(string word, char targetLetter)
        {
            int[] letterIndex = new int[] { };
            for (int j = 0; j < word.Length; j++)
            {
                if (word[j] == targetLetter)
                {
                    letterIndex = letterIndex.Append(j).ToArray();
                }
            }
            return letterIndex;
        }
        private char[,] InsertWordIntoBoardHorizontal(char[,] board, string word, int letterIndex, int currentIndex, int wordDifferents)
        {
            int startingPoint = (this.Max - 1) - letterIndex;
            char[,] extraBoard = board; 
            for (int i = 0; i < word.Length; i++)
            {
                extraBoard[currentIndex + wordDifferents, i + startingPoint] = word[i];
            }
            if (FoundFlawInBoard(board)) return board;
            return extraBoard;
        }
        private bool FoundFlawInBoard(char[,] newBoard)
        {
            bool foundFlaw = false;

            string[] words = new string[] { };

            for (int i = 0; i < newBoard.GetLength(0) ; i++)
            {
                string word = "";
                for (int j = 0; j < newBoard.GetLength(1); j++)
                {
                    if (newBoard[i, j] == '\0') continue;
                    word += newBoard[i, j]; 
                }

                if (String.IsNullOrWhiteSpace(word))  continue;
                if (word.Length < this.Min || word.Length > this.Max) continue;
                if (word == "\0") continue;

                words = words.Append(word).ToArray();
            }

            for (int i = 0; i < newBoard.GetLength(1); i++)
            {
                string word = "";
                for (int j = 0; j < newBoard.GetLength(0); j++)
                {
                    if (newBoard[j, i] == '\0') continue;
                    word += newBoard[j, i];
                }

                if (String.IsNullOrWhiteSpace(word)) continue;
                if (word.Length < this.Min || word.Length > this.Max) continue;
                if (word == "\0") continue;

                words = words.Append(word).ToArray();
            }

            if (!words.All(x => this.WordArray.Contains(x)))
            {
                foundFlaw = true;
            }

            return foundFlaw;
        }

    }
}
