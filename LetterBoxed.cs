using Letter_Boxed_Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LetterBoxedSolver
{
    public class LetterBoxed
    {
        private const int NumLettersPerSide = 3;
        private const int NumSides = 4;
        private const int TurnLimit = 3;
        public void Run()
        {
            WordDatabase wordDb = new WordDatabase();
            Console.WriteLine(wordDb);

            string[] sides = LetterParser();
            Square square = new Square(sides[0], sides[1], sides[2], sides[3]);
        }

        private string[] LetterParser()
        {
            string[] sides = new string[NumSides];

            for (int i = 0; i < NumSides; i++)
            {
                string side = "";
                while (!IsValidSide(side))
                {
                    Console.WriteLine($"Enter the letters for side {i + 1}.");
                    side = Console.ReadLine();
                    Console.WriteLine();
                }

                sides[i] = side.ToUpper();
            }
            return sides;
        }

        private bool IsValidSide(string letters)
        {
            return !string.IsNullOrEmpty(letters) && letters.Length == NumLettersPerSide && letters.All(Char.IsLetter);
        }
    }
}
