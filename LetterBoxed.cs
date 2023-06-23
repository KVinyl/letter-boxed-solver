using LetterBoxedSolver;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LetterBoxedSolver
{
    public class LetterBoxed
    {
        public LetterBoxed(string side0, string side1, string side2, string side3)
        {
            Square = new(side0, side1, side2, side3);
        }
        private WordFilter wordFilter = new();
        private Queue<string[]> permutationQueue = new();

        public string[] Result { get; private set; }
        public Square Square { get; }
        public WordDatabase WordDb { get; private set; }

        public void Run()
        {
            WordDb = new(Square);
            FilterWordDatabase();

            Result = Solve();
        }

        private void InitializePermutations()
        {
            if (permutationQueue.Count == 0)
            {
                List<string> possibleWords = WordDb.AllWords().OrderByDescending(x => x.Distinct().Count()).ToList();

                foreach (string word in possibleWords)
                {
                    string[] permutation = { word };
                    permutationQueue.Enqueue(permutation);
                }
            }
        }

        private bool IsWinningPermutation(string[] permutation)
        {
            string[] sides = Square.Sides;
            Square testSquare = new(sides[0], sides[1], sides[2], sides[3]);

            testSquare.Play(permutation);

            return testSquare.IsGameOver;
        }

        private List<string[]> ExtendPermutation(string[] permutation)
        {
            List<string[]> permutationsList = new();

            string lastWord = permutation[permutation.Length - 1];
            char lastChar = lastWord[lastWord.Length - 1];

            foreach (string word in WordDb[lastChar]) 
            {
                string[] extension = new string[] { word };
                string[] newPermutation = permutation.Concat(extension).ToArray();

                permutationsList.Add(newPermutation);
            }

            return permutationsList;
        }

        private string[] Solve()
        {
            InitializePermutations();

            while (true)
            {
                string[] permutation = permutationQueue.Dequeue();
                if (IsWinningPermutation(permutation))
                {
                    return permutation;
                }

                foreach (string[] newPermutation in ExtendPermutation(permutation))
                {
                    permutationQueue.Enqueue(newPermutation);
                }
            }
        }

        public string DisplayResults()
        {
            return (Result != null) ? string.Join(", ", Result) : "";
        }

        public void FilterWord(string wordToFilter)
        {
            wordFilter.AddWord(wordToFilter);
        }

        private void FilterWordDatabase()
        {
            foreach (string word in wordFilter.Words)
            {
                WordDb.RemoveWord(word);
            }
        }
    }
}
