namespace Challenges.Day08.Models;

public record Node(string Identifier)
{
    public Node? LeftDescendant { get; set; }
    public Node? RightDescendant { get; set; }

    public static implicit operator Node(string input) => new(input);

    public override string ToString() => Identifier;
}
