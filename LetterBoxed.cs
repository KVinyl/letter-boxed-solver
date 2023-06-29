namespace LetterBoxedSolver
{
    public class LetterBoxed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LetterBoxed"/> class.
        /// </summary>
        public LetterBoxed(string side0, string side1, string side2, string side3)
        {
            Square = new(side0, side1, side2, side3);
            Reset();
        }

        private WordFilter wordFilter = new();
        private Queue<string[]> permutationQueue = new();
        // A record of winning permutations in string form with a comma in between each word.
        private HashSet<string> solutions = new();

        public Square Square { get; }
        public WordDatabase WordDb { get; private set; } = new();

        /// <summary>
        /// Initalizes WordDb, filters words from WordDb, and initializes PermutationQueue.
        /// </summary>
        public void Reset()
        {
            WordDb = new(Square);
            FilterWordDatabase();
            InitializePermutationQueue();
        }

        /// <summary>
        /// Set ups permutationQueue to queue of 1-word-length permutation.
        /// </summary>
        private void InitializePermutationQueue()
        {
            permutationQueue.Clear();
            List<string> possibleWords = WordDb.AllWords().OrderByDescending(x => x.Distinct().Count()).ToList();

            foreach (string word in possibleWords)
            {
                string[] permutation = { word };
                permutationQueue.Enqueue(permutation);
            }
        }

        /// <summary>
        /// Determines whether permutation solves LetterBoxed.
        /// </summary>
        /// <returns>True if the permutation solves LetterBoxed, otherwise false.</returns>
        private bool IsWinningPermutation(string[] permutation)
        {
            string[] sides = Square.Sides;
            Square testSquare = new(sides[0], sides[1], sides[2], sides[3]);

            foreach (string word in permutation)
            {
                testSquare.Play(word);
            }

            return testSquare.IsGameOver;
        }

        /// <summary>
        /// Takes rootPermutation of n length and generate list of permutations of (n+1) length.
        /// Each generated permutation is the rootPermutation plus one valid additional word for
        /// Letterboxed.
        /// </summary>
        /// <returns>List of permutations generated.</returns>
        private List<string[]> ExtendPermutation(string[] rootPermutation)
        {
            List<string[]> permutationsList = new();

            string lastWord = rootPermutation[rootPermutation.Length - 1];
            char lastChar = lastWord[lastWord.Length - 1];

            foreach (string word in WordDb[lastChar])
            {
                string[] extension = new string[] { word };
                string[] newPermutation = rootPermutation.Concat(extension).ToArray();

                permutationsList.Add(newPermutation);
            }

            return permutationsList;
        }

        /// <summary>
        /// Finds a winning word permutation for LetterBoxed.
        /// </summary>
        /// <returns>Returns a winning word permutation for LetterBoxed.</returns>
        public string[] Solve()
        {
            while (true)
            {
                string[] permutation = permutationQueue.Dequeue();
                string permutationString = PermutationToString(permutation);

                if (IsWinningPermutation(permutation) && !solutions.Contains(permutationString))
                {
                    solutions.Add(permutationString);
                    return permutation;
                }

                foreach (string[] newPermutation in ExtendPermutation(permutation))
                {
                    permutationQueue.Enqueue(newPermutation);
                }
            }
        }

        /// <summary>
        /// Adds wordToFilter to wordFilter.
        /// </summary>
        /// <param name="wordToFilter"></param>
        public void FilterWord(string wordToFilter)
        {
            wordFilter.AddWord(wordToFilter);
        }

        /// <summary>
        /// Removes all words in wordFilter from WordDb.
        /// </summary>
        private void FilterWordDatabase()
        {
            foreach (string word in wordFilter.Words)
            {
                WordDb.RemoveWord(word);
            }
        }

        /// <summary>
        /// Converts a string array to a string with a comma in between each word.
        /// </summary>
        /// <returns>A string that represents a string array with a comma in between each word.</returns>
        public string PermutationToString(string[] permutation)
        {
            return string.Join(",", permutation);
        }
    }
}
