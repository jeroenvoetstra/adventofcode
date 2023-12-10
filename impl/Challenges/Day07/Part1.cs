using Challenges.Day07.Models.Part1;
using Utility;

namespace Challenges.Day07;

/// <summary>
/// Using a simple regular expression which splits hand and bid
/// is enough to solve this one. To determine a hand's rank, 
/// sorting the list of hands is done through implementation of the
/// <see cref="IComparable{T}"/> interface.
/// </summary>
public class Part1 : AdventOfCodeChallenge
{
    private const string Sample = """
                                  32T3K 765
                                  T55J5 684
                                  KK677 28
                                  KTJJT 220
                                  QQQJA 483
                                  """;

    public Part1()
        : base(7, 1, @"Day07\input.txt")
    {
        SetupTest(Sample, 6440);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 250120186);
    }

    protected override long Run(string input)
    {
        var handPattern = RegularExpressions.HandPattern();

        var allHands = new List<Hand>();

        using var reader = new StringReader(input);
        string? line;
        while ((line = reader.ReadLine()) != null)
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
        var total = 0;
        foreach (var hand in allHands)
        {
            rank++;
            total += rank * hand.Bid;
        }

        return total;
    }
}
