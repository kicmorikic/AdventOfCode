using System.Collections.Generic;

namespace AdventOfCode.Solutions._2023;

public class Day05 : IChallangeSolution
{
    public string ChallangeName => "2023, Day 05: If You Give A Seed A Fertilizer";

    public bool HasSecondStage => true;

    public string FirstStageInputPath => "Data/2023/05/01.txt";

    public string SecondStageInputPath => FirstStageInputPath;

    public string GetFirstStageResult(IEnumerable<string> input)
    {
        List<IEnumerable<string>> mapsStrings = SplitStringsOnEmptyLines(input);
        List<SourceToDestinationMap> actualMaps = new List<SourceToDestinationMap>();
        var seedsStrings = mapsStrings[0];
        var seeds = seedsStrings.First()
            .Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries).
            Select(str => long.Parse(str.Trim()));
        foreach (var mapsString in mapsStrings.Skip(1))
        {
            actualMaps.Add(new SourceToDestinationMap(mapsString));
        }

        List<long> locations = new();
        foreach (var seed in seeds)
        {
            long location = seed;
            foreach (var map in actualMaps)
            {
                location = map.MapEntity(location);
            }
            locations.Add(location);
        }

        return locations.Min(i => i).ToString();
    }

    public string GetSecondStageResult(IEnumerable<string> input)
    {
        List<IEnumerable<string>> mapsStrings = SplitStringsOnEmptyLines(input);
        List<SourceToDestinationMap> actualMaps = new List<SourceToDestinationMap>();
        var seedsStrings = mapsStrings[0];
        
        foreach (var mapsString in mapsStrings.Skip(1))
        {
            actualMaps.Add(new SourceToDestinationMap(mapsString));
        }
        
        
        List<long> seedDefinitions = seedsStrings.First()
            .Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries).
            Select(str => long.Parse(str.Trim())).ToList();
        if (seedDefinitions.Count % 2 != 0)
        {
            throw new ArgumentException("Odd number of seed numbers which means we cannot do range definitions");
        }


        long lowestLocationNumber = long.MaxValue;

        List<Task<long>> tasks = new List<Task<long>>();

        for (int i = 0; i < seedDefinitions.Count; i += 2)
        {
              tasks.Add(LowestLocationNumber(seedDefinitions[i], seedDefinitions[i + 1], actualMaps, lowestLocationNumber));
        }

        var result = Task.WhenAll(tasks).GetAwaiter().GetResult();
        
        return result.Min().ToString();
    }

    private static Task<long> LowestLocationNumber(long rangeStart, long rangeLength, List<SourceToDestinationMap> actualMaps, long lowestLocationNumber)
    {
        return Task.Run(() =>
        {
            for (int j = 0; j < rangeLength; j++)
            {
                var location = MapSeedToLocation(rangeStart + j, actualMaps);
                if (lowestLocationNumber > location)
                {
                    lowestLocationNumber = location;
                }
            }

            return lowestLocationNumber;
        });
    }

    private static long MapSeedToLocation(long seed, List<SourceToDestinationMap> actualMaps)
    {
        long location = seed;
        foreach (var map in actualMaps)
        {
            location = map.MapEntity(location);
        }

        return location;
    }

    

    private List<IEnumerable<string>> SplitStringsOnEmptyLines(IEnumerable<string> input)
    {
        List<IEnumerable<string>> result = new();
        foreach (var entry in String.Join("\r\n", input.Select(s => s.Trim()))
                     .Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries))
        {
            result.Add(entry.Split("\r\n"));
        }
        return result;
    }

    private class SourceToDestinationMap
    {
        public SourceToDestinationMap(IEnumerable<string> mapRangeLines)
        {
            var rangeLines = mapRangeLines as string[] ?? mapRangeLines.ToArray();
            MapName = rangeLines[0].Replace(":", "");
            var sourceToDestArray = MapName.Split(" ")[0].Split("-");
            if (sourceToDestArray.Length == 3)
            {
                SourceName = sourceToDestArray[0];
                DestinatinoName = sourceToDestArray[2];
            }
            else
            {
                throw new ArgumentException("Map in wrong format. Expected correct header");
            }
            foreach (var line in rangeLines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                mappingRanges.Add(new SourceToDestinationMappingRange(line));
            }
        }

        public string MapName { get; }
        public string SourceName { get; }
        public string DestinatinoName { get; }
        private List<SourceToDestinationMappingRange> mappingRanges = new();

        public long MapEntity(long id)
        {
            var mapping = mappingRanges.FirstOrDefault(mapping => mapping.IsSourceEntityInRange(id));
            return mapping == null ? id : mapping.MapEntity(id);
        }
    }

    private class SourceToDestinationMappingRange
    {
        public long SourceRangeStartInclusive { get; }
        public long SourceRangeEndExclusive { get; }
        public long SourceRangeLength { get; }
        public long DestinationRangeStartInclusive { get; }
        public long DestinationRangeEndExclusive { get; }
        public long DestinationRangeLength => SourceRangeLength;

        public SourceToDestinationMappingRange(string line)
        {
            var items = line.Trim().Split(" ");
            if (items.Length != 3)
                throw new ArgumentException(
                    $"Map line: {line} is in a wrong format. Expected 3 ints separated by spaces");

            DestinationRangeStartInclusive = long.Parse(items[0]);
            SourceRangeStartInclusive = long.Parse(items[1]);
            SourceRangeLength = long.Parse(items[2]);
            SourceRangeEndExclusive = SourceRangeStartInclusive + SourceRangeLength;
            DestinationRangeEndExclusive = DestinationRangeStartInclusive + DestinationRangeLength;
        }

        public bool IsSourceEntityInRange(long id) => SourceRangeStartInclusive <= id && id < SourceRangeEndExclusive;

        public long MapEntity(long id) => DestinationRangeStartInclusive + (id - SourceRangeStartInclusive);
    }
}