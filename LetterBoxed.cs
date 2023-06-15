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
        private const int NumSides = 4;
        private const int TurnLimit = 3;
        private List<string[]> result = new();

        public string[][] Result { get { return result.ToArray(); } }

        public void Run()
        {
            string[] sides = LetterParser();
            Square square = new(sides[0], sides[1], sides[2], sides[3]);

            WordDatabase wordDb = new(square); 
        }

        private string[] LetterParser()
        {
            string[] sides = new string[NumSides];

            for (int i = 0; i < NumSides; i++)
            {
                string side = "";
                while (!Square.IsValidSide(side))
                {
                    Console.WriteLine($"Enter the letters for side {i + 1}.");
                    side = Console.ReadLine();
                    Console.WriteLine();
                }
                sides[i] = side.ToUpper();
            }
            return sides;
        }

        private string[][] GeneratePossiblePermutations()
        {
            List<string[]> resultList = new();

            return resultList.ToArray();
        }
    }
}
