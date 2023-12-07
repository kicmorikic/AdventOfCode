

using AdventOfCode.Solutions._2023;
using FluentAssertions;

namespace _2023
{
    public class Day05Tests: TestsBase
    {
        private Day05 _sut = new();
        public static IEnumerable<object[]> _sampleDataStage1 = new List<object[]>
        {
            new[] {"seeds: 79 14 55 13"},
            new[] {""},
            new[] {"seed-to-soil map:"},
            new[] {"50 98 2"},
            new[] {"52 50 48"},
            new[] {""},
            new[] {"soil-to-fertilizer map:"},
            new[] {"0 15 37"},
            new[] {"37 52 2"},
            new[] {"39 0 15"},
            new[] {""},
            new[] {"fertilizer-to-water map:"},
            new[] {"49 53 8"},
            new[] {"0 11 42"},
            new[] {"42 0 7"},
            new[] {"57 7 4"},
            new[] {""},
            new[] {"water-to-light map:"},
            new[] {"88 18 7"},
            new[] {"18 25 70"},
            new[] {""},
            new[] {"light-to-temperature map:"},
            new[] {"45 77 23"},
            new[] {"81 45 19"},
            new[] {"68 64 13"},
            new[] {""},
            new[] {"temperature-to-humidity map:"},
            new[] {"0 69 1"},
            new[] {"1 0 69"},
            new[] {""},
            new[] {"humidity-to-location map:"},
            new[] {"60 56 37"},
            new[] {"56 93 4"},
        };


        [Fact]
        public void FirstStage_AllSamplesReturnCorrectValue()
        {
            var input = GenerateInputList(_sampleDataStage1);
            var result = _sut.GetFirstStageResult(input);
            result.Should().Be("35");
        }


        [Fact]
        public void SecondStage_AllSamplesReturnCorrectValue()
        {
            var input = GenerateInputList(_sampleDataStage1);
            var result = _sut.GetSecondStageResult(input);
            result.Should().Be("46");
        }

    }
}