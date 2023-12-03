

using AdventOfCode.Solutions._2023;
using FluentAssertions;

namespace _2023
{
    public class Day03Tests
    {
        private Day03 _sut = new();

        private List<string> sampleInput = new List<string>()
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };

        [Theory]
        [InlineData(
            "..........",
            "..........",
            "..........",
            "0")]
        [InlineData(
            "..........",
            "..........",
            null,
            "0")]
        [InlineData(
            "..........",
            null,
            null,
            "0")]
        [InlineData(
            "..........",
            "....12....",
            "..........",
            "0")]
        [InlineData(
            "....1.....",
            "..........",
            null,
            "0")]
        [InlineData(
            "....1.....",
            null,
            null,
            "0")]
        public void FirstStage_NoSymbolAdjacent(string? line01, string? line02, string? line03, string expectedResult)
        {
            List<string> input = new();
            if (line01 != null)
                input.Add(line01);
            if (line02 != null)
                input.Add(line02);
            if (line03 != null)
                input.Add(line03);
                
            var actualResult = _sut.GetFirstStageResult(input);
            actualResult.Should().Be(expectedResult);
        }
        [Theory]
        [InlineData(
            "..........",
            "....11*...",
            "..........",
            "11")]
        [InlineData(
            "..........",
            "....11....",
            "......*...",
            "11")]
        [InlineData(
            "..........",
            "....11....",
            ".....*....",
            "11")]
        [InlineData(
            "..........",
            "....11....",
            "....*.....",
            "11")]
        [InlineData(
            "..........",
            "....11....",
            "...*......",
            "11")]
        [InlineData(
            "..........",
            "...*11....",
            "..........",
            "11")]
        [InlineData(
            "...*......",
            "....11....",
            "..........",
            "11")]
        [InlineData(
            "....*.....",
            "....11....",
            "..........",
            "11")]
        [InlineData(
            ".....*....",
            "....11....",
            "..........",
            "11")]
        [InlineData(
            "1*........",
            "..........",
            "..........",
            "1")]
        [InlineData(
            "1.........",
            ".*........",
            "..........",
            "1")]
        [InlineData(
            "1.........",
            "*.........",
            "..........",
            "1")]
        [InlineData(
            "11........",
            ".*........",
            "..........",
            "11")]
        [InlineData(
            "11........",
            "..*.......",
            "..........",
            "11")]
        [InlineData(
            "11*.......",
            "..........",
            "..........",
            "11")]
        [InlineData(
            "..........",
            "..........",
            "........*1",
            "1")]
        [InlineData(
            "..........",
            "........*.",
            ".........1",
            "1")]
        [InlineData(
            "..........",
            ".........*",
            ".........1",
            "1")]
        [InlineData(
            "..........",
            "...1*1....",
            "..........",
            "2")]
        public void FirstStage_SingleInteger(string? line01, string? line02, string? line03, string expectedResult)
        {
            List<string> input = new();
            if (line01 != null)
                input.Add(line01);
            if (line02 != null)
                input.Add(line02);
            if (line03 != null)
                input.Add(line03);
                
            var actualResult = _sut.GetFirstStageResult(input);
            actualResult.Should().Be(expectedResult);
        }
        [Fact]
        public void FirstStage_sampleDataShouldReturnExpectedResult()
        {
            var actualResult = _sut.GetFirstStageResult(sampleInput);
            actualResult.Should().Be("4361");
        }
        [Theory]
        [InlineData(
            "..........",
            "..........",
            "..........",
            "0")]
        [InlineData(
            ".156",
            ".*..",
            "....",
            "0")]
        [InlineData(
            "11*2......",
            "..........",
            "..........",
            "22")]
        [InlineData(
            "11111111..",
            "........*.",
            ".........2",
            "22222222")]
        [InlineData(
            ".........4",
            ".........*",
            ".........5",
            "20")]
        [InlineData(
            "...2*3....",
            "..........",
            "..........",
            "6")]
        [InlineData(
            "...3......",
            "....*.....",
            ".....4....",
            "12")]
        public void SecondStage_SingleInteger(string? line01, string? line02, string? line03, string expectedResult)
        {
            List<string> input = new();
            if (line01 != null)
                input.Add(line01);
            if (line02 != null)
                input.Add(line02);
            if (line03 != null)
                input.Add(line03);
                
            var actualResult = _sut.GetSecondStageResult(input);
            actualResult.Should().Be(expectedResult);
        }
        [Fact]
        public void SecondStage_sampleDataShouldReturnExpectedResult()
        {
            var actualResult = _sut.GetSecondStageResult(sampleInput);
            actualResult.Should().Be("467835");
        }


    }
}