using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LetterBoxedSolver
{
    public class WordFilter
    {
        public WordFilter()
        {
            ExtractWords();
        }
      
        private readonly string filename = "../../../wordfilter.txt";
        public SortedSet<string> Words { get; private set; } = new();

        /// <summary>
        /// Reads and extracts words from file has a filtered word on each line.
        /// </summary>
        private void ExtractWords()
        {
            try
            {
                using (StreamReader sr = new(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        Words.Add(line.ToLower());
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Writes to file the filtered words with each word on each line.
        /// </summary>
        private void UpdateFile()
        {
            try
            {
                using (StreamWriter sw = new(filename))
                {
                    foreach (string word in Words)
                    {
                        sw.WriteLine(word);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Adds wordToAdd to WordFilter.
        /// </summary>
        /// <param name="wordToAdd"></param>
        public void AddWord(string wordToAdd)
        {
            Words.Add(wordToAdd);
            UpdateFile();
        }
    }
}
