using Xunit;

namespace Challenges.Tests.Day03;

public class Part2Tests
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
    [InlineData(Sample, 467835)]
    //[InlineData(<INPUT>, 84907174)]
    public void Test_Part2_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day03.Part2();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
