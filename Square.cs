using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterBoxedSolver
{
    public class Square
    {
        public Square(string side0, string side1, string side2, string side3)
        {
            Sides = new string[] { side0, side1, side2, side3 };

            for (int i = 0; i < Sides.Length; i++)
            {
                foreach (char letter in Sides[i])
                {
                    letterSideDict[char.ToUpper(letter)] = i;
                }
            }

            Letters = letterSideDict.Keys.ToArray();
            unplayedLetters = new HashSet<char>(letterSideDict.Keys);
        }

        private const int NumLettersPerSide = 3;
        private HashSet<char> playedLetters = new();
        private HashSet<char> unplayedLetters = new();
        private Dictionary<char, int> letterSideDict = new();

        public string[] Sides { get; }
        public char[] Letters { get; }
        public char[] PlayedLetters { get { return playedLetters.ToArray(); } }
        public char[] UnplayedLetters { get { return unplayedLetters.ToArray(); } }
        public char LastLetter { get; private set; }
        public bool IsGameOver { get { return unplayedLetters.Count == 0; } }

        public void Play(string word)
        {
            if (IsValidWord(word))
            {
                HashSet<char> playingLetters = new(word.ToUpper());
                unplayedLetters.ExceptWith(playingLetters);
                playedLetters.UnionWith(playingLetters);
                LastLetter = char.ToUpper(word[word.Length-1]);
            }
        }

        public void Play(string[] words)
        {
            string combinedWord = string.Join("", words);
            Play(combinedWord);
        }

        public static bool IsValidSide(string letters)
        {
            return !string.IsNullOrEmpty(letters) && letters.Length == NumLettersPerSide && letters.All(char.IsLetter);
        }

        public bool IsValidWord(string word)
        {
            word = word.ToUpper();

            int minValidWordLength = 3;
            if (word.Length < minValidWordLength)
            {
                return false;
            }

            char firstLetter = word[0];
            if (LastLetter != '\0' &&  firstLetter != LastLetter)
            {
                return false;
            }

            int lastIndex = -1;
            foreach (char letter in word)
            {
                int index = IndexOf(letter);
                if (index == -1 || index == lastIndex)
                {
                    return false;
                }
                lastIndex = index;
            }
            return true;
        }
        public int IndexOf(char letter)
        {
            char upperLetter = char.ToUpper(letter);
            if (!letterSideDict.ContainsKey(upperLetter))
            {
                return -1;
            }
            return letterSideDict[upperLetter];
        }
    }
}
