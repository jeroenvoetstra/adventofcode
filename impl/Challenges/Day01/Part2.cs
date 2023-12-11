using Utility;

namespace Challenges.Day01;

/// <summary>
/// The difficulty of this challenge lies in the fact that
/// two (written) numbers can have overlap, e.g. twone (two
/// and one) or oneight (one and eight). Getting the last one
/// is the challenge here.
/// It's possible to use look-aheads in the regular expression
/// but for the sake of simplicity, why not have a reverse
/// one to get the first match from the reversed input?
/// </summary>
public class Part2() : AdventOfCodeChallenge(1, 2, @"Day01\input.txt")
{
    protected override long Run(string input)
    {
        var result = 0;
        var numberPattern = RegularExpressions.NumbersWrittenPattern();
        var numberReversePattern = RegularExpressions.NumbersWrittenReversePattern();

        using var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var firstMatch = numberPattern.Match(line);
            var lastMatch = numberReversePattern.Match(new string(line.Reverse().ToArray()));
            if (!firstMatch.Success || !lastMatch.Success)
                continue;
            
            var firstMatchValue = firstMatch.Value;
            var lastMatchValue = new string(lastMatch.Value.Reverse().ToArray());
            if (!TryGetNumberFromWord(firstMatchValue, out var first))
            {
                first = Convert.ToInt32(firstMatchValue);
            }
            if (!TryGetNumberFromWord(lastMatchValue, out var last))
            {
                last = Convert.ToInt32(lastMatchValue);
            }

            var number = Convert.ToInt32($"{first}{last}");
            if (number is < 10 or > 100)
                throw new Exception();

            result += number;
        }

        return result;
    }

    private static bool TryGetNumberFromWord(string? word, out int result) => word?.ToLower() switch
    {
        // ReSharper disable ConditionIsAlwaysTrueOrFalse
        "one" => (result = 1) == 1,
        "two" => (result = 2) == 2,
        "three" => (result = 3) == 3,
        "four" => (result = 4) == 4,
        "five" => (result = 5) == 5,
        "six" => (result = 6) == 6,
        "seven" => (result = 7) == 7,
        "eight" => (result = 8) == 8,
        "nine" => (result = 9) == 9,
        _ => (result = 0) != 0,
        // ReSharper restore ConditionIsAlwaysTrueOrFalse
    };

}
