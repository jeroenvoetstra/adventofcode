using Challenges.Day10.Models;
using Utility;
using static Challenges.Day10.Part1;

namespace Challenges.Day10;

/// <summary>
/// 
/// </summary>
public class Part2 : AdventOfCodeChallenge
{
    private const string Sample = """
                                  ...........
                                  .S-------7.
                                  .|F-----7|.
                                  .||.....||.
                                  .||.....||.
                                  .|L-7.F-J|.
                                  .|..|.|..|.
                                  .L--J.L--J.
                                  ...........
                                  """;
    private const string Sample2 = """
                                   .F----7F7F7F7F-7....
                                   .|F--7||||||||FJ....
                                   .||.FJ||||||||L7....
                                   FJL7L7LJLJ||LJ.L-7..
                                   L--J.L7...LJS7F-7L7.
                                   ....F-J..F7FJ|L7L7L7
                                   ....L7.F7||L7|.L7L7|
                                   .....|FJLJ|FJ|F7|.LJ
                                   ....FJL-7.||.||||...
                                   ....L---J.LJ.LJLJ...
                                   """;
    private const string Sample3 = """
                                   FF7FSF7F7F7F7F7F---7
                                   L|LJ||||||||||||F--J
                                   FL-7LJLJ||||||LJL-77
                                   F--JF--7||LJLJ7F7FJ-
                                   L---JF-JLJ.||-FJLJJ7
                                   |F|F-JF---7F7-L7L|7|
                                   |FFJF7L7F-JF7|JL---7
                                   7-L-JL7||F7|L7F-7F7|
                                   L.L7LFJ|||||FJL7||LJ
                                   L7JLJL-JLJLJL--JLJ.L
                                   """;

    public Part2()
        : base(10, 2, @"Day10\input.txt")
    {
        SetupTest(Sample, 4);
        SetupTest(Sample2, 8);
        SetupTest(Sample3, 10);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 461);
    }

    protected override long Run(string input)
    {
        var result = 0L;

        var lines = input.Split('\n')
            .Where((line) => !string.IsNullOrWhiteSpace(line))
            .Select((line) => line.Replace("\r", ""))
            .ToArray()
            ;

        var map = new Dictionary<Position, Direction>();
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                var node = line[x];
                if (node != '.')
                {
                    var nodeConnections = node switch
                    {
                        '|' => Direction.North | Direction.South,
                        '-' => Direction.East | Direction.West,
                        'L' => Direction.North | Direction.East,
                        'J' => Direction.North | Direction.West,
                        '7' => Direction.South | Direction.West,
                        'F' => Direction.East | Direction.South,
                        'S' => Direction.Unknown,
                        _ => throw new InvalidOperationException(),
                    };

                    map.Add((x, y), nodeConnections);
                }
            }
        }

        var mainLoop = GetMainLoopNodes(map).ToArray();

        // Count all parts of the main loop with a connector to the south as switch points for
        // inside to outside and the other way around. Implementation of the non-zero rule,
        // see https://en.wikipedia.org/wiki/Nonzero-rule. Only counting nodes pointing north
        // should also work.
        char[] switchPoints =
            map[mainLoop.First()].HasFlag(Direction.South) ? ['S', 'F', '7', '|'] : ['F', '7', '|'];
        for (var y = 0; y < lines.Length; y++)
        {
            var inside = false;
            for (var x = 0; x < lines[y].Length; x++)
            {
                var c = lines[y][x];

                if (switchPoints.Contains(c) && mainLoop.Contains((x, y)))
                    inside = !inside;
                else if (inside && !mainLoop.Contains((x, y)))
                    result++;
            }
        }

        return result;
    }

    private static IEnumerable<Position> GetMainLoopNodes(Dictionary<Position, Direction> map)
    {
        var startNode = map.Single((item) => item.Value == Direction.Unknown);
        yield return startNode.Key;
        var (startX, startY) = startNode.Key;
        var startingPoints = map
            .Where((item) =>
                (item.Key == (startX - 1, startY) && item.Value.HasFlag(Direction.East))
                    || (item.Key == (startX + 1, startY) && item.Value.HasFlag(Direction.West))
                    || (item.Key == (startX, startY - 1) && item.Value.HasFlag(Direction.South))
                    || (item.Key == (startX, startY + 1) && item.Value.HasFlag(Direction.North))
                )
            .Select((item) =>
            {
                // Modify starting point with directions
                var lateral = item.Key.X == startX - 1 ? Direction.West : Direction.East;
                var longitudinal = item.Key.Y == startY - 1 ? Direction.North : Direction.South;
                map[(startX, startY)] = lateral | longitudinal;
                startNode = map.First((item) => item.Key == (startX, startY));

                return item;
            })
            .ToArray()
            ;
        if (startingPoints.Length != 2)
            throw new InvalidOperationException();

        var currentStep1 = startingPoints[0];
        yield return currentStep1.Key;
        var currentStep2 = startingPoints[1];
        yield return currentStep2.Key;
        var sourceDirectionStep1 = currentStep1.Key switch
        {
            // If x is on the right of start (or previous) node, cross off going west when starting to move.
            (int x, int y) when x == startNode.Key.X + 1 && y == startNode.Key.Y => Direction.West,
            // Else, if x is on the left of start (or previous) node, don't go right.
            (int x, int y) when x == startNode.Key.X - 1 && y == startNode.Key.Y => Direction.East,
            // Same as before, don't go up when start (or previous) node was above
            (int x, int y) when x == startNode.Key.X && y == startNode.Key.Y + 1 => Direction.North,
            // And finally the same for down
            (int x, int y) when x == startNode.Key.X && y == startNode.Key.Y - 1 => Direction.South,
            _ => throw new InvalidOperationException(),
        };
        var sourceDirectionStep2 = currentStep2.Key switch
        {
            // Same thing as above for the second step.
            (int x, int y) when x == startNode.Key.X + 1 && y == startNode.Key.Y => Direction.West,
            (int x, int y) when x == startNode.Key.X - 1 && y == startNode.Key.Y => Direction.East,
            (int x, int y) when x == startNode.Key.X && y == startNode.Key.Y + 1 => Direction.North,
            (int x, int y) when x == startNode.Key.X && y == startNode.Key.Y - 1 => Direction.South,
            _ => throw new InvalidOperationException(),
        };

        while (true)
        {
            var (targetX, targetY) = (
                currentStep1.Value.HasFlag(Direction.West) && sourceDirectionStep1 != Direction.West ?
                    currentStep1.Key.X - 1 :
                    (currentStep1.Value.HasFlag(Direction.East) && sourceDirectionStep1 != Direction.East ? currentStep1.Key.X + 1 : currentStep1.Key.X),
                currentStep1.Value.HasFlag(Direction.North) && sourceDirectionStep1 != Direction.North ?
                    currentStep1.Key.Y - 1 :
                    (currentStep1.Value.HasFlag(Direction.South) && sourceDirectionStep1 != Direction.South ? currentStep1.Key.Y + 1 : currentStep1.Key.Y)
                );
            // We could also negate currentStep1.Value but then we cannot use our shiny logic :)
            sourceDirectionStep1 = (targetX, targetY) switch
            {
                (int x, int y) when x == currentStep1.Key.X + 1 && y == currentStep1.Key.Y => Direction.West,
                (int x, int y) when x == currentStep1.Key.X - 1 && y == currentStep1.Key.Y => Direction.East,
                (int x, int y) when x == currentStep1.Key.X && y == currentStep1.Key.Y + 1 => Direction.North,
                (int x, int y) when x == currentStep1.Key.X && y == currentStep1.Key.Y - 1 => Direction.South,
                _ => throw new InvalidOperationException(),
            };
            currentStep1 = new KeyValuePair<Position, Direction>((targetX, targetY), map[(targetX, targetY)]);
            yield return currentStep1.Key;

            (targetX, targetY) = (
                currentStep2.Value.HasFlag(Direction.West) && sourceDirectionStep2 != Direction.West ?
                    currentStep2.Key.X - 1 :
                    (currentStep2.Value.HasFlag(Direction.East) && sourceDirectionStep2 != Direction.East ? currentStep2.Key.X + 1 : currentStep2.Key.X),
                currentStep2.Value.HasFlag(Direction.North) && sourceDirectionStep2 != Direction.North ?
                    currentStep2.Key.Y - 1 :
                    (currentStep2.Value.HasFlag(Direction.South) && sourceDirectionStep2 != Direction.South ? currentStep2.Key.Y + 1 : currentStep2.Key.Y)
                );
            sourceDirectionStep2 = (targetX, targetY) switch
            {
                (int x, int y) when x == currentStep2.Key.X + 1 && y == currentStep2.Key.Y => Direction.West,
                (int x, int y) when x == currentStep2.Key.X - 1 && y == currentStep2.Key.Y => Direction.East,
                (int x, int y) when x == currentStep2.Key.X && y == currentStep2.Key.Y + 1 => Direction.North,
                (int x, int y) when x == currentStep2.Key.X && y == currentStep2.Key.Y - 1 => Direction.South,
                _ => throw new InvalidOperationException(),
            };
            currentStep2 = new KeyValuePair<Position, Direction>((targetX, targetY), map[(targetX, targetY)]);
            yield return currentStep2.Key;

            if (currentStep1.Key == currentStep2.Key)
                break;
        }
    }
}
