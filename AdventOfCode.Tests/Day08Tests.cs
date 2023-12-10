using AdventOfCode.Solutions._2023;
using FluentAssertions;

namespace _2023;

public class Day08Tests : TestsBase
{
    private Day08 _sut = new Day08();
    public static IEnumerable<object[]> _sampleDataStage1 = new List<object[]>
    {
        
        new[] {"RL"},
        new[] {""},
        new[] {"AAA = (BBB, CCC)"},
        new[] {"BBB = (DDD, EEE)"},
        new[] {"CCC = (ZZZ, GGG)"},
        new[] {"DDD = (DDD, DDD)"},
        new[] {"EEE = (EEE, EEE)"},
        new[] {"GGG = (GGG, GGG)"},
        new[] {"ZZZ = (ZZZ, ZZZ)"},
    };
    public static IEnumerable<object[]> _sampleData2Stage1 = new List<object[]>
    {
        
        new[] {"LLR"},
        new[] {""},
        new[] {"AAA = (BBB, BBB)"},
        new[] {"BBB = (AAA, ZZZ)"},
        new[] {"ZZZ = (ZZZ, ZZZ)"},
    };
    public static IEnumerable<object[]> _sampleData1Stage2 = new List<object[]>
    {
        new[]{"LR"},
        new[]{""},
        new[]{"11A = (11B, XXX)"},
        new[]{"11B = (XXX, 11Z)"},
        new[]{"11Z = (11B, XXX)"},
        new[]{"22A = (22B, XXX)"},
        new[]{"22B = (22C, 22C)"},
        new[]{"22C = (22Z, 22Z)"},
        new[]{"22Z = (22B, 22B)"},
        new[]{"XXX = (XXX, XXX)" },
    };


    

    [Fact]
    public void FirstStage_SimpleCaseTwoSteps()
    {
        var input = GenerateInputList(_sampleDataStage1);
        var result = _sut.GetFirstStageResult(input);
        result.Should().Be("2");
    }

    [Fact]
    public void FirstStage_MoreComplexCaseLoopInstructions()
    {
        var input = GenerateInputList(_sampleData2Stage1);
        var result = _sut.GetFirstStageResult(input);
        result.Should().Be("6");
    }


    [Fact]
    public void SecondStage_AllSamplesReturnCorrectValue()
    {
        var input = GenerateInputList(_sampleData1Stage2);
        var result = _sut.GetSecondStageResult(input);
        result.Should().Be("6");
    }
}