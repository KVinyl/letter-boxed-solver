﻿using Letter_Boxed_Solver;
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

        // TODO: Prevent duplicate permutations in Result

        public string DisplayResults()
        {
            return (Result != null) ? string.Join(", ", Result) : "";
        }
    }
}
