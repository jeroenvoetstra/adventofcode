namespace Utility.Exceptions;

public class AoCTestFailedException(long expected, long actual) : Exception
{
    public long Expected { get; } = expected;
    public long Actual { get; } = actual;
}
