using System.Text.RegularExpressions;

namespace Challenges.Day01;

internal static partial class RegularExpressions
{
    [GeneratedRegex(@"\d")]
    public static partial Regex NumberPattern();

    [GeneratedRegex(@"(\d|one|two|three|four|five|six|seven|eight|nine)")]
    public static partial Regex NumbersWrittenPattern();

    [GeneratedRegex(@"(\d|eno|owt|eerht|ruof|evif|xis|neves|thgie|enin)")]
    public static partial Regex NumbersWrittenReversePattern();
}
