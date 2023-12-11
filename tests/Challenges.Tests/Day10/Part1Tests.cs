using Xunit;

namespace Challenges.Tests.Day10;

public class Part1Tests
{
    private const string Sample = """
                                  .....
                                  .S-7.
                                  .|.|.
                                  .L-J.
                                  .....
                                  """; // 4
    private const string Sample2 = """
                                   ..F7.
                                   .FJ|.
                                   SJ.L7
                                   |F--J
                                   LJ...
                                   """; // 8

    [Theory]
    [InlineData(Sample, 4)]
    [InlineData(Sample2, 8)]
    //[InlineData(<INPUT>, 6909)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day10.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
