using System.Text.RegularExpressions;

namespace Challenges.Day03;

internal static partial class RegularExpressions
{
    [GeneratedRegex(@"\b\d+\b")]
    public static partial Regex NumberPattern();

    [GeneratedRegex(@"[^\d\.]")]
    public static partial Regex SymbolPattern();
}
