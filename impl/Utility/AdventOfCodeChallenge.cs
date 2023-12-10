using System.Diagnostics;
using Utility.Exceptions;

namespace Utility;

public abstract class AdventOfCodeChallenge
{
    private readonly List<AoCTest> _tests;

    public int Day { get; }
    public int Part { get; }
    public string Input { get; }

    protected AdventOfCodeChallenge(int day, int part, string inputResourcePath, params AoCTest[] tests)
    {
        Day = day;
        Part = part;

        using var reader = new StreamReader(AssemblyResource.Load(inputResourcePath)!);
        Input = reader.ReadToEnd();

        _tests = new List<AoCTest>(tests);
    }

    public void SetupTest(string input, long expected)
    {
        _tests.Add(new AoCTest(input, expected));
    }

    public void ExecuteTests()
    {
        try
        {
            var start = DateTime.Now;
            foreach (var test in _tests)
            {
                var actual = Run(test.Input);
                if (actual != test.Expected)
                    throw new AoCTestFailedException(test.Expected, actual);
            }
            var elapsed = DateTime.Now - start;

            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Advent of Code day {Day}, part {Part}.");
            Console.ForegroundColor = originalColor;
            Console.WriteLine($"\t{_tests.Count:N0} test(s) succeeded in {elapsed.TotalMilliseconds:N0} ms.");
        }
        catch (AoCTestFailedException ex)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Advent of Code day {Day}, part {Part}. Tests failed. Expected: {ex.Expected}, actual: {ex.Actual}");
            Console.ForegroundColor = originalColor;
            throw;
        }
    }

    public void Execute()
    {
        var start = DateTime.Now;
        var result = Run(Input);
        var elapsed = DateTime.Now - start;

        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Advent of Code day {Day}, part {Part} executed in {elapsed.TotalMilliseconds:N0} ms.");
        Console.ForegroundColor = originalColor;
        Console.WriteLine($"\tThe answer is: {result}");
    }

    protected abstract long Run(string input);
}
