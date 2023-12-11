﻿using Challenges.Day02.Model;
using Utility;

namespace Challenges.Day02;

/// <summary>
/// 
/// </summary>
public class Part1() : AdventOfCodeChallenge(2, 1, @"Day02\input.txt")
{
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
