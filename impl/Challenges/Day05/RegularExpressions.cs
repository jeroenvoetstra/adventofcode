using System.Text.RegularExpressions;

namespace Challenges.Day05;

internal static partial class RegularExpressions
{
    [GeneratedRegex(@"(?<seeds>seeds: (?<seedNumbers>(?<seedNumberItem>(\d+)\s?)+))", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline)]
    public static partial Regex SeedsPattern();
}
