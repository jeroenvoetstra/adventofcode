using Xunit;

namespace Challenges.Tests.Day08;

public class Part2Tests
{
    private const string Sample = """
                                  LR
                                  
                                  11A = (11B, XXX)
                                  11B = (XXX, 11Z)
                                  11Z = (11B, XXX)
                                  22A = (22B, XXX)
                                  22B = (22C, 22C)
                                  22C = (22Z, 22Z)
                                  22Z = (22B, 22B)
                                  XXX = (XXX, XXX)
                                  """;

    [Theory]
    [InlineData(Sample, 6)]
    //[InlineData(<INPUT>, 12315788159977)]
    public void Test_Part2_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day08.Part2();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
