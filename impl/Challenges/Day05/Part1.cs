using Challenges.Day05.Models;
using System.Text.RegularExpressions;
using Utility;

namespace Challenges.Day05;

/// <summary>
/// 
/// </summary>
public class Part1() : AdventOfCodeChallenge(5, 1, @"Day05\input.txt")
{
    protected override long Run(string input)
    {
        var seedsPattern = RegularExpressions.SeedsPattern();
        var seeds = seedsPattern.Match(input) is { } match ? ProcessSeeds(match) : throw new Exception();
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

        return locations.Min();
    }

    private static long[] ProcessSeeds(Match match) => match.Groups["seedNumberItem"].Captures.Select((capture) => Convert.ToInt64(capture.Value)).ToArray();
    
    private static IEnumerable<Mapping> GetMapping(string name, string input)
    {
        var mappingPattern = new Regex($@"{name} map:[\s\r\n](?<mappings>[\d\s]+)[\r\n][\r\n]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
        var match = mappingPattern.Match(input);
        if (!match.Success)
            throw new Exception();

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
