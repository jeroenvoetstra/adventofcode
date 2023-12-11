using Xunit;

namespace Challenges.Tests.Day11;

public class Part1Tests
{
    private const string Sample = """
                                  
                                  """;

    [Theory]
    [InlineData(Sample, 00000)]
    //[InlineData(<INPUT>, 00000)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day11.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
