using Utility;
using Utility.Exceptions;

if (args.Length > 0 && args[0] == "all")
{
    ExecuteAll();
}
else
{
    ExecuteChallenge<Challenges.Day10.Part1>();
    ExecuteChallenge<Challenges.Day10.Part2>();
}

static void ExecuteChallenge<TChallenge>()
    where TChallenge : AdventOfCodeChallenge, new()
{
    var challenge = new TChallenge();
    try
    {
        Console.WriteLine($"Executing challenge with type {typeof(TChallenge)}");
        challenge.ExecuteTests();
        Console.WriteLine();
        challenge.Execute();
        Console.WriteLine();
        Console.WriteLine();
    }
    catch (AoCTestFailedException)
    { }
}

static void ExecuteAll()
{
    ExecuteChallenge<Challenges.Day01.Part1>();
    ExecuteChallenge<Challenges.Day01.Part2>();
    ExecuteChallenge<Challenges.Day02.Part1>();
    ExecuteChallenge<Challenges.Day02.Part2>();
    ExecuteChallenge<Challenges.Day03.Part1>();
    ExecuteChallenge<Challenges.Day03.Part2>();
    ExecuteChallenge<Challenges.Day04.Part1>();
    ExecuteChallenge<Challenges.Day04.Part2>();
    ExecuteChallenge<Challenges.Day05.Part1>();
    ExecuteChallenge<Challenges.Day05.Part2>();
    ExecuteChallenge<Challenges.Day06.Part1>();
    ExecuteChallenge<Challenges.Day06.Part2>();
    ExecuteChallenge<Challenges.Day07.Part1>();
    ExecuteChallenge<Challenges.Day07.Part2>();
    ExecuteChallenge<Challenges.Day08.Part1>();
    ExecuteChallenge<Challenges.Day08.Part2>();
    ExecuteChallenge<Challenges.Day09.Part1>();
    ExecuteChallenge<Challenges.Day09.Part2>();
    ExecuteChallenge<Challenges.Day10.Part1>();
    ExecuteChallenge<Challenges.Day10.Part2>();
}
