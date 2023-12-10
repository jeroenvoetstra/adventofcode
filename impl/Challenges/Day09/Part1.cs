using Challenges.Day08.Models;
using Utility;

namespace Challenges.Day09;

public class Part1 : AdventOfCodeChallenge
{
    private const string Sample = """
        0 3 6 9 12 15
        1 3 6 10 15 21
        10 13 16 21 30 45
        """;

    public Part1()
        : base(8, 1, @"Day09\input.txt")
    {
        SetupTest(Sample, 114);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 1666172641);
    }

    protected override long Run(string input)
    {
        var result = 0L;

        var lines = input.Split('\n')
            .Where((line) => !string.IsNullOrWhiteSpace(line))
            .Select((line) => line.Replace("\r", ""))
            .ToArray()
            ;

        foreach (var line in lines)
        {
            var numbers = line.Split(' ').Select((item) => Convert.ToInt64(item)).ToList();
            var tracker = new List<List<long>>(new[] { numbers });
            var lastItem = tracker.Last();
            do
            {
                tracker.Add(
                    Enumerable.Range(1, lastItem.Count - 1)
                        .Select((i) => lastItem[i] - lastItem[i - 1])
                        .ToList()
                    );
                lastItem = tracker.Last();
            }
            while (!lastItem.All((item) => item == 0));

            for (var i = tracker.Count - 1; i > 0; i--)
            {
                tracker[i - 1].Add(tracker[i - 1].Last() + tracker[i].Last());
            }

            result += tracker[0].Last();
        }

        return result;
    }
}
