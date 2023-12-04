

using AdventOfCode.Solutions._2023;
using FluentAssertions;

namespace _2023
{
    public class Day04Tests
    {
        private Day04 _sut = new();
        public static IEnumerable<object[]> _sampleDataStage1 = new List<object[]>
        {
            new []{"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53","8"},
            new []{"Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19","2"},
            new []{"Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1","2"},
            new []{"Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83","1"},
            new []{"Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36","0"},
            new []{"Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11","0"}
        };
        public static IEnumerable<object[]> _sampleDataStage2 = new List<object[]>
        {
            new []{"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53","8"},
            new []{"Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19","2"},
            new []{"Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1","2"},
            new []{"Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83","1"},
            new []{"Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36","0"},
            new []{"Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11","0"}
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
            result.Should().Be("13");
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
            result.Should().Be("30");
        }
    }
}