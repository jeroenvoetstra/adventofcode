using Challenges.Day02.Model;
using System.Text.RegularExpressions;
using Utility;

namespace Challenges.Day02;

/// <summary>
/// 
/// </summary>
public class Part2() : AdventOfCodeChallenge(2, 2, @"Day02\input.txt")
{
    protected override long Run(string input)
    {
        var result = 0L;

        var results = new List<Game>();
        var gamePattern = new Regex(@"Game (?<number>\d+): (?<values>[\d\w\s\,\;]+)", RegexOptions.IgnoreCase);
        var drawPattern = new Regex(@"(?<number>\d+) (?<color>(green|red|blue))", RegexOptions.IgnoreCase);
        using var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            var gameMatch = gamePattern.Match(line);
            if (gameMatch.Success)
            {
                var gameNumberRaw = gameMatch.Groups["number"].Value;
                var gameNumber = Convert.ToInt32(gameNumberRaw);
                var turns = gameMatch.Groups["values"].Value?.Split(';')?.Select((item) => item.Trim());

                results.Add(new Game()
                {
                    Raw = gameNumberRaw,
                    Number = gameNumber,
                    Turns = turns!.Select((turn) =>
                    {
                        var draws = turn?.Split(',')?.Select((draw) => draw.Trim());
                        return new Turn()
                        {
                            Raw = turn!,
                            Items = draws!.Select((draw) =>
                            {
                                var drawMatch = drawPattern.Match(draw);
                                return new DrawItem()
                                {
                                    Number = Convert.ToInt32(drawMatch.Groups["number"].Value),
                                    Color = drawMatch.Groups["color"].Value,
                                };
                            }),
                        };
                    }),
                });
            }
        }
        result = results.Sum((game) => game.Power);

        return result;
    }
}
