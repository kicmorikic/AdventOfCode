namespace AdventOfCode.Solutions._2023;

public class Day04 : IChallangeSolution
{
    public string ChallangeName => "Day 4: Scratchcards";
    public bool HasSecondStage => true;
    public string FirstStageInputPath => "Data/2023/04/01.txt";
    public string SecondStageInputPath => "Data/2023/04/01.txt";
    public string GetFirstStageResult(IEnumerable<string> input)
    {
        int sumOfGameScores = 0;
        foreach (var inputLine in input)
        {
            var parsedData = new GameData(inputLine);
            var intersection = parsedData.NumbersThatWon;
            int currentScore = 0;
            foreach (var item in intersection)
            {
                if (currentScore == 0)
                    currentScore++;
                else
                    currentScore *= 2;
            }
            sumOfGameScores += currentScore;
        }
        return sumOfGameScores.ToString();
    }

    public string GetSecondStageResult(IEnumerable<string> input)
    {
        Dictionary<int,GameData> IdAndGameDatas = new Dictionary<int,GameData>();
        foreach (var inputLine in input)
        {
            var gameData= new GameData(inputLine);
            IdAndGameDatas.Add(gameData.GameId,gameData);
        }

        foreach (var kvp in IdAndGameDatas)
        {
            var numberOfTicketsWon = kvp.Value.NumbersThatWon.Count;
            for (int copyProcessingIndex = 0; copyProcessingIndex < kvp.Value.CouponCount; copyProcessingIndex++)
            {
                for (int i = 1; i <= numberOfTicketsWon; i++)
                {
                    if (IdAndGameDatas.TryGetValue(kvp.Key + i, out var gameData))
                    {
                        gameData.CouponCount++;
                    }
                    else
                    {
                        Console.WriteLine($"tried to increment ticket that does not exist: Card {kvp.Key + i} because Card {kvp.Value.GameId} won {kvp.Value.NumbersThatWon} coupons");
                    }
                }
            }
        }

        return IdAndGameDatas.Sum(kvp => kvp.Value.CouponCount).ToString();
    }

    private class GameData
    {
        public int CouponCount { get; set; }
        public ISet<int> NumbersThatWon
        {
            get
            {
                var intersection = new HashSet<int>( this.WinningNumbers.ToArray());
                intersection.IntersectWith(this.DraftedNumbers);
                return intersection;
            }
        }

        public int GameId { get; }
        private string GameName { get;}
        private readonly ISet<int> WinningNumbers = new HashSet<int>();
        private readonly ISet<int> DraftedNumbers = new HashSet<int>();
        public GameData(string inputLine)
        {
            CouponCount = 1;
            if(string.IsNullOrWhiteSpace(inputLine)) throw new ArgumentNullException(nameof(inputLine));
            if (!inputLine.Contains('|') || !inputLine.Contains(':') || inputLine.Length<15)
                throw new ArgumentException(
                    $"String: '{inputLine}' is not in the correct format, expected it to contain '|', ':' and be longer than 15 characters",
                    nameof(inputLine));

            var gameNameAndRest = inputLine.Split(':');
            if (gameNameAndRest.Length != 2)
            {
                throw new ArgumentException(
                    $"String: '{inputLine}' is not in the correct format. It has more colons than required",
                    nameof(inputLine));
            }
            GameName = gameNameAndRest[0].Trim();
            GameId = int.Parse(GameName.Replace("Card", "").Trim());
            var rest = gameNameAndRest[1].Trim();
            var winningNumbersAndDraftedNumbers = rest.Split('|');
            if (winningNumbersAndDraftedNumbers.Length != 2)
            {
                throw new ArgumentException(
                    $"String: '{inputLine}' is not in the correct format. It has more pipes than required",
                    nameof(inputLine));
            }

            var winningNumbersString = winningNumbersAndDraftedNumbers[0].Trim().Replace("  "," ");
            var draftedNumbersString = winningNumbersAndDraftedNumbers[1].Trim().Replace("  "," ");
            foreach (var numberStr in winningNumbersString.Split(" "))
            {
                WinningNumbers.Add(int.Parse(numberStr));
            }
            foreach (var numberStr in draftedNumbersString.Split(" "))
            {
                DraftedNumbers.Add(int.Parse(numberStr));
            }

        }
    }

    
}