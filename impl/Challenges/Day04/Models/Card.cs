namespace Challenges.Day04.Models;

public record Card(int CardNumber, int[] WinningNumbers, int[] DrawnNumbers)
{
    public int WinCount => DrawnNumbers.Count((number) => WinningNumbers.Contains(number));
}
