using Xunit;

namespace Challenges.Tests.Day06;

public class Part1Tests
{
    private const string Sample = """
                                  Time:      7  15   30
                                  Distance:  9  40  200
                                  """;

    [Theory]
    [InlineData(Sample, 288)]
    //[InlineData(<INPUT>, 138915)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day06.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
