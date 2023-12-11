namespace Utility;

public abstract class AdventOfCodeChallenge(int day, int part, string inputResourcePath)
{
    public int Day { get; } = day;
    public int Part { get; } = part;

    public void Execute()
    {
        using var reader = new StreamReader(AssemblyResource.Load(inputResourcePath)!);
        var input = reader.ReadToEnd();

        var start = DateTime.Now;
        var result = Run(input);
        var elapsed = DateTime.Now - start;

        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Advent of Code day {Day}, part {Part} executed in {elapsed.TotalMilliseconds:N0} ms.");
        Console.ForegroundColor = originalColor;
        Console.WriteLine($"\tThe answer is: {result}");
    }

    protected internal abstract long Run(string input);
}
