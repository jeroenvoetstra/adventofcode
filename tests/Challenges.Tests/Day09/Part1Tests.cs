using Xunit;

namespace Challenges.Tests.Day09;

public class Part1Tests
{
    private const string Sample = """
                                  0 3 6 9 12 15
                                  1 3 6 10 15 21
                                  10 13 16 21 30 45
                                  """;

    [Theory]
    [InlineData(Sample, 114)]
    //[InlineData(<INPUT>, 1666172641)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day09.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
