using Xunit;

namespace Challenges.Tests.Day08;

public class Part1Tests
{
    private const string Sample = """
                                  RL

                                  AAA = (BBB, CCC)
                                  BBB = (DDD, EEE)
                                  CCC = (ZZZ, GGG)
                                  DDD = (DDD, DDD)
                                  EEE = (EEE, EEE)
                                  GGG = (GGG, GGG)
                                  ZZZ = (ZZZ, ZZZ)
                                  """;
    private const string Sample2 = """
                                   LLR

                                   AAA = (BBB, BBB)
                                   BBB = (AAA, ZZZ)
                                   ZZZ = (ZZZ, ZZZ)
                                   """;

    [Theory]
    [InlineData(Sample, 2)]
    [InlineData(Sample2, 6)]
    //[InlineData(<INPUT>, 18113)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day08.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
