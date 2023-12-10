using Challenges.Day05.Models;
using System.Text.RegularExpressions;
using Utility;

namespace Challenges.Day05;

/// <summary>
/// 
/// </summary>
public class Part2 : AdventOfCodeChallenge
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

    public Part2()
        : base(5, 2, @"Day05\input.txt")
    {
        SetupTest(Sample, 46);

        // Use correct answer for input as test. // TODO: be sure to remove when using different input
        SetupTest(Input, 84206669);
    }

    protected override long Run(string input)
    {
        var result = 0L;

        var seedsPattern = RegularExpressions.SeedsPattern();
        var seeds = seedsPattern.Match(input) is Match match ? ProcessSeeds(match) : throw new Exception();

        var seedToSoilMapping = GetMapping("seed-to-soil", input);
        var soilToFertilizerMapping = GetMapping("soil-to-fertilizer", input);
        var fertilizerToWaterMapping = GetMapping("fertilizer-to-water", input);
        var waterToLightMapping = GetMapping("water-to-light", input);
        var lightToTemperatureMapping = GetMapping("light-to-temperature", input);
        var temperatureToHumidityMapping = GetMapping("temperature-to-humidity", input);
        var humidityToLocationMapping = GetMapping("humidity-to-location", input);

        var soils = seeds.SelectMany((seed) => seed.Intersect(seedToSoilMapping)).Concat(seeds.SelectMany((seed) => seedToSoilMapping.Except(seedToSoilMapping)));//.Dump("soils");
        var fertilizers = soils.SelectMany((soil) => soil.Intersect(soilToFertilizerMapping)).Concat(soils.SelectMany((soil) => soil.Except(soilToFertilizerMapping)));//.Dump("fertilizers");
        var waters = fertilizers.SelectMany((fertilizer) => fertilizer.Intersect(fertilizerToWaterMapping)).Concat(fertilizers.SelectMany((fertilizer) => fertilizer.Except(fertilizerToWaterMapping)));//.Dump("waters");
        var lights = waters.SelectMany((water) => water.Intersect(waterToLightMapping)).Concat(waters.SelectMany((water) => water.Except(waterToLightMapping)));//.Dump("lights");
        var temperatures = lights.SelectMany((light) => light.Intersect(lightToTemperatureMapping)).Concat(lights.SelectMany((light) => light.Except(lightToTemperatureMapping)));//.Dump("temperatures");
        var humidities = temperatures.SelectMany((temperature) => temperature.Intersect(temperatureToHumidityMapping)).Concat(temperatures.SelectMany((temperature) => temperature.Except(temperatureToHumidityMapping)));//.Dump("humidities");
        var locations = humidities.SelectMany((humidity) => humidity.Intersect(humidityToLocationMapping)).Concat(humidities.SelectMany((humidity) => humidity.Except(humidityToLocationMapping)));//.Dump("locations");

        result = locations.Min((item) => item.Min);

        return result;
    }

    private static IEnumerable<Models.Range> ProcessSeeds(Match match)
    {
        var items = match.Groups["seedNumberItem"].Captures.OfType<Capture>().Select((capture) => Convert.ToInt64(capture.Value)).ToArray();
        return Enumerable.Range(0, items.Length / 2).Select((i) => new Models.Range(items[i * 2], items[i * 2] + items[(i * 2) + 1]));
    }

    private static IEnumerable<Models.Range> GetMapping(string name, string input)
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
            var data = line.Split(' ').Select((item) => Convert.ToInt64(item.Trim())).ToArray();
            var destination = data[0];
            var source = data[1];
            var length = data[2];
            yield return new Models.Range(source, source + length, destination - source);
        }
    }
}
