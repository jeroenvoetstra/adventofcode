namespace Challenges.Day10.Models;

[Flags]
public enum Direction
{
    Unknown = 0,
    North = 1 << 0,
    East = 1 << 1,
    South = 1 << 2,
    West = 1 << 3,
}
