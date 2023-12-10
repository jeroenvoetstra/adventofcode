namespace Challenges.Day03.Models;

public class Number : Item
{
    public int Value { get; set; }
    public bool IsValid { get; set; }

    public bool IsInVicinity(Symbol symbol)
    {
        var (xMin, y) = Position;
        var xMax = xMin + Value.ToString().Length - 1;

        return y >= (symbol.Position.Y - 1) && y <= (symbol.Position.Y + 1)
            && symbol.Position.X >= xMin - 1 && symbol.Position.X <= xMax + 1
            ;
    }
}
