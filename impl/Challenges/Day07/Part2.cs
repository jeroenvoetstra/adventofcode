using Challenges.Day07.Models.Part2;
using System.Text.RegularExpressions;
using Utility;

namespace Challenges.Day07;

/// <summary>
/// A bit more of a challenge. The heart of the difference between this
/// and part one lies in the implementation of the <see cref="Hand.CalculateScore"/> method. It
/// has additional rules for joker replacements.
/// </summary>
public class Part2 : AdventOfCodeChallenge
{
    private const string Sample = """
                                  32T3K 765
                                  T55J5 684
                                  KK677 28
                                  KTJJT 220
                                  QQQJA 483
                                  """;

    public Part2()
        : base(7, 2, @"Day07\input.txt")
    {
        SetupTest(Sample, 5905);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 250665248);
    }

    protected override long Run(string input)
    {
        var handPattern = RegularExpressions.HandPattern();

        var allHands = new List<Hand>();

        using var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            var match = handPattern.Match(line);
            if (match.Success)
            {
                var hand = match.Groups["hand"].Value;
                var bid = match.Groups["bid"].Value;
                allHands.Add(new Hand(hand, Convert.ToInt32(bid)));
            }
        }

        allHands.Sort();
        var rank = 0;
        var total = 0L;
        foreach (var hand in allHands)
        {
            rank++;
            total += rank * hand.Bid;
        }

        return total;
    }

    bool TryGetNumberFromWord(string? word, out int result) => word?.ToLower() switch
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
