using System.Collections.Generic;

namespace AdventOfCode.Solutions._2023;

public class Day06 : IChallangeSolution
{
    public string ChallangeName => "2023, Day 06: Wait For It";

    public bool HasSecondStage => true;

    public string FirstStageInputPath => "Data/2023/06/01.txt";

    public string SecondStageInputPath => "Data/2023/06/02.txt";

    public string GetFirstStageResult(IEnumerable<string> input)
    {
        var strings = input.ToArray();
        var times = strings[0].Split(":", StringSplitOptions.TrimEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
        var distances = strings[1].Split(":", StringSplitOptions.TrimEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
        if (times.Count() != distances.Count())
        {
            throw new ArgumentException("Number of times is not equal to number of distances");
        }
        var races = new List<Race>();
        for (long i = 0; i < times.Count(); i++)
        {
            races.Add(new Race(times[i],distances[i]));
        }


        return races.Select(r => r.GetNumberOfWaysToWin()).Aggregate(1L,((acc, nextVal) => acc*nextVal)).ToString();
    }

    public string GetSecondStageResult(IEnumerable<string> input)
    {
        var strings = input.ToArray();
        var times = strings[0].Split(":", StringSplitOptions.TrimEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
        var distances = strings[1].Split(":", StringSplitOptions.TrimEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
        if (times.Count() != distances.Count())
        {
            throw new ArgumentException("Number of times is not equal to number of distances");
        }
        var races = new List<Race>();
        for (long i = 0; i < times.Count(); i++)
        {
            races.Add(new Race(times[i],distances[i]));
        }


        return races.Select(r => r.GetNumberOfWaysToWin()).Aggregate(1L,((acc, nextVal) => acc*nextVal)).ToString();
    }

    private struct Race
    {
        public long Time { get;  }
        public long Distance { get; }
        public Race(long time, long distance)
        {
            Time = time;
            Distance = distance;
        }

        public long GetNumberOfWaysToWin()
        {
            long numberOfWaysToWin = 0;
            for (long i = 0; i < Time; i++)
            {
                numberOfWaysToWin += i * (Time - i) > Distance ? 1 : 0;
            }
            return numberOfWaysToWin;
        }
    }

    
    
}