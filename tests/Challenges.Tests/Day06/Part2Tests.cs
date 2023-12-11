using Xunit;

namespace Challenges.Tests.Day06;

public class Part2Tests
{
    private const string Sample = """
                                  Time:      7  15   30
                                  Distance:  9  40  200
                                  """;

    [Theory]
    [InlineData(Sample, 71503)]
    //[InlineData(<INPUT>, 27340847)]
    public void Test_Part2_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day06.Part2();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
