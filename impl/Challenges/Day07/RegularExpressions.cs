using System.Text.RegularExpressions;

namespace Challenges.Day07;

internal static partial class RegularExpressions
{
    [GeneratedRegex(@"Time:\s*(?<times>(?<time>\d+)\s*)+Distance:\s*(?<distances>(?<distance>\d+)\s*)+")]
    public static partial Regex SummaryPattern();

    [GeneratedRegex(@"(?<hand>[2-9TJQKA]{5,5})\s+(?<bid>\d+)")]
    public static partial Regex HandPattern();
}
