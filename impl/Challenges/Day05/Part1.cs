using Challenges.Day05.Models;
using System.Text.RegularExpressions;
using Utility;

namespace Challenges.Day05;

/// <summary>
/// 
/// </summary>
public class Part1 : AdventOfCodeChallenge
{
    private const string Sample = """
                                  seeds: 79 14 55 13

                                  seed-to-soil map:
                                  50 98 2
                                  52 50 48

                                  soil-to-fertilizer map:
                                  0 15 37
                                  37 52 2
                                  39 0 15

                                  fertilizer-to-water map:
                                  49 53 8
                                  0 11 42
                                  42 0 7
                                  57 7 4

                                  water-to-light map:
                                  88 18 7
                                  18 25 70

                                  light-to-temperature map:
                                  45 77 23
                                  81 45 19
                                  68 64 13

                                  temperature-to-humidity map:
                                  0 69 1
                                  1 0 69

                                  humidity-to-location map:
                                  60 56 37
                                  56 93 4
                                  """;

    public Part1()
        : base(5, 1, @"Day05\input.txt")
    {
        SetupTest(Sample, 35);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 388071289);
    }

    protected override long Run(string input)
    {
        var result = 0L;

        var seedsPattern = new Regex(@"(?<seeds>seeds: (?<seedNumbers>(?<seedNumberItem>(\d+)\s?)+))", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
        var seeds = seedsPattern.Match(input) is Match match ? ProcessSeeds(match) : throw new Exception();
        var seedToSoilMapping = GetMapping("seed-to-soil", input);
        var soilToFertilizerMapping = GetMapping("soil-to-fertilizer", input);
        var fertilizerToWaterMapping = GetMapping("fertilizer-to-water", input);
        var waterToLightMapping = GetMapping("water-to-light", input);
        var lightToTemperatureMapping = GetMapping("light-to-temperature", input);
        var temperatureToHumidityMapping = GetMapping("temperature-to-humidity", input);
        var humidityToLocationMapping = GetMapping("humidity-to-location", input);

        var locations = seeds.Select((seed) =>
        {
            var soil = (seedToSoilMapping.FirstOrDefault((map) => map.IsInSourceRange(seed))?.DestinationModifier ?? 0L) + seed;
            var fertilizer = (soilToFertilizerMapping.FirstOrDefault((map) => map.IsInSourceRange(soil))?.DestinationModifier ?? 0L) + soil;
            var water = (fertilizerToWaterMapping.FirstOrDefault((map) => map.IsInSourceRange(fertilizer))?.DestinationModifier ?? 0L) + fertilizer;
            var light = (waterToLightMapping.FirstOrDefault((map) => map.IsInSourceRange(water))?.DestinationModifier ?? 0L) + water;
            var temperature = (lightToTemperatureMapping.FirstOrDefault((map) => map.IsInSourceRange(light))?.DestinationModifier ?? 0L) + light;
            var humidity = (temperatureToHumidityMapping.FirstOrDefault((map) => map.IsInSourceRange(temperature))?.DestinationModifier ?? 0L) + temperature;
            var location = (humidityToLocationMapping.FirstOrDefault((map) => map.IsInSourceRange(humidity))?.DestinationModifier ?? 0L) + humidity;

            return location;
        });

        result = locations.Min();

        return result;
    }

    private static long[] ProcessSeeds(Match match) => match.Groups["seedNumberItem"].Captures.OfType<Capture>().Select((capture) => Convert.ToInt64(capture.Value)).ToArray();
    private static IEnumerable<Mapping> GetMapping(string name, string input)
    {
        var mappingPattern = new Regex($@"{name} map:[\s\r\n](?<mappings>[\d\s]+)[\r\n][\r\n]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
        var match = mappingPattern.Match(input);
        if (!match.Success)
            throw new Exception();

        var results = new List<Mapping>();

        var mappings = match.Groups["mappings"].Value;
        using var reader = new StringReader(mappings);
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            yield return new Mapping(line.Split(' ').Select((item) => Convert.ToInt64(item.Trim())).ToArray());
        }
    }
}
