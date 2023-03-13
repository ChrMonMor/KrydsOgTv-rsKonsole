using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KrydsOgTværsKonsole
{
    public class WordListFactory
    {
        private static readonly string path = "./../../2012dkk.txt";
        private static string[] RemoveFlawsFromArray(string[] readText, bool isDistinct = false)
        {
            Regex r = new Regex("[0-9]$");
            Array.Sort(readText);
            for (int i = 0; i < readText.Length; i++)
            {
                readText[i] = readText[i].ToLower();
                int j = readText[i].IndexOf(';');
                if (readText[i][j - 1] == '.')
                {
                    j--;
                }
                readText[i] = readText[i].Remove(j, readText[i].Length - j);
                readText[i] = readText[i].Replace("-", "");
                if (readText[i].Any(char.IsDigit))
                {
                    readText[i] = readText[i].Remove(0, 3);
                }
                if (!readText[i].All(char.IsLetter))
                {
                    readText[i] = " ";
                }
            }
            //if you want all words to be unik
            if (isDistinct)
            {
                readText = readText.Distinct().ToArray();
            }
            return readText = readText.Where(x => !x.Contains(" ")).ToArray();
        }
        public static string[] WordListDansk(int min, int max)
        {
            string[] readText = File.ReadAllLines(path);
            readText = RemoveFlawsFromArray(readText);
            if(min < max)
            {
                readText = readText.Where(x => x.Length > min - 1 && x.Length < max + 1).ToArray();
            }
            else
            {
                readText = readText.Where(x => x.Length > max - 1 && x.Length < min + 1).ToArray();
            }
            return readText;
        }
        public static string[] WordsWithLetterArray(string[] wordsArray, char letter)
        {
            if (wordsArray.Any(x => String.IsNullOrWhiteSpace(x)))
            {
                return wordsArray;
            }
            if (!char.IsLetter(letter))
            {
                return wordsArray;
            }
            
            wordsArray = wordsArray.OrderBy(x => RNG.NewRandom()).ToArray();
            return wordsArray.Where(x => x.Contains(letter)).ToArray();
        }
    }
}
