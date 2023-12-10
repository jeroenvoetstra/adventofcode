namespace Challenges.Day05.Models;

public class Mapping
{
    private long[] _code;

    public long Destination => _code[0];
    public long Source => _code[1];
    public long Length => _code[2];

    public long DestinationModifier => Destination - Source;

    public Mapping(long[] code)
    {
        if (code.Length != 3)
            throw new Exception();
        _code = code;
    }

    public bool IsInSourceRange(long item) => item >= Source && item < Source + Length;
}
