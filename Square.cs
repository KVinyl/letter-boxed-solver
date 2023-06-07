﻿using System;
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
            string[] sides = { side0, side1, side2, side3 };

            for (int i = 0; i < sides.Length; i++)
            {
                foreach (char letter in sides[i])
                {
                    letterSideDict[letter] = i;
                }
            }
            Letters = letterSideDict.Keys.ToArray();
            unplayedLetters = new HashSet<char>(letterSideDict.Keys);
        }

        private HashSet<char> playedLetters = new HashSet<char>();
        private HashSet<char> unplayedLetters = new HashSet<char>();
        private Dictionary<char, int> letterSideDict = new Dictionary<char, int>();
        public char[] Letters { get; }
        public char[] PlayedLetters { get { return playedLetters.ToArray(); } }
        public char[] UnplayedLetters { get { return unplayedLetters.ToArray(); } }


        public void Play(string word)
        {
            if (IsValidWord(word))
            {
                HashSet<char> playingLetters = new HashSet<char>(word);
                unplayedLetters.Except(playingLetters);
                playedLetters.Union(playingLetters);
            }
        }

        public bool IsValidWord(string word)
        {
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
            if (!letterSideDict.ContainsKey(letter))
            {
                return -1;
            }
            return letterSideDict[letter];
        }

        public bool IsUsed()
        {
            return unplayedLetters.Count == 0;
        }
    }
}
