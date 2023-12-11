using Xunit;

namespace Challenges.Tests.Day01;

public class Part2Tests
{
    private const string Sample = """
                                  two1nine
                                  eightwothree
                                  abcone2threexyz
                                  xtwone3four
                                  4nineeightseven2
                                  zoneight234
                                  7pqrstsixteen
                                  """;

    [Theory]
    [InlineData(Sample, 281)]
    //[InlineData(<INPUT>, 53268)]
    public void Test_Part2_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day01.Part2();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
