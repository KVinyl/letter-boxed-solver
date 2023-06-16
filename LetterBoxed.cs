using Letter_Boxed_Solver;
using System;
using System.Collections.Generic;
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
        private List<string[]> result = new();

        public string[][] Result { get { return result.ToArray(); } }
        public Square Square { get; }
        public WordDatabase WordDb { get; private set; }

        public void Run()
        {
            // WordDb = new(Square);
            WordDb = new();
            WordDb.AddWord("mantis");
            WordDb.AddWord("power");
            WordDb.AddWord("superpower");
            WordDb.AddWord("rew");

            string[][] wordPermutations = GenerateWordPermutations();
            foreach (string[] permutation in wordPermutations)
            {
                string[] sides = Square.Sides;
                Square testSquare = new(sides[0], sides[1], sides[2], sides[3]);

                for (int i = 0; i < permutation.Length; i++)
                {
                    string word = permutation[i];
                    testSquare.Play(word);

                    if (testSquare.IsGameOver)
                    {
                        if (i < TurnLimit - 1)
                        {
                            result.Add(permutation.ToList().GetRange(0, i + 1).ToArray());
                        }
                        else
                        {
                            result.Add(permutation);
                        }
                        continue;
                    }
                }
            }
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

        public string DisplayResults()
        {
            string displayResult = "";
            foreach (string[] words in Result)
            {
                Console.WriteLine(words);
                displayResult += string.Join(", ", words) + "\n";
            }

            return displayResult;
        }
    }
}
