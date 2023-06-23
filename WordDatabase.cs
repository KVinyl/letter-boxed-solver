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

namespace LetterBoxedSolver
{
    public class WordDatabase
    {
        private Dictionary<char, SortedSet<string>> wordDatabase = new();

        /// words.txt is a file generated from the following URL:
        // https://github.com/dwyl/english-words/blob/master/words_alpha.txt
        private string filename = "../../../words.txt";

        public WordDatabase()
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            InitializeDatabase(alphabet.ToArray());
            LoadDatabase();
        }

        public WordDatabase(Square sq)
        {
            Square = sq;
            InitializeDatabase(Square.Letters);
            LoadDatabase();
        }

        public Square? Square { get; } = null;

        public string[] this[char ch]
        {
            get
            {
                return wordDatabase[char.ToUpper(ch)].ToArray();
            }
        }
        public int WordCount { get { return wordDatabase.Sum(x => x.Value.Count); } }

        private void InitializeDatabase(char[] letters)
        {
            foreach (char letter in letters)
            {
                wordDatabase[char.ToUpper(letter)] = new SortedSet<string>();
            }
        }

        private void LoadDatabase()
        {
            try
            {
                using (StreamReader sr = new(filename))
                {
                    if (Square == null)
                    {
                        while (!sr.EndOfStream)
                        {
                            string word = sr.ReadLine();
                            AddWord(word);
                        }
                    }
                    else
                    {
                        while (!sr.EndOfStream)
                        {
                            string word = sr.ReadLine();
                            if (Square.IsValidWord(word))
                            {
                                AddWord(word);
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddWord(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                char firstLetter = word.ToUpper()[0];
                if (wordDatabase.TryGetValue(firstLetter, out SortedSet<string> value))
                {
                    value.Add(word);
                }
            }
        }

        public bool RemoveWord(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                char firstLetter = word.ToUpper()[0];
                if (wordDatabase.TryGetValue(firstLetter, out SortedSet<string> value))
                {
                    return value.Remove(word);
                }
            }

            return false;
        }

        public string[] AllWords()
        {
            List<string> wordList = new();

            foreach (SortedSet<string> wordSet in wordDatabase.Values)
            {
                wordList.AddRange(wordSet);
            }

            wordList.Sort();
            return wordList.ToArray();
        }
    }
}
