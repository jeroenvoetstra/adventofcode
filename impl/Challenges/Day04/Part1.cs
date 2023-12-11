using Utility;

namespace Challenges.Day04;

/// <summary>
/// 
/// </summary>
public class Part1() : AdventOfCodeChallenge(4, 1, @"Day04\input.txt")
{
    protected override long Run(string input)
    {
        var totalScore = 0L;
        using var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            var cardData = line.Split(':');
            //var cardNumber = Convert.ToInt32(cardData[0].Replace("Card ", ""));
            var numberData = cardData[1].Split('|');
            var winningNumbers = numberData[0].Trim().Split(' ').Where((item) => !string.IsNullOrWhiteSpace(item)).Select((number) => Convert.ToInt32(number.Trim()));
            var drawnNumbers = numberData[1].Trim().Split(' ').Where((item) => !string.IsNullOrWhiteSpace(item)).Select((number) => Convert.ToInt32(number.Trim()));
            var matchingNumberCount = drawnNumbers.Count((number) => winningNumbers.Contains(number));
            var score = matchingNumberCount > 0 ? (int)Math.Pow(2, matchingNumberCount - 1) : 0;

            totalScore += score;
        }

        return totalScore;
    }
}
