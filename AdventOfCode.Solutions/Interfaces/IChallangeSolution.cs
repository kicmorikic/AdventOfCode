// See https://aka.ms/new-console-template for more information

namespace AdventOfCode.Solutions
{
    public interface IChallangeSolution
    {
        public string ChallangeName { get; }
        public bool HasSecondStage { get; }
        public string FirstStageInputPath { get; }
        public string SecondStageInputPath { get; }
        string GetFirstStageResult(IEnumerable<string> input);

        string GetSecondStageResult(IEnumerable<string> input);
    }
}