using Challenges.Day02.Model;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using Utility;

namespace Challenges.Day02;

/// <summary>
/// 
/// </summary>
public class Part1 : AdventOfCodeChallenge
{
    private const string Sample = """
                                  Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                                  Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                                  Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                                  Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                                  Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                                  """;

    public Part1()
        : base(2, 1, @"Day02\input.txt")
    {
        SetupTest(Sample, 8);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 1853);
    }

    protected override long Run(string input)
    {
        var result = 0L;

        var results = new List<Game>();
        var gamePattern = RegularExpressions.GamePattern();
        var drawPattern = RegularExpressions.DrawPattern();
        
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
        result = results.Where((game) => !game.Turns.Any((turn) => turn.Items.Any((draw) => !draw.IsValid))).Sum((game) => game.Number);

        return result;
    }
}
