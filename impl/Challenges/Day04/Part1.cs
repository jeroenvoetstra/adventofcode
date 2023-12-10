using Utility;

namespace Challenges.Day04;

/// <summary>
/// 
/// </summary>
public class Part1 : AdventOfCodeChallenge
{
    private const string Sample = """
                                  Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                                  Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
                                  Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
                                  Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
                                  Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
                                  Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
                                  """;

    public Part1()
        : base(4, 1, @"Day04\input.txt")
    {
        SetupTest(Sample, 13);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 21959);
    }

    protected override long Run(string input)
    {
        var totalScore = 0L;
        using (var reader = new StringReader(input))
        {
            while (reader.ReadLine() is { } line)
            {
                var cardData = line.Split(':');
                var cardNumber = Convert.ToInt32(cardData[0].Replace("Card ", ""));
                var numberData = cardData[1].Split('|');
                var winningNumbers = numberData[0].Trim().Split(' ').Where((item) => !string.IsNullOrWhiteSpace(item)).Select((number) => Convert.ToInt32(number.Trim()));
                var drawnNumbers = numberData[1].Trim().Split(' ').Where((item) => !string.IsNullOrWhiteSpace(item)).Select((number) => Convert.ToInt32(number.Trim()));
                var matchingNumberCount = drawnNumbers.Count((number) => winningNumbers.Contains(number));
                var score = matchingNumberCount > 0 ? (int)Math.Pow(2, matchingNumberCount - 1) : 0;

                totalScore += score;
            }
        }

        return totalScore;
    }
}
