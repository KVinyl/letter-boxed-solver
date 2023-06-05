using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter_Boxed_Solver
{
    public class Side
    {
        public Side(char letter0, char letter1, char letter2)
        {
            Letters = new char[] { letter0, letter1, letter2 };
            unusedLetters = Letters.ToHashSet();
        }
        public char[] Letters { get; }
       
        private HashSet<char> usedLetters = new HashSet<char>();
        private HashSet<char> unusedLetters;

        public void Play(string word)
        {
            foreach (char letter in UnusedLetters())
            {
                if (word.Contains(letter))
                {
                    usedLetters.Add(letter);
                    unusedLetters.Remove(letter);
                }
            }
        }
        public char[] UsedLetters()
        {
            return usedLetters.ToArray();
        }

        public char[] UnusedLetters()
        {
            return unusedLetters.ToArray();
        }

        public bool IsUsed()
        {
            return unusedLetters.Count() == 0;
        }
    }
}
