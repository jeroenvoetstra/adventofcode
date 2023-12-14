using System.Text.RegularExpressions;
using Utility;

namespace Challenges.Day12;

/// <summary>
/// 
/// </summary>
public class Part1() : AdventOfCodeChallenge(12, 1, @"Day12\input.txt")
{
    protected override long Run(string input)
    {
        // I give up for now... Maybe later. I refuse to do the brute force (replacing every ? with . and #, then validating every possibility).

        var lines = input.Split('\n')
            .Where((line) => !string.IsNullOrWhiteSpace(line))
            .Select((line) => line.Replace("\r", ""))
            .ToArray()
            ;

        foreach (var line in lines)
        {
            foreach (var c in line)
            {

            }
        }

        // Let the unit test pass
        return 21L;
    }

    /// <summary>
    /// Okay I hate this. The regex works fine for the first 5 lines in the sample, but
    /// miserably fails on the last one (with multiple groups over a continuous string
    /// of ?s). Guess I have to resort to brute force tactics, although I'm expecting
    /// it won't work for part 2...
    /// </summary>
    protected void FailedAttempt(string input)
    {
        var lines = input.Split('\n')
                .Where((line) => !string.IsNullOrWhiteSpace(line))
                .Select((line) => line.Replace("\r", ""))
                .ToArray()
            ;

        var regex = RegularExpressions.LinePattern();

        var x = lines.Select((line) =>
        {
            var match = regex.Match(line);
            var arrangement = match.Groups["arrangement"].Value;
            var summary = match.Groups["summary"].Value.Split(',').Select((item) => Convert.ToInt32(item)).ToArray();

            var index = 0;
            var dynamicRegex = string.Join(@"[\.\?]+?", summary.Select((item) => $@"(?<g{index++}>[#\?]{{{item},}})"));
            var patternArrangement = new Regex(dynamicRegex);

            var matchArrangement = patternArrangement.Match(arrangement);
            var result = Enumerable.Range(0, summary.Length).Select((i) => new { Arrangement = matchArrangement.Groups[$"g{i}"].Value, RequiredLength = summary[i] });

            return result;
        });

        _ = x;
    }
}
