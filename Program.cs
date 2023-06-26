namespace LetterBoxedSolver
{
    public class Program
    {
        private const int NumSides = 4;

        private static void Main(string[] args)
        {
            string[] sides = LetterParser();

            while (true)
            {
                LetterBoxed game = new(sides[0], sides[1], sides[2], sides[3]);

                Console.WriteLine("Solving...");
                Console.WriteLine();

                game.Solve();

                if (game.Result == null)
                {
                    Console.WriteLine("No result found.");
                    break;
                }

                string resultOutput = string.Join(", ", game.Result);
                Console.WriteLine(resultOutput);

                Console.WriteLine("Did that solve the puzzle? (Y/N)");
                string? gameOverInput = Console.ReadLine();

                Console.WriteLine();

                if (gameOverInput != null && gameOverInput.ToUpper()[0] == 'Y')
                {
                    Console.WriteLine("Game Over. Thanks for playing.");
                    break;
                }
                else
                {
                    PromptWordFilter(game);
                }
            }
        }

        private static string[] LetterParser()
        {
            string[] sides = new string[NumSides];

            for (int i = 0; i < NumSides; i++)
            {
                string side = "";
                while (!Square.IsValidSide(side))
                {
                    Console.WriteLine($"Enter the letters for side {i + 1}.");
                    string? lettersInput = Console.ReadLine();
                    if (lettersInput != null)
                    {
                        side = lettersInput;
                    }
                    
                    Console.WriteLine();
                }
                sides[i] = side.ToUpper();
            }

            return sides;
        }

        private static void PromptWordFilter(LetterBoxed game)
        {
            if (game.Result == null)
            {
                return;
            }

            Console.WriteLine("What word did not work?");

            for (int i = 0; i < game.Result.Length; i++)
            {
                Console.WriteLine($"({i + 1}) {game.Result[i]}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter corresponding number");

            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                int i = choice - 1;

                if (i >= 0 && i < game.Result.Length)
                {
                    string wordToFilter = game.Result[i];
                    game.FilterWord(wordToFilter);
                    Console.WriteLine($"{wordToFilter} has been added to the word filter.");
                }
                else
                {
                    Console.WriteLine("Not a valid number. No word has been added to word filter.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Not a valid answer. No word has been added to word filter.");
            }

            Console.WriteLine();
        }
    }
}
