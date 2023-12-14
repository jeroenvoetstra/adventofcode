using Xunit;

namespace Challenges.Tests.Day12;

public class Part2Tests
{
    private const string Sample = """

                                  """;

    [Theory]
    [InlineData(Sample, 0)]
    //[InlineData(<INPUT>, 0)]
    public void Test_Part2_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day12.Part2();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}