using System.Text.RegularExpressions;

namespace Challenges.Day02;

internal static partial class RegularExpressions
{
    [GeneratedRegex(@"Game (?<number>\d+): (?<values>[\d\w\s\,\;]+)", RegexOptions.IgnoreCase)]
    public static partial Regex GamePattern();

    [GeneratedRegex(@"(?<number>\d+) (?<color>(green|red|blue))", RegexOptions.IgnoreCase)]
    public static partial Regex DrawPattern();
}
