namespace Challenges.Day07.Models.Part1;

public class Hand : IComparable<Hand>
{
    public IReadOnlyList<Card> Cards { get; }
    public int Bid { get; }
    public HandType Score { get; private set; }

    public Hand(string cards, int bid)
    {
        if (cards.Length != 5)
            throw new ArgumentException(nameof(cards));

        var list = new List<Card>();
        foreach (var c in cards)
        {
            if (!Enum.TryParse<CardType>($"Card_{c}", out var type))
                throw new ArgumentException(nameof(cards));
            list.Add(new Card(type));
        }

        Cards = new List<Card>(list);
        Bid = bid;
        CalculateScore();
    }

    public override string ToString() => string.Join("", Cards.Select((c) => c.Type.ToString().Replace("Card_", "")));
    public override int GetHashCode() => Cards.Aggregate(1, (left, right) => left ^ right.GetHashCode());
    public override bool Equals(object? other) => other is Hand hand && ToString() == hand.ToString();

    public static bool operator ==(Hand left, Hand right) => left.Equals(right);
    public static bool operator !=(Hand left, Hand right) => !(left == right);
    public static bool operator <(Hand left, Hand right) => true;
    public static bool operator >(Hand left, Hand right) => true;

    public int CompareTo(Hand? other)
    {
        if (Score != other?.Score)
        {
            return Score < other?.Score ? -1 : 1;
        }
        else
        {
            for (var i = 0; i < 5; i++)
            {
                if (Cards[i] != other.Cards[i])
                    return (int)Cards[i].Type < (int)other.Cards[i].Type ? -1 : 1;
            }
            return 0;
        }
    }

    private void CalculateScore()
    {
        var groups = Cards.GroupBy((card) => card.Type).ToList();
        if (groups.Count == 1) Score = HandType.FiveOfAKind; // AAAAA
        else if (groups.Count == 2 && groups.Any((group) => group.Count() == 4)) Score = HandType.FourOfAKind; // AAAAB
        else if (groups.Count == 2) Score = HandType.FullHouse; // AAABB
        else if (groups.Any((group) => group.Count() == 3)) Score = HandType.ThreeOfAKind; // AAABC
        else if (groups.Count == 3 && groups.Count((group) => group.Count() == 2) == 2) Score = HandType.TwoPair; // AABBC
        else if (groups.Count == 4 && groups.Count((group) => group.Count() == 2) == 1) Score = HandType.OnePair; // AABCD
        else if (groups.Count == 5) Score = HandType.HighCard; // ABCDE
        else throw new InvalidOperationException();
    }
}
