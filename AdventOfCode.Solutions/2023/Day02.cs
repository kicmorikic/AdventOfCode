namespace AdventOfCode.Solutions._2023;

public class Day02 : IChallangeSolution
{
    private Dictionary<string, int> BagContents = new Dictionary<string, int>()
    {
        {"red", 12},
        {"green", 13},
        {"blue", 14}
    };
    public string ChallangeName => "2023, day 2: Cube Conundrum";
    public bool HasSecondStage => false;
    
    public string FirstStageInputPath => "Data/2023/02/01.txt";

    public string SecondStageInputPath => "Data/2023/02/02.txt";

    public string GetFirstStageResult(IEnumerable<string> input)
    {
        int sumOfPossibleIds = 0;
        foreach (var gameResult in input)
        {
            if (IsGamePossible(gameResult, out int id))
            {
                sumOfPossibleIds += id;
            }
        }

        return sumOfPossibleIds.ToString();
    }

    public string GetSecondStageResult(IEnumerable<string> input)
    {
        throw new NotImplementedException();
    }

    private bool IsGamePossible(string gameResult, out int i)
    {
        i = 0;
        return false;
    }
}