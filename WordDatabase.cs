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
        private readonly string filename = "../../../words.txt";

        /// <summary>
        /// Initializes a new instance of the <see cref="WordDatabase"/> class.
        /// </summary>
        public WordDatabase()
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            InitializeDatabase(alphabet.ToArray());
            LoadDatabase();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WordDatabase"/> class with 
        /// a Square parameter.
        /// </summary>
        public WordDatabase(Square sq)
        {
            Square = sq;
            InitializeDatabase(Square.Letters);
            LoadDatabase();
        }

        public Square? Square { get; } = null;

        /// <summary>
        /// Gets string array of words with a starting letter of <param name="ch">ch</param>.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns>Returns string array of words with a starting letter of <param name="ch">ch</param>.
        /// Otherwise, returns empty string array.</returns>
        public string[] this[char ch]
        {
            get
            {
                try
                {
                    return wordDatabase[char.ToUpper(ch)].ToArray();
                }
                catch (KeyNotFoundException)
                {
                    return Array.Empty<string>();
                }
            }
        }

        /// <summary>
        /// Gets total count of all words in WordDatabase object.
        /// </summary>
        public int WordCount { get { return wordDatabase.Sum(x => x.Value.Count); } }

        /// <summary>
        /// Initializes WordDatabase object with each key being an element in <param name="letters">letters</param> with
        /// a value of an empty SortedSet.
        /// </summary>
        /// <param name="letters"></param>
        private void InitializeDatabase(char[] letters)
        {
            foreach (char letter in letters)
            {
                wordDatabase[char.ToUpper(letter)] = new SortedSet<string>();
            }
        }

        /// <summary>
        /// Reads words from file and loads them to WordDatabase object.
        /// </summary>
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

        /// <summary>
        /// Adds word to WordDatabase.
        /// </summary>
        /// <param name="word"></param>
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

        /// <summary>
        /// Removes word from WordDatabase if word is in WordDatabase.
        /// </summary>
        /// <param name="word"></param>
        /// <returns>Returns true if word was in WordDatabase is now removed. Otherwise, returns false.</returns>
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

        /// <summary>
        /// Gets all words in WordDatabase.
        /// </summary>
        /// <returns>Returns a string array of all words in WordDatabase.</returns>
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
