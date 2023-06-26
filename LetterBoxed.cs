namespace LetterBoxedSolver
{
    public class LetterBoxed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LetterBoxed"/> class.
        /// </summary>
        /// <param name="side0"></param>
        /// <param name="side1"></param>
        /// <param name="side2"></param>
        /// <param name="side3"></param>
        public LetterBoxed(string side0, string side1, string side2, string side3)
        {
            Square = new(side0, side1, side2, side3);
            WordDb = new(Square);
            FilterWordDatabase();
            InitializePermutationQueue();
        }

        private WordFilter wordFilter = new();
        private Queue<string[]> permutationQueue = new();

        public string[]? Result { get; private set; } = null;
        public Square Square { get; }
        public WordDatabase WordDb { get; private set; }

        /// <summary>
        /// Set ups permutationQueue to queue of 1-word-length permutation.
        /// </summary>
        private void InitializePermutationQueue()
        {
            if (permutationQueue.Count == 0)
            {
                List<string> possibleWords = WordDb.AllWords().OrderByDescending(x => x.Distinct().Count()).ToList();

                foreach (string word in possibleWords)
                {
                    string[] permutation = { word };
                    permutationQueue.Enqueue(permutation);
                }
            }
        }

        /// <summary>
        /// Determines whether permutation solves LetterBoxed.
        /// </summary>
        /// <param name="permutation"></param>
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
        /// <param name="rootPermutation"></param>
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
            while (Result == null)
            {
                string[] permutation = permutationQueue.Dequeue();
                if (IsWinningPermutation(permutation))
                {
                    Result = permutation; 
                }

                else
                {
                    foreach (string[] newPermutation in ExtendPermutation(permutation))
                    {
                        permutationQueue.Enqueue(newPermutation);
                    }
                }     
            }

            return Result;
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
    }
}
