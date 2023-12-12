using Xunit;

namespace Challenges.Tests.Day11;

public class Part2Tests
{
    private const string Sample = """
                                  ...#......
                                  .......#..
                                  #.........
                                  ..........
                                  ......#...
                                  .#........
                                  .........#
                                  ..........
                                  .......#..
                                  #...#.....
                                  """;

    [Theory]
    [InlineData(Sample, 10, 1030)]
    [InlineData(Sample, 100, 8410)]
    //[InlineData(<INPUT>, 726820169514)]
    public void Test_Part2_Sample(string input, int multiplier, long expected)
    {
        // Arrange
        var sut = new Challenges.Day11.Part2() { Multiplier = multiplier };

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}