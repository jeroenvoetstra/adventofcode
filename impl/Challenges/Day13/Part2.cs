using Utility;

namespace Challenges.Day13;

/// <summary>
/// 
/// </summary>
public class Part2() : AdventOfCodeChallenge(13, 2, @"Day13\input.txt")
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
            var horizontalReflectionPoint = GetTurningPoint(horizontalLines);
            result += 100 * horizontalReflectionPoint;

            if (horizontalReflectionPoint > 0)
                continue;

            var verticalLines = Enumerable.Range(0, horizontalLines.First().Length)
                .Select((column) => new string(Enumerable.Range(0, horizontalLines.Length).Select((row) => horizontalLines[row][column]).ToArray()))
                .ToArray();
            var verticalReflectionPoint = GetTurningPoint(verticalLines);

            result += verticalReflectionPoint;
        }

        return result;
    }

    private static int GetTurningPoint(IReadOnlyList<string> input)
    {
        var result = 0;
        for (var i = 0; i < input.Count; i++)
        {
            if (i == 0)
                continue;

            // Not very efficient but I spent way too much time on this... We need at least 1 line with hamming distance 1 when
            // backtracking. The rest of the lines need to match in order for the smudge clearing to work
            var has1HammingDistance1 = Enumerable.Range(0, Math.Min(i, input.Count - i))
                .Count((j) => HammingDistance(input[i + j], input[i - j - 1]) == 1) == 1;
            var allEqualOrWithHammingDistance1 = Enumerable.Range(0, Math.Min(i, input.Count - i))
                .All((j) => input[i + j] == input[i - j - 1] || HammingDistance(input[i + j], input[i - j - 1]) == 1);

            if (!has1HammingDistance1 || !allEqualOrWithHammingDistance1)
                continue;

            return i;
        }

        return result;
    }

    private static int HammingDistance(string left, string right)
    {
        if (left == null || right == null || left.Length != right.Length)
            throw new ArgumentException();

        return left.Zip(right, (c1, c2) => c1 != c2).Count((result) => result);
    }
}