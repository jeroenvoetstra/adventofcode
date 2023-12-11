﻿using Xunit;

namespace Challenges.Tests.Day04;

public class Part1Tests
{
    private const string Sample = """
                                  Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                                  Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
                                  Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
                                  Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
                                  Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
                                  Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
                                  """;

    [Theory]
    [InlineData(Sample, 13)]
    //[InlineData(<INPUT>, 21959)]
    public void Test_Part1_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day04.Part1();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
