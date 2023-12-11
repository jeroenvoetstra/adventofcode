using Challenges.Day08.Models;
using Utility;

namespace Challenges.Day08;

public class Part1() : AdventOfCodeChallenge(8, 1, @"Day08\input.txt")
{
    protected override long Run(string input)
    {
        var result = 0;

        var lines = input.Split('\n')
            .Where((line) => !string.IsNullOrWhiteSpace(line))
            .Select((line) => line.Replace("\r", ""))
            .ToArray()
            ;

        var instructions = lines.First();

        var buffer = new Dictionary<string, Node>();
        var nodePattern = RegularExpressions.LocationPattern();

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

        var currentNode = buffer["AAA"];
        for (var i = 0; i < instructions.Length; i++)
        {
            var instruction = instructions[i];
            currentNode = instruction switch
            {
                'L' => currentNode.LeftDescendant,
                'R' => currentNode.RightDescendant,
                _ => throw new InvalidOperationException(),
            };
            result++;

            if (currentNode!.Identifier == "ZZZ")
                break;

            if (i == instructions.Length - 1)
                i = -1;
        }

        return result;
    }
}
