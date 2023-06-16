using Letter_Boxed_Solver;
using LetterBoxedSolver;

public class Program
{
    private const int NumSides = 4;

    private static void Main(string[] args)
    {
        string[] sides = LetterParser();

        LetterBoxed game = new(sides[0], sides[1], sides[2], sides[3]);
        game.Run();

        Console.WriteLine(game.DisplayResults);
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