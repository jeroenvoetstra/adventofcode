using Challenges.Day04.Models;
using Utility;

namespace Challenges.Day04;

/// <summary>
/// 
/// </summary>
public class Part2 : AdventOfCodeChallenge
{
    private const string Sample = """
                                  Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                                  Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
                                  Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
                                  Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
                                  Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
                                  Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
                                  """;

    public Part2()
        : base(4, 2, @"Day04\input.txt")
    {
        SetupTest(Sample, 30);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 5132675);
    }

    protected override long Run(string input)
    {
        var originalStack = new List<Card>();
        using (var reader = new StringReader(input))
        {
            while (reader.ReadLine() is { } line)
            {
                var cardData = line.Split(':');
                var cardNumber = Convert.ToInt32(cardData[0].Replace("Card ", ""));
                var numberData = cardData[1].Split('|');
                var winningNumbers = numberData[0].Trim().Split(' ').Where((item) => !string.IsNullOrWhiteSpace(item)).Select((number) => Convert.ToInt32(number.Trim())).ToArray();
                var drawnNumbers = numberData[1].Trim().Split(' ').Where((item) => !string.IsNullOrWhiteSpace(item)).Select((number) => Convert.ToInt32(number.Trim())).ToArray();
                originalStack.Add(new Card(cardNumber, winningNumbers, drawnNumbers));
            }
        }

        var currentIndex = 0;
        var cardNumbers = new List<int>(originalStack.Select((card) => card.CardNumber));
        while (true)
        {
            var currentCard = originalStack[currentIndex];
            var duplicateCount = cardNumbers.Count((number) => number == currentCard.CardNumber);
            for (var j = 1; j <= duplicateCount; j++)
            {
                for (var i = 1; i <= currentCard.WinCount; i++)
                {
                    var newIndex = currentIndex + i;
                    if (newIndex >= originalStack.Count)
                        break;
                    cardNumbers.Add(originalStack[newIndex].CardNumber);
                }
            }

            currentIndex++;
            if (currentIndex >= originalStack.Count)
                break;
        }

        return cardNumbers.Count;
    }
}
