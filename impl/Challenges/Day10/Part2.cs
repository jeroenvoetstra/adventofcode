using Challenges.Day10.Models;
using Utility;

namespace Challenges.Day10;

/// <summary>
/// 
/// </summary>
public class Part2() : AdventOfCodeChallenge(10, 2, @"Day10\input.txt")
{
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
        var switchPoints = map[mainLoop.First()].HasFlag(Direction.South) ?
            new[] { 'S', 'F', '7', '|' } :
            new[] { 'F', '7', '|' }
            ;
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
                startNode = map.First((mapItem) => mapItem.Key == (startX, startY));

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
            var (x, y) when x == startNode.Key.X + 1 && y == startNode.Key.Y => Direction.West,
            // Else, if x is on the left of start (or previous) node, don't go right.
            var (x, y) when x == startNode.Key.X - 1 && y == startNode.Key.Y => Direction.East,
            // Same as before, don't go up when start (or previous) node was above
            var (x, y) when x == startNode.Key.X && y == startNode.Key.Y + 1 => Direction.North,
            // And finally the same for down
            var (x, y) when x == startNode.Key.X && y == startNode.Key.Y - 1 => Direction.South,
            _ => throw new InvalidOperationException(),
        };
        var sourceDirectionStep2 = currentStep2.Key switch
        {
            // Same thing as above for the second step.
            var (x, y) when x == startNode.Key.X + 1 && y == startNode.Key.Y => Direction.West,
            var (x, y) when x == startNode.Key.X - 1 && y == startNode.Key.Y => Direction.East,
            var (x, y) when x == startNode.Key.X && y == startNode.Key.Y + 1 => Direction.North,
            var (x, y) when x == startNode.Key.X && y == startNode.Key.Y - 1 => Direction.South,
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
                var (x, y) when x == currentStep1.Key.X + 1 && y == currentStep1.Key.Y => Direction.West,
                var (x, y) when x == currentStep1.Key.X - 1 && y == currentStep1.Key.Y => Direction.East,
                var (x, y) when x == currentStep1.Key.X && y == currentStep1.Key.Y + 1 => Direction.North,
                var (x, y) when x == currentStep1.Key.X && y == currentStep1.Key.Y - 1 => Direction.South,
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
                var (x, y) when x == currentStep2.Key.X + 1 && y == currentStep2.Key.Y => Direction.West,
                var (x, y) when x == currentStep2.Key.X - 1 && y == currentStep2.Key.Y => Direction.East,
                var (x, y) when x == currentStep2.Key.X && y == currentStep2.Key.Y + 1 => Direction.North,
                var (x, y) when x == currentStep2.Key.X && y == currentStep2.Key.Y - 1 => Direction.South,
                _ => throw new InvalidOperationException(),
            };
            currentStep2 = new KeyValuePair<Position, Direction>((targetX, targetY), map[(targetX, targetY)]);
            yield return currentStep2.Key;

            if (currentStep1.Key == currentStep2.Key)
                break;
        }
    }
}
