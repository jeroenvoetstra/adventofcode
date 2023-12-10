namespace Challenges.Day07.Models.Part2;

public record Card(CardType Type)
{
    public override int GetHashCode() => Type.GetHashCode();
}
