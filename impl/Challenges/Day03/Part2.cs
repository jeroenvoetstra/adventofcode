using Challenges.Day03.Models;
using Utility;

namespace Challenges.Day03;

/// <summary>
/// 
/// </summary>
public class Part2() : AdventOfCodeChallenge(3, 2, @"Day03\input.txt")
{
    protected override long Run(string input)
    {
        var numbers = new List<Number>();
        var symbols = new List<Symbol>();

        var numberPattern = RegularExpressions.NumberPattern();
        var symbolPattern = RegularExpressions.SymbolPattern();
        using var reader = new StringReader(input);
        var lineCounter = 0;
        while (reader.ReadLine() is { } line)
        {
            numbers.AddRange(numberPattern.Matches(line).Select((m) => new Number() { Value = Convert.ToInt32(m.Value), Position = new Position(m.Index, lineCounter) }));
            symbols.AddRange(symbolPattern.Matches(line).Select((m) => new Symbol() { Position = new Position(m.Index, lineCounter), IsGear = m.Value == "*" }));

            lineCounter++;
        }

        foreach (var number in numbers)
        {
            var relevantSymbols = symbols.Where((symbol) => symbol.Position.Y >= (number.Position.Y - 1) && symbol.Position.Y <= (number.Position.Y + 1));
            var adjacentSymbols = relevantSymbols.Where(number.IsInVicinity).ToList();
            if (adjacentSymbols.Count == 0)
                continue;

            number.IsValid = true;
            foreach (var symbol in adjacentSymbols)
            {
                symbol.AttachedNumbers.Add(number);
            }
        }

        return symbols.Where((symbol) => symbol is { IsGear: true, AttachedNumbers.Count: 2 }).Sum((symbol) => symbol.AttachedNumbers[0].Value * symbol.AttachedNumbers[1].Value);
    }
}
