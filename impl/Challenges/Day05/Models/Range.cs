namespace Challenges.Day05.Models;

public record Range(long Min, long Max, long Offset = 0)
{
    public IEnumerable<Range> Intersect(IEnumerable<Range> input)
    {
        foreach (var item in input)
        {
            if (HasOverlap(item))
                yield return new Range(Math.Max(item.Min, Min) + item.Offset, Math.Min(item.Max, Max) + item.Offset);
        }
    }

    public IEnumerable<Range> Except(IEnumerable<Range> input)
    {
        if (Min < input.Min((item) => item.Min))
            yield return new Range(Min, Max);
        if (Max > input.Max((item) => item.Max))
            yield return new Range(Min, Max);
    }

    public bool HasOverlap(Range other)
    {
        return (Min < other.Max && Max >= other.Min) || (Max >= other.Min && Max < other.Max);
    }
}
