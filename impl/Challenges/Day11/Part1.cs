using Challenges.Day11.Models;
using Utility;

namespace Challenges.Day11;

/// <summary>
/// Create a set of coordinates for every galaxy, and use the Manhattan distance
/// to determine the distance between every set of galaxies. Things to take
/// into consideration is the calculation of the Manhattan distance itself, as well
/// as counting every connection between galaxies only once (creating a complete graph).
/// </summary>
public class Part1() : AdventOfCodeChallenge(11, 1, @"Day11\input.txt")
{
    protected override long Run(string input)
    {
        var lines = input.Split('\n')
            .Where((line) => !string.IsNullOrWhiteSpace(line))
            .Select((line) => line.Replace("\r", ""))
            .ToArray()
            ;

        var universe = Universe.Create(lines);
        universe.Expand(by: 2);
        
        var alreadyConnected = new List<Galaxy>();
        return universe.Galaxies
            .SelectMany((item) =>
                universe.Galaxies
                    .Except(alreadyConnected.Concat(new[] { item }))
                    .Select((item2) =>
                    {
                        alreadyConnected.Add(item);
                        return new[] { item, item2 };
                    })
                    .Distinct()
            )
            .Select((items) => new
            {
                From = items[1], To = items[0],
                // Today I learned this is called the Manhattan distance (https://en.wikipedia.org/wiki/Taxicab_geometry)
                Distance = (Math.Abs(items[1].X - items[0].X)) + (Math.Abs(items[1].Y - items[0].Y)),
            })
            .Sum((item) => item.Distance)
            ;
    }
}
