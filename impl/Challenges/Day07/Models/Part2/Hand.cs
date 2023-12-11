namespace Challenges.Day07.Models.Part2;

public class Hand : IComparable<Hand>
{
    public IReadOnlyList<Card> Cards { get; }
    public int Bid { get; }
    public HandType Score { get; private set; }

    public Hand(string cards, int bid)
    {
        if (cards.Length != 5)
            throw new ArgumentException("Invalid card count", nameof(cards));

        var list = new List<Card>();
        foreach (var c in cards)
        {
            if (!Enum.TryParse<CardType>($"Card{c}", out var type))
                throw new ArgumentException("Invalid card", nameof(cards));

            list.Add(new Card(type));
        }

        Cards = new List<Card>(list);
        Bid = bid;
        CalculateScore();
    }

    public override string ToString() => string.Join("", Cards.Select((c) => c.Type.ToString().Replace("Card", "")));
    public override int GetHashCode() => Cards.Aggregate(1, (left, right) => left ^ right.GetHashCode());
    public override bool Equals(object? other) => other is Hand hand && ToString() == hand.ToString();

    public static bool operator ==(Hand left, Hand right) => left.Equals(right);
    public static bool operator !=(Hand left, Hand right) => !(left == right);

    public int CompareTo(Hand? other)
    {
        if (Score != other?.Score)
        {
            return Score < other?.Score ? -1 : 1;
        }

        for (var i = 0; i < 5; i++)
        {
            if (Cards[i] != other.Cards[i])
                return (int)Cards[i].Type < (int)other.Cards[i].Type ? -1 : 1;
        }
        return 0;
    }

    private void CalculateScore()
    {
        var jokerCount = Cards.Count((card) => card.Type == CardType.CardJ);
        var groups = Cards.Where((card) => card.Type != CardType.CardJ).GroupBy((card) => card.Type).ToList();

        if (jokerCount >= 4 || (groups.Any((group) => group.Count() + jokerCount == 5))) Score = HandType.FiveOfAKind; // AAAAA
        else if (jokerCount == 3 || (groups.Count == 2 && groups.Any((group) => group.Count() + jokerCount == 4))) Score = HandType.FourOfAKind; // AAAAB
        else if ((groups.Count == 2)) Score = HandType.FullHouse; // AAABB
        else if ((groups.Any((group) => group.Count() + jokerCount == 3))) Score = HandType.ThreeOfAKind; // AAABC
        else if ((groups.Count == 3 && (jokerCount == 2 || groups.Count((group) => group.Count() + jokerCount == 2) == 2))) Score = HandType.TwoPair; // AABBC
        else if ((groups.Count == 4 && (jokerCount == 1 || groups.Count((group) => group.Count() + jokerCount == 2) == 1))) Score = HandType.OnePair; // AABCD
        else if ((groups.Count == 5)) Score = HandType.HighCard; // ABCDE
        else throw new InvalidOperationException();
    }
}
