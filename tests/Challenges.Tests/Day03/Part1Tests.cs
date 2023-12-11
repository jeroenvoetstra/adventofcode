using Xunit;

namespace Challenges.Tests.Day03;

public class Part1Tests
{
    private const string Sample = """
                                  467..114..
                                  ...*......
                                  ..35..633.
                                  ......#...
                                  617*......
                                  .....+.58.
                                  ..592.....
                                  ......755.
                                  ...$.*....
                                  .664.598..
                                  """;

    [Theory]
    [InlineData(Sample, 4361)]
    //[InlineData(<INPUT>, 528799)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day03.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
