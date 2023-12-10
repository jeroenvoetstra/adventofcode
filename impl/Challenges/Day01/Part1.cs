using Utility;

namespace Challenges.Day01;

/// <summary>
/// Nothing too difficult, just using a '\d' regular expression
/// and getting the first and the last match.
/// </summary>
public class Part1 : AdventOfCodeChallenge
{
    private const string Sample = """
                                  1abc2
                                  pqr3stu8vwx
                                  a1b2c3d4e5f
                                  treb7uchet
                                  """;

    public Part1()
        : base(1, 1, @"Day01\input.txt")
    {
        SetupTest(Sample, 142);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 53080);
    }

    protected override long Run(string input)
    {
        var result = 0;
        var numberPattern = RegularExpressions.NumberPattern();

        using var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var matches = numberPattern.Matches(line);
            if (matches.Count <= 0)
                continue;

            var firstMatchValue = matches.First().Value;
            var lastMatchValue = matches.Last().Value;

            var number = Convert.ToInt32($"{firstMatchValue}{lastMatchValue}");
            if (number is < 10 or > 100)
                throw new Exception();

            result += number;
        }

        return result;
    }
}
