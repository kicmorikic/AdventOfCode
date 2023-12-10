using AdventOfCode.Solutions._2023;
using FluentAssertions;

namespace _2023;

public class Day09Tests : TestsBase
{
    private Day09 _sut = new();
    public static IEnumerable<object[]> _sampleDataStage1 = new List<object[]>
        {
            new []{"0 3 6 9 12 15","18"},
            new []{"1 3 6 10 15 21","28"},
            new []{"10 13 16 21 30 45","68"},
        };
    public static IEnumerable<object[]> _sampleDataStage2 = new List<object[]>
        {
            new []{"10 13 16 21 30 45","5"},
            new []{"0 3 6 9 12 15","-3"},
            new []{"1 3 6 10 15 21","0"},
        };




    [Theory]
    [MemberData(nameof(_sampleDataStage1))]
    public void FirstStage_SingleStringShouldReturnCorrectResults(string input, string expectedResult)
    {
        var result = _sut.GetFirstStageResult(new[] { input });
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void FirstStage_AllSamplesReturnCorrectValue()
    {
        var input = GenerateInputList(_sampleDataStage1);
        var result = _sut.GetFirstStageResult(input);
        result.Should().Be("114");
    }

    [Theory]
    [MemberData(nameof(_sampleDataStage2))]
    public void SecondStage_SingleStringShouldReturnCorrectResults(string input, string expectedResult)
    {
        var result = _sut.GetSecondStageResult(new[] { input });
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void SecondStage_AllSamplesReturnCorrectValue()
    {
        var input = GenerateInputList(_sampleDataStage2);
        var result = _sut.GetSecondStageResult(input);
        result.Should().Be("2");
    }
}