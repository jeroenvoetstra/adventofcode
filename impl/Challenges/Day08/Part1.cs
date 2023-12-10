using Challenges.Day08.Models;
using Utility;

namespace Challenges.Day08;

public class Part1 : AdventOfCodeChallenge
{
    private const string Sample = """
        RL

        AAA = (BBB, CCC)
        BBB = (DDD, EEE)
        CCC = (ZZZ, GGG)
        DDD = (DDD, DDD)
        EEE = (EEE, EEE)
        GGG = (GGG, GGG)
        ZZZ = (ZZZ, ZZZ)
        """;
    private const string Sample2 = """
        LLR

        AAA = (BBB, BBB)
        BBB = (AAA, ZZZ)
        ZZZ = (ZZZ, ZZZ)
        """;

    public Part1()
        : base(8, 1, @"Day08\input.txt")
    {
        SetupTest(Sample, 2);
        SetupTest(Sample2, 6);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 18113);
    }

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

            if (!buffer.ContainsKey(id))
                buffer.Add(id, (Node)id);
            if (!buffer.ContainsKey(left))
                buffer.Add(left, (Node)left);
            if (!buffer.ContainsKey(right))
                buffer.Add(right, (Node)right);

            buffer[id].LeftDescendant = buffer[left];
            buffer[id].RightDescendant = buffer[right];
        }

        var currentNode = buffer["AAA"];
        for (var i = 0; i < instructions.Length; i++)
        {
            var instruction = instructions[i];
            var next = instruction switch
            {
                'L' => currentNode = currentNode!.LeftDescendant,
                'R' => currentNode = currentNode!.RightDescendant,
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
