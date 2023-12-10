namespace Challenges.Day03.Models;

public class Symbol : Item
{
    public bool IsGear { get; set; }
    public List<Number> AttachedNumbers { get; } = [];
}
