using Challenges.Day04.Models;
using Utility;

namespace Challenges.Day04;

/// <summary>
/// 
/// </summary>
public class Part2() : AdventOfCodeChallenge(4, 2, @"Day04\input.txt")
{
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
