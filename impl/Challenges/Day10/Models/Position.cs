namespace Challenges.Day10.Models;

public record Position(int X, int Y)
{
    public static implicit operator Position((int x, int y) input) => new Position(input.x, input.y);
}
