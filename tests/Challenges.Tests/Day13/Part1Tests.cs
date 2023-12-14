using Xunit;

namespace Challenges.Tests.Day13;

public class Part1Tests
{
    private const string Sample = """
                                  #.##..##.
                                  ..#.##.#.
                                  ##......#
                                  ##......#
                                  ..#.##.#.
                                  ..##..##.
                                  #.#.##.#.

                                  #...##..#
                                  #....#..#
                                  ..##..###
                                  #####.##.
                                  #####.##.
                                  ..##..###
                                  #....#..#
                                  """;

    [Theory]
    [InlineData(Sample, 405)]
    //[InlineData(<INPUT>, 31265)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day13.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
