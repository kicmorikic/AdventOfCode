namespace AdventOfCode.Solutions._2023;

public class Day02 : IChallangeSolution
{
    private Dictionary<string, int> _bagContents = new Dictionary<string, int>()
    {
        {"red", 12},
        {"green", 13},
        {"blue", 14}
    };
    public string ChallangeName => "2023, day 2: Cube Conundrum";
    public bool HasSecondStage => true;
    
    public string FirstStageInputPath => "Data/2023/02/01.txt";

    public string SecondStageInputPath => "Data/2023/02/01.txt";

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
        var sumOfProducts = 0;
        foreach (var gameResult in input)
        {
            Dictionary<string,int> minimumSet = getMinimumSetFromTheGameResult(gameResult);
            int minimumCountsMultiplied = 1;
            foreach (var i in minimumSet)
            {
                minimumCountsMultiplied *= i.Value;
            }

            sumOfProducts += minimumCountsMultiplied;
        }
        return sumOfProducts.ToString();
    }

    private Dictionary<string, int> getMinimumSetFromTheGameResult(string gameResult)
    {
        Dictionary<string, int>  resultDictionary = _bagContents.ToDictionary(pair => pair.Key, pair => 0);
        foreach (var gameStage in GetGameRecordDictionary(gameResult))
        {
            if(resultDictionary[gameStage.Key] < gameStage.Value)
                resultDictionary[gameStage.Key] = gameStage.Value;
        }
        return resultDictionary;
    }

    private bool IsGamePossible(string gameResult, out int i)
    {
        i = 0;
        int gameId = GetGameID(gameResult);
        List<KeyValuePair<string,int>> gameRecord = GetGameRecordDictionary(gameResult);
        foreach (var gameStageKVP in gameRecord)
        {
            if (_bagContents[gameStageKVP.Key] < gameStageKVP.Value)
            {
                return false;
            }
        }

        i = gameId;
        return true;
    }

    private List<KeyValuePair<string,int>> GetGameRecordDictionary(string gameResult)
    {
        List<KeyValuePair<string,int>> gameRecordDictionary = new();
        var gameResultString= gameResult.Split(':')[1];
        var gameStages = gameResultString.Split(new []{',',';'});
        foreach (var stageString in gameStages)
        {
            var stageSplit = stageString.Trim().Split(" ");
            try
            {
                int count = int.Parse(stageSplit[0]);
                string name = stageSplit[1];
                gameRecordDictionary.Add(new KeyValuePair<string, int>(name,count));
            }
            catch
            {
                throw new ArgumentException($"String '{gameResult}' is in a wrong format");
            }
        }
        return gameRecordDictionary;
    }

    private int GetGameID(string gameResult)
    {
        var gameIdentifier= gameResult.Split(':')[0];
        if (int.TryParse(gameIdentifier.Split(' ')[1], out int result))
        {
            return result;
        }
        throw new ArgumentException($"String: {gameResult} is in a wrong format");
    }
}