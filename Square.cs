using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter_Boxed_Solver
{
    public class Square
    {
        public Square(Side side0, Side side1, Side side2, Side side3)
        {
            Sides = new Side[] { side0, side1, side2, side3 };

            for (int i = 0; i < Sides.Length; i++)
            {
                foreach (char letter in Sides[i].Letters)
                {
                    letterSideDict[letter] = i;
                }
            }

            Letters = letterSideDict.Keys.ToHashSet();
        }

        public Side[] Sides { get; }
        public HashSet<char> Letters { get; } 
        private Dictionary<char, int> letterSideDict = new Dictionary<char, int>();

        public void Play(string word)
        {
            foreach (Side side in Sides)
            {
                side.Play(word);
            }
        }
        
        public bool IsValidWord(string word)
        {
            HashSet<char> wordLetters = new HashSet<char>(word);
            return wordLetters.IsSubsetOf(Letters);
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
            return Sides.All(side => side.IsUsed());
        }
    }
}
