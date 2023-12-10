using AdventOfCode.Solutions._2023;
using FluentAssertions;

namespace _2023;

public class Day10Tests : TestsBase
{
    private Day10 _sut = new();
    public static IEnumerable<object[]> _sampleDataStage1 = new List<object[]>
        {
            new []{".....\r\n.S-7.\r\n.|.|.\r\n.L-J.\r\n.....","4"},
            new []{"-L|F7\r\n7S-7|\r\nL|7||\r\n-L-J|\r\nL|-JF","4"},
            new []{"..F7.\r\n.FJ|.\r\nSJ.L7\r\n|F--J\r\nLJ...","8"},
            new []{"7-F7-\r\n.FJ|7\r\nSJLL7\r\n|F--J\r\nLJ.LJ","8"},
        };
    public static IEnumerable<object[]> _sampleDataStage2 = new List<object[]>
        {
            new []{"",""},
            new []{"",""},
            new []{"",""},
        };




    [Theory]
    [MemberData(nameof(_sampleDataStage1))]
    public void FirstStage_SingleStringShouldReturnCorrectResults(string input, string expectedResult)
    {
        var result = _sut.GetFirstStageResult(new[] { input });
        result.Should().Be(expectedResult);
    }


    [Theory]
    [MemberData(nameof(_sampleDataStage2))]
    public void SecondStage_SingleStringShouldReturnCorrectResults(string input, string expectedResult)
    {
        var result = _sut.GetSecondStageResult(new[] { input });
        result.Should().Be(expectedResult);
    }

    
}