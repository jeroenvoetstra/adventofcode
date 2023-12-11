using Xunit;

namespace Challenges.Tests.Day09;

public class Part2Tests
{
    private const string Sample = """
                                  0 3 6 9 12 15
                                  1 3 6 10 15 21
                                  10 13 16 21 30 45
                                  """;

    [Theory]
    [InlineData(Sample, 2)]
    //[InlineData(<INPUT>, 933)]
    public void Test_Part2_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day09.Part2();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
