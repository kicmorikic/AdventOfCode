

using AdventOfCode.Solutions._2023;
using FluentAssertions;

namespace _2023
{
    public class Day01Tests
    {
        private Day01 _sut = new();
        public static IEnumerable<object[]> _sampleDataStage1 = new List<object[]>
        {
            new []{"1abc2", "12"},
            new []{"pqr3stu8vwx","38"},
            new []{"a1b2c3d4e5f","15"},
            new []{"treb7uchet","77" }
        };
        public static IEnumerable<object[]> _sampleDataStage2 = new List<object[]>
        {
            new []{"two1nine", "29"},
            new []{"eightwothree","83"},
            new []{"abcone2threexyz","13"},
            new []{"xtwone3four","24" },
            new []{"4nineeightseven2","42" },
            new []{"zoneight234","14" },
            new []{"7pqrstsixteen","76" }
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
            result.Should().Be("142");
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
            result.Should().Be("281");
        }
    }
}