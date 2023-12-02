

using AdventOfCode.Solutions._2023;
using FluentAssertions;

namespace _2023
{
    public class Day02Tests
    {
        private Day02 _sut = new();
        public static IEnumerable<object[]> _sampleDataStage1 = new List<object[]>
        {
            new []{"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "1"},
            new []{"Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue","2"},
            new []{"Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red","0"},
            new []{"Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red","0" },
            new []{"Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green","5" }
        };
        public static IEnumerable<object[]> _sampleDataStage2 = new List<object[]>
        {
            new []{"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "48"},
            new []{"Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue","12"},
            new []{"Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red","1560"},
            new []{"Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red","630" },
            new []{"Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green","36" }
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
            var input = new List<string>();
            foreach (var objects in _sampleDataStage1)
            {
                input.Add((string)objects[0]);
            }
            var result = _sut.GetFirstStageResult(input);
            result.Should().Be("8");
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
            var input = new List<string>();
            foreach (var objects in _sampleDataStage2)
            {
                input.Add((string)objects[0]);
            }
            var result = _sut.GetSecondStageResult(input);
            result.Should().Be("2286");
        }
    }
}