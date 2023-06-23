using LetterBoxedSolver;

public class Program
{
    private const int NumSides = 4;

    private static void Main(string[] args)
    {
        string[] sides = LetterParser();

        bool gameOver = false;
        while (!gameOver)
        {
            LetterBoxed game = new(sides[0], sides[1], sides[2], sides[3]);
            game.Run();

            Console.WriteLine(game.DisplayResults());

            Console.WriteLine("Did that solve the puzzle? (Y/N)");
            string gameOverReply = Console.ReadLine();

            Console.WriteLine();

            if (!string.IsNullOrEmpty(gameOverReply) && gameOverReply.ToUpper()[0] == 'Y')
            {
                Console.WriteLine("Game Over. Thanks for playing.");
                gameOver = true;
            }
            else
            {
                Console.WriteLine("What word did not work?");
                string wordToFilter = Console.ReadLine().ToLower();

                if (game.Result.Contains(wordToFilter))
                {
                    game.FilterWord(wordToFilter);
                    Console.WriteLine($"{wordToFilter} has been added to the word filter.");
                } 
                else
                {
                    Console.WriteLine($"{wordToFilter} was not found in the result.");
                }
                Console.WriteLine();
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
                side = Console.ReadLine();
                Console.WriteLine();
            }
            sides[i] = side.ToUpper();
        }

        return sides;
    }
}