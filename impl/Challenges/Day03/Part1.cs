using Challenges.Day03.Models;
using System.Text.RegularExpressions;
using Utility;

namespace Challenges.Day03;

/// <summary>
/// 
/// </summary>
public class Part1 : AdventOfCodeChallenge
{
    private const string Sample = """
                                  467..114..
                                  ...*......
                                  ..35..633.
                                  ......#...
                                  617*......
                                  .....+.58.
                                  ..592.....
                                  ......755.
                                  ...$.*....
                                  .664.598..
                                  """;

    public Part1()
        : base(3, 1, @"Day03\input.txt")
    {
        SetupTest(Sample, 4361);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 528799);
    }

    protected override long Run(string input)
    {
        var result = 0L;

        var numbers = new List<Number>();
        var symbols = new List<Symbol>();

        var numberPattern = RegularExpressions.NumberPattern();
        var symbolPattern = RegularExpressions.SymbolPattern();
        using var reader = new StringReader(input);
        var lineCounter = 0;
        while (reader.ReadLine() is { } line)
        {
            numbers.AddRange(numberPattern.Matches(line).OfType<Match>().Select((m) => new Number() { Value = Convert.ToInt32(m.Value), Position = new Position(m.Index, lineCounter) }));
            symbols.AddRange(symbolPattern.Matches(line).OfType<Match>().Select((m) => new Symbol() { Position = new Position(m.Index, lineCounter) }));

            lineCounter++;
        }

        foreach (var number in numbers)
        {
            var relevantSymbols = symbols.Where((symbol) => symbol.Position.Y >= (number.Position.Y - 1) && symbol.Position.Y <= (number.Position.Y + 1));
            if (relevantSymbols.Any(number.IsInVicinity))
                number.IsValid = true;
        }

        result = numbers.Where((number) => number.IsValid).Sum((number) => number.Value);

        return result;
    }
}
