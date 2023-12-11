using Xunit;

namespace Challenges.Tests.Day01;

public class Part1Tests
{
    private const string Sample = """
                                  1abc2
                                  pqr3stu8vwx
                                  a1b2c3d4e5f
                                  treb7uchet
                                  """;

    [Theory]
    [InlineData(Sample, 142)]
    //[InlineData(<INPUT>, 53080)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day01.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
