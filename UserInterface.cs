namespace LetterBoxedSolver
{
    public class UserInterface
    {
        private const int NumSides = 4;
        public void Run()
        {
            string[] sides = LetterParser();
            LetterBoxed game = new(sides[0], sides[1], sides[2], sides[3]);

            while (true)
            {
                Console.WriteLine("Solving...");
                Console.WriteLine();

                string[] result = game.Solve();

                string resultOutput = string.Join(", ", result);
                Console.WriteLine(resultOutput);

                Console.WriteLine("Did that solve the puzzle? (Y/N)");
                string? isSolvedInput = Console.ReadLine();

                Console.WriteLine();

                if (IsYes(isSolvedInput))
                {
                    Console.WriteLine("Do you want another solution? (Y/N)");
                    string? continueInput = Console.ReadLine();

                    if (!IsYes(continueInput))
                    {
                        Console.WriteLine("Game Over. Thanks for playing.");
                        break;
                    }
                }
                else
                {
                    string? wordToFilter = PromptWordFilter(result);

                    if (wordToFilter != null)
                    {
                        game.FilterWord(wordToFilter);
                        Console.WriteLine($"{wordToFilter} has been added to the word filter.");
                    }
                    else
                    {
                        Console.WriteLine("No word has been added to the word filter.");
                    }

                    Console.WriteLine();
                    game.Reset();
                }
            }
        }

        private bool IsYes(string? str)
        {
            return !string.IsNullOrEmpty(str) && str.ToUpper()[0] == 'Y';
        }

        private string[] LetterParser()
        {
            string[] sides = new string[NumSides];

            for (int i = 0; i < NumSides; i++)
            {
                string side = "";
                while (true)
                {
                    Console.WriteLine($"Enter the letters for side {i + 1}.");
                    string? lettersInput = Console.ReadLine();
                    if (lettersInput != null)
                    {
                        side = lettersInput;
                    }

                    Console.WriteLine();

                    if (Square.IsValidSide(side))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Input is not valid. Please try again.");
                    }
                }
                sides[i] = side.ToUpper();
            }

            return sides;
        }

        private string? PromptWordFilter(string[] words)
        {
            while (true)
            {
                Console.WriteLine("What word did not work?");
                for (int i = 0; i < words.Length; i++)
                {
                    Console.WriteLine($"({i + 1}) {words[i]}");
                }

                Console.WriteLine();
                Console.WriteLine("Enter corresponding number. Enter 0 to cancel.");

                try
                {
                    int numInput = Convert.ToInt32(Console.ReadLine());

                    if (numInput == 0)
                    {
                        return null;
                    }
                    else if (numInput > 0 && numInput <= words.Length)
                    {
                        string wordToFilter = words[numInput - 1];
                        return wordToFilter;
                    }
                    else
                    {
                        Console.WriteLine("Not a valid number. Try again.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Not a valid input. Try again.");
                }

                Console.WriteLine();
            }
        }
    }
}
