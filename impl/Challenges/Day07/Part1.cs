using Challenges.Day07.Models.Part1;
using Utility;

namespace Challenges.Day07;

/// <summary>
/// Using a simple regular expression which splits hand and bid
/// is enough to solve this one. To determine a hand's rank, 
/// sorting the list of hands is done through implementation of the
/// <see cref="IComparable{T}"/> interface.
/// </summary>
public class Part1() : AdventOfCodeChallenge(7, 1, @"Day07\input.txt")
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
        var total = 0;
        foreach (var hand in allHands)
        {
            rank++;
            total += rank * hand.Bid;
        }

        return total;
    }
}
