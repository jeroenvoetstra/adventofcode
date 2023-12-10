using Challenges.Day08.Models;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using Utility;

namespace Challenges.Day08;

public class Part2 : AdventOfCodeChallenge
{
    private const string Sample = """
        LR

        11A = (11B, XXX)
        11B = (XXX, 11Z)
        11Z = (11B, XXX)
        22A = (22B, XXX)
        22B = (22C, 22C)
        22C = (22Z, 22Z)
        22Z = (22B, 22B)
        XXX = (XXX, XXX)
        """;

    public Part2()
        : base(8, 2, @"Day08\input.txt")
    {
        SetupTest(Sample, 6);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 12315788159977);
    }

    protected override long Run(string input)
    {
        var result = 0L;

        var lines = input.Split('\n')
            .Where((line) => !string.IsNullOrWhiteSpace(line))
            .Select((line) => line.Replace("\r", ""))
            .ToArray()
            ;

        var instructions = lines.First();

        var buffer = new Dictionary<string, Node>();
        var nodePattern = RegularExpressions.LocationPattern();

        // Build a dictionary with all nodes in it
        foreach (var line in lines.Skip(1))
        {
            var match = nodePattern.Match(line);
            if (!match.Success)
                throw new ArgumentException("Mismatching location line found");

            var id = match.Groups["current"].Value;
            var left = match.Groups["left"].Value;
            var right = match.Groups["right"].Value;

            if (!buffer.ContainsKey(id))
                buffer.Add(id, (Node)id);
            if (!buffer.ContainsKey(left))
                buffer.Add(left, (Node)left);
            if (!buffer.ContainsKey(right))
                buffer.Add(right, (Node)right);

            buffer[id].LeftDescendant = buffer[left];
            buffer[id].RightDescendant = buffer[right];
        }

        // Get all starting points.
        var currentNodes = buffer.Where((item) => item.Key.EndsWith('A')).Select((item) => item.Value).ToArray();
        // We'll use the least common multiplier to find our answer. When we arrive at an 'end' node again after previously visiting it from
        // the current starting point, use the last time we encountered it as our relevant step count. The LCM of all first occurrences before
        // starting to loop in each one will yield the correct result.
        var lcmStore = new Dictionary<string, long>();
        foreach (var node in currentNodes)
        {
            var currentNode = node;
            var nodeResult = 0L;
            for (var i = 0; i < instructions.Length; i++)
            {
                var next = instructions[i] switch
                {
                    'L' => currentNode = currentNode!.LeftDescendant,
                    'R' => currentNode = currentNode!.RightDescendant,
                    _ => throw new InvalidOperationException(),
                };
                nodeResult++;

                if (currentNode!.Identifier.EndsWith('Z'))
                {
                    // If the end node is already present, we are starting to loop, so use previous result and break out of processing.
                    if (lcmStore.ContainsKey(currentNode!.Identifier))
                        break;
                    else
                        lcmStore[currentNode!.Identifier] = nodeResult;
                }

                // Restart the instructions loop, we'll break out of it when we found what we're looking for.
                if (i == instructions.Length - 1)
                    i = -1;
            }
        }

        result = LeastCommonMultiple(lcmStore.Values.AsEnumerable());

        return result;
    }

    static long LeastCommonMultiple(IEnumerable<long> input)
    {
        return input.Aggregate((aggregator, value) => aggregator * value / GreatestCommonDivisor(aggregator, value));
    }

    static long GreatestCommonDivisor(long input1, long input2)
    {
        return input2 == 0 ? input1 : GreatestCommonDivisor(input2, input1 % input2);
    }
}
