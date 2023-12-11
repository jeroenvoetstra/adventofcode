using Challenges.Day08.Models;
using Utility;

namespace Challenges.Day08;

public class Part2() : AdventOfCodeChallenge(8, 2, @"Day08\input.txt")
{
    protected override long Run(string input)
    {
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

            buffer.TryAdd(id, id);
            buffer.TryAdd(left, left);
            buffer.TryAdd(right, right);

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
                currentNode = instructions[i] switch
                {
                    'L' => currentNode.LeftDescendant,
                    'R' => currentNode.RightDescendant,
                    _ => throw new InvalidOperationException(),
                };
                nodeResult++;

                if (currentNode!.Identifier.EndsWith('Z'))
                {
                    // If the end node is already present, we are starting to loop, so use previous result and break out of processing.
                    if (lcmStore.ContainsKey(currentNode.Identifier))
                        break;

                    lcmStore[currentNode.Identifier] = nodeResult;
                }

                // Restart the instructions loop, we'll break out of it when we found what we're looking for.
                if (i == instructions.Length - 1)
                    i = -1;
            }
        }

        return LeastCommonMultiple(lcmStore.Values.AsEnumerable());
    }

    private static long LeastCommonMultiple(IEnumerable<long> input)
    {
        return input.Aggregate((aggregator, value) => aggregator * value / GreatestCommonDivisor(aggregator, value));
    }

    private static long GreatestCommonDivisor(long input1, long input2)
    {
        return input2 == 0 ? input1 : GreatestCommonDivisor(input2, input1 % input2);
    }
}
