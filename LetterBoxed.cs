using Letter_Boxed_Solver;
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

        private const int TurnLimit = 3;

        public string[] Result { get; private set; }
        public Square Square { get; }
        public WordDatabase WordDb { get; private set; }

        public void Run()
        {
            WordDb = new(Square);
           
            Result = GenerateSolution();
        }

        private string[] GenerateSolution()
        {
            List<string> possibleWords = WordDb.AllWords().ToList().OrderByDescending(x => x.Length).ToList();
          
            // Brute Force
            // Works only for two words
            foreach (string word0 in possibleWords)
            {
                foreach (string word1 in possibleWords)
                {
                    if (word0[word0.Length - 1] == word1[0])
                    {
                        string[] sides = Square.Sides;
                        Square testSquare = new(sides[0], sides[1], sides[2], sides[3]);

                        testSquare.Play(word0);
                        testSquare.Play(word1);

                        if (testSquare.IsGameOver)
                        {
                            return new string[] { word0, word1 };
                        }

                    }
                }
            }

            return null;
        }

        private string[][] GenerateWordPermutations()
        {
            List<string[]> resultList = new();
            string[] possibleWords = WordDb.AllWords();

            // TODO Make algorithm more efficient
            // and rely on TurnLimit for the length of word permutation
            // Only makes 3-word permutations for now
            foreach (string word0 in possibleWords)
            {
                char lastLetter0 = word0[word0.Length - 1];
                foreach (string word1 in WordDb[lastLetter0])
                {
                    char lastLetter1 = word1[word1.Length - 1];
                    foreach (string word2 in WordDb[lastLetter1])
                    {
                        string[] permutation = { word0, word1, word2 };
                        resultList.Add(permutation);
                    }
                }
            }
 
            return resultList.ToArray();
        }

        // TODO: Prevent duplicate permutations in Result

        public string DisplayResults()
        {
            return (Result != null) ? string.Join(", ", Result) : "";
        }
    }
}
