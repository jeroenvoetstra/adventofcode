using System.Text.RegularExpressions;

namespace Challenges.Day08;

internal static partial class RegularExpressions
{
    [GeneratedRegex(@"(?<current>[A-Z0-9]{3,3})\s*\=\s*\((?<left>[A-Z0-9]{3,3})\,\s*(?<right>[A-Z0-9]{3,3})\)")]
    public static partial Regex LocationPattern();
}
