using System.Text.RegularExpressions;
using Utility;

namespace Challenges.Day06;

/// <summary>
/// 
/// </summary>
public class Part2() : AdventOfCodeChallenge(6, 2, @"Day06\input.txt")
{
    protected override long Run(string input)
    {
        var summaryPattern = RegularExpressions.SummaryPattern();

        input = input.Replace(" ", "");
        var times = summaryPattern.Match(input).Groups["time"].Captures.OfType<Capture>().Select((item) => Convert.ToInt64(item.Value)).ToArray();
        var distances = summaryPattern.Match(input).Groups["distance"].Captures.OfType<Capture>().Select((item) => Convert.ToInt64(item.Value)).ToArray();
        if (times.Length != distances.Length)
            throw new Exception();

        var races = Enumerable.Range(0, times.Length).Select((i) => new { MaxTime = times[i], MinDistance = distances[i] }).ToArray();
        var results = races.Select((race) => new { Race = race, Solution = SolveDistanceOverTime(race.MaxTime, race.MinDistance) });//.Dump();

        return (long)results.Aggregate(1.0d, (left, right) => left * right.Solution);
    }

    double SolveDistanceOverTime(long maxTime, long minDistance)
    {
        // x^2 - [t]*x + [d] => (-b +/- sqrt(b^2 - 4ac)) / 2a
        var b = (double)-maxTime;
        var c = (double)minDistance;

        var min = Math.Ceiling((-b - Math.Sqrt(Math.Pow(b, 2) - (4 * c))) / 2);
        var max = Math.Floor((-b + Math.Sqrt(Math.Pow(b, 2) - (4 * c))) / 2);

        var result = max - min;
        if (min * (maxTime - min) == minDistance)
            result -= 1;
        if (max * (maxTime - max) == minDistance)
            result -= 1;

        return result + 1;
    }
}
