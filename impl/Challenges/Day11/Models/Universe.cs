namespace Challenges.Day11.Models;

public class Universe
{
    public Galaxy[] Galaxies { get; private init; } = null!;

    public static Universe Create(IReadOnlyList<string> input)
    {
        if (input == null || input.Count == 0 || input.Any(line => line.Length != input.First().Length))
            throw new ArgumentException("Universe is malformed", nameof(input));

        var width = input.First().Length;
        var height = input.Count;

        var lines = input.Select((line) => line.ToCharArray()).ToArray();
        return new Universe()
        {
            Galaxies = Enumerable.Range(0, width)
                .SelectMany((x) => Enumerable.Range(0, height).Select((y) =>
                    lines[y][x] == '#' ? new Galaxy() { X = x, Y = y } : null)
                )
                .Where((item) => item != null)
                .Select((item) => item!)
                .ToArray(),
        };
    }

    public void Expand(int by)
    {
        var (minX, minY) = (Galaxies.Min((item) => item.X), Galaxies.Min((item) => item.Y));
        var (maxX, maxY) = (Galaxies.Max((item) => item.X), Galaxies.Max((item) => item.Y));
        var xIndices = Enumerable.Range((int)minX, (int)(maxX - minX)).Where((x) => !Galaxies.Any((item) => item.X == x)).ToList();
        var yIndices = Enumerable.Range((int)minY, (int)(maxY - minY)).Where((y) => !Galaxies.Any((item) => item.Y == y)).ToList();
        for (var index = xIndices.Count - 1; index >= 0; index--)
        {
            var x = xIndices[index];
            foreach (var location in Galaxies.Where((location) => location.X >= x))
            {
                location.X += by - 1;
            }
        }
        for (var index = yIndices.Count - 1; index >= 0; index--)
        {
            var y = yIndices[index];
            foreach (var location in Galaxies.Where((location) => location.Y >= y))
            {
                location.Y += by - 1;
            }
        }
    }
}
