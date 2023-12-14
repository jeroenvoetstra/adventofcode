using Xunit;

namespace Challenges.Tests.Day12;

public class Part1Tests
{
    private const string Sample = """
                                  ???.### 1,1,3
                                  .??..??...?##. 1,1,3
                                  ?#?#?#?#?#?#?#? 1,3,1,6
                                  ????.#...#... 4,1,1
                                  ????.######..#####. 1,6,5
                                  ?###???????? 3,2,1
                                  """;

    [Theory]
    [InlineData(Sample, 21)]
    //[InlineData(<INPUT>, 0)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day12.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
