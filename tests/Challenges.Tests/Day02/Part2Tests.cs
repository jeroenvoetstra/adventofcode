using Xunit;

namespace Challenges.Tests.Day02;

public class Part2Tests
{
    private const string Sample = """
                                  Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                                  Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                                  Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                                  Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                                  Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                                  """;

    [Theory]
    [InlineData(Sample, 2286)]
    //[InlineData(<INPUT>, 72706)]
    public void Test_Part2_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day02.Part2();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
