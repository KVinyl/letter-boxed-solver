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
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            InitializeDatabase(alphabet.ToArray());
            LoadDatabase();
        }
        public WordDatabase(char[] letters)
        {
            InitializeDatabase(letters);
            LoadDatabase();
            Console.WriteLine(WordCount);
        }

        public int WordCount { get { return wordDatabase.Sum(x => x.Value.Count); } }
        public const int WordLengthMin = 3;
        private void InitializeDatabase(char[] letters)
        {
            foreach (char letter in letters)
            {
                wordDatabase[letter] = new HashSet<string>();
            }
        }

        private void LoadDatabase()
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        string word = sr.ReadLine();
                        char firstLetter = word.ToUpper()[0];
                        if (wordDatabase.ContainsKey(firstLetter) && word.Length >= WordLengthMin)
                        {
                            wordDatabase[firstLetter].Add(word);
                        }
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

