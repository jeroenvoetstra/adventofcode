using System.Text.RegularExpressions;

namespace Challenges.Day12;

internal static partial class RegularExpressions
{
    [GeneratedRegex(@"(?<arrangement>[\#\.\?]+)\s+(?<summary>(\d+\,?)+)")]
    public static partial Regex LinePattern();
}
