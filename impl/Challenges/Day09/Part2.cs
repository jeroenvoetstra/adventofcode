using Utility;

namespace Challenges.Day09;

public class Part2() : AdventOfCodeChallenge(9, 2, @"Day09\input.txt")
{
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
            while (lastItem.Any(item => item != 0));

            for (var i = tracker.Count - 1; i > 0; i--)
            {
                tracker[i - 1].Insert(0, tracker[i - 1].First() - tracker[i].First());
            }

            result += tracker[0].First();
        }

        return result;
    }
}
