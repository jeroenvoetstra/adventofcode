using Xunit;

namespace Challenges.Tests.Day07;

public class Part1Tests
{
    private const string Sample = """
                                  32T3K 765
                                  T55J5 684
                                  KK677 28
                                  KTJJT 220
                                  QQQJA 483
                                  """;

    [Theory]
    [InlineData(Sample, 6440)]
    //[InlineData(<INPUT>, 250120186)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day07.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
