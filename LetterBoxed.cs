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
            WordDb = new(Square);    
        }

   
        private string[][] GeneratePossiblePermutations()
        {
            List<string[]> resultList = new();
            // TODO

            return resultList.ToArray();
        }
    }
}
