using System.Text.RegularExpressions;

namespace Challenges.Day06;

internal static partial class RegularExpressions
{
    [GeneratedRegex(@"Time:\s*(?<times>(?<time>\d+)\s*)+Distance:\s*(?<distances>(?<distance>\d+)\s*)+")]
    public static partial Regex SummaryPattern();
}
