using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Letter_Boxed_Solver
{
    public class WordDatabase
    {
        private Dictionary<char, HashSet<string>> wordDatabase = new Dictionary<char, HashSet<string>>();

        /// words.txt is a file generated from the following URL:
        // https://github.com/dwyl/english-words/blob/master/words_alpha.txt
        string filename = "../../../words.txt";

        public WordDatabase()
        {
            Initialize();
        }

        private void Initialize()
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach (char letter in alphabet)
            {
                wordDatabase[letter] = new HashSet<string>();
            }

            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        string word = sr.ReadLine();
                        char firstLetter = word.ToUpper()[0];
                        wordDatabase[firstLetter].Add(word);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
