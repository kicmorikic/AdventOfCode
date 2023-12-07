

using AdventOfCode.Solutions._2023;
using FluentAssertions;

namespace _2023
{
    public class Day06Tests: TestsBase
    {
        private Day06 _sut = new();
        public static IEnumerable<object[]> _sampleDataStage1 = new List<object[]>
        {
            new []{"Time:      7|Distance:  9", "4"},
            new []{"Time:      15|Distance:  40", "8"},
            new []{"Time:      30|Distance:  200", "9"},
        };
        [Theory]
        [MemberData(nameof(_sampleDataStage1))]
        public void FirstStage_SingleStringShouldReturnCorrectResults(string input, string expectedResult)
        {
            var result = _sut.GetFirstStageResult(input.Split("|"));
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void FirstStage_AllSamplesReturnCorrectValue()
        {
            var input = new List<string>() {
                "Time:      7  15   30", 
                "Distance:  9  40  200"

            };
            var result = _sut.GetFirstStageResult(input);
            result.Should().Be("288");
        }


        //[Fact]
        public void SecondStage_AllSamplesReturnCorrectValue()
        {
            var input = GenerateInputList(_sampleDataStage1);
            var result = _sut.GetSecondStageResult(input);
            result.Should().Be("46");
        }

    }
}