using Challenges.Day07.Models.Part2;
using Utility;

namespace Challenges.Day07;

/// <summary>
/// A bit more of a challenge. The heart of the difference between this
/// and part one lies in the implementation of the <see cref="Hand.CalculateScore"/> method. It
/// has additional rules for joker replacements.
/// </summary>
public class Part2() : AdventOfCodeChallenge(7, 2, @"Day07\input.txt")
{
    protected override long Run(string input)
    {
        var handPattern = RegularExpressions.HandPattern();

        var allHands = new List<Hand>();

        using var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            var match = handPattern.Match(line);
            if (!match.Success)
                continue;

            var hand = match.Groups["hand"].Value;
            var bid = match.Groups["bid"].Value;
            allHands.Add(new Hand(hand, Convert.ToInt32(bid)));
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
}
