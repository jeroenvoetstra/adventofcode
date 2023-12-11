using Xunit;

namespace Challenges.Tests.Day10;

public class Part2Tests
{
    private const string Sample = """
                                  ...........
                                  .S-------7.
                                  .|F-----7|.
                                  .||.....||.
                                  .||.....||.
                                  .|L-7.F-J|.
                                  .|..|.|..|.
                                  .L--J.L--J.
                                  ...........
                                  """; // 4
    private const string Sample2 = """
                                   .F----7F7F7F7F-7....
                                   .|F--7||||||||FJ....
                                   .||.FJ||||||||L7....
                                   FJL7L7LJLJ||LJ.L-7..
                                   L--J.L7...LJS7F-7L7.
                                   ....F-J..F7FJ|L7L7L7
                                   ....L7.F7||L7|.L7L7|
                                   .....|FJLJ|FJ|F7|.LJ
                                   ....FJL-7.||.||||...
                                   ....L---J.LJ.LJLJ...
                                   """; // 8
    private const string Sample3 = """
                                   FF7FSF7F7F7F7F7F---7
                                   L|LJ||||||||||||F--J
                                   FL-7LJLJ||||||LJL-77
                                   F--JF--7||LJLJ7F7FJ-
                                   L---JF-JLJ.||-FJLJJ7
                                   |F|F-JF---7F7-L7L|7|
                                   |FFJF7L7F-JF7|JL---7
                                   7-L-JL7||F7|L7F-7F7|
                                   L.L7LFJ|||||FJL7||LJ
                                   L7JLJL-JLJLJL--JLJ.L
                                   """; // 10

    [Theory]
    [InlineData(Sample, 4)]
    [InlineData(Sample2, 8)]
    [InlineData(Sample3, 10)]
    //[InlineData(<INPUT>, 461)]
    public void Test_Part2_Sample(string input, long expected)
    {
        // Arrange
        var sut = new Challenges.Day10.Part2();

        // Act
        var result = sut.Run(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
