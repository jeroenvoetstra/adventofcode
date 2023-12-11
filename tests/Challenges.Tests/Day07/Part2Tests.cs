using Xunit;

namespace Challenges.Tests.Day07;

public class Part2Tests
{
    private const string Sample = """
                                  32T3K 765
                                  T55J5 684
                                  KK677 28
                                  KTJJT 220
                                  QQQJA 483
                                  """;

    [Theory]
    [InlineData(Sample, 5905)]
    //[InlineData(<INPUT>, 250665248)]
    public void Test_Part2_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day07.Part2();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
