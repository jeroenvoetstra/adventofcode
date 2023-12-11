using Xunit;

namespace Challenges.Tests.Day11;

public class Part2Tests
{
    private const string Sample = """

                                  """;

    [Theory]
    [InlineData(Sample, 00000)]
    //[InlineData(<INPUT>, 00000)]
    public void Test_Part2_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day11.Part2();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}