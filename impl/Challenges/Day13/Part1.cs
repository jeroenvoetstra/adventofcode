using Utility;

namespace Challenges.Day13;

/// <summary>
/// Main topic: comparing strings while simultaneously moving forward as well backtrack.
/// I chose to compare integers instead by constructing a binary string from the input,
/// replacing # with 1 and . with 0.
/// </summary>
public class Part1() : AdventOfCodeChallenge(13, 1, @"Day13\input.txt")
{
    protected override long Run(string input)
    {
        var result = 0L;

        var blocks = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        foreach (var block in blocks)
        {
            var horizontalLines = block.Split('\n')
                .Select((line) => line.Trim())
                .Where((line) => !string.IsNullOrWhiteSpace(line))
                .ToArray();
            var horizontalLinesAsIntegers = horizontalLines
                .Select((line) => Convert.ToInt64(line.Replace(".", "0").Replace("#", "1").PadLeft(64, '0'), 2))
                .ToArray();
            var horizontalReflectionPoint = GetTurningPoint(horizontalLinesAsIntegers);

            var verticalLines = Enumerable.Range(0, horizontalLines.First().Length)
                .Select((column) => new string(Enumerable.Range(0, horizontalLines.Length).Select((row) => horizontalLines[row][column]).ToArray()));
            var verticalLinesAsIntegers = verticalLines
                .Select((line) => Convert.ToInt64(line.Replace(".", "0").Replace("#", "1").PadLeft(64, '0'), 2))
                .ToArray();
            var verticalReflectionPoint = GetTurningPoint(verticalLinesAsIntegers);

            result += verticalReflectionPoint + (100 * horizontalReflectionPoint);
        }

        return result;
    }

    private static int GetTurningPoint(IReadOnlyList<long> input)
    {
        for (var i = 0; i < input.Count; i++)
        {
            if (i == 0)
                continue;

            if (input[i] != input[i - 1])
                continue;
            
            if (Enumerable.Range(0, Math.Min(i, input.Count - i)).Any((j) => input[i + j] != input[i - j - 1]))
                continue;

            return i;
        }
        return 0;
    }

}
