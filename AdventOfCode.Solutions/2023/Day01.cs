using System.Diagnostics;

namespace AdventOfCode.Solutions._2023
{
    public class Day01 : IChallangeSolution
    {
        private Dictionary<string, char> stringToCharMap = new Dictionary<string, char>()
        {
            {"0",'0'},
            {"1",'1'},
            {"2",'2'},
            {"3",'3'},
            {"4",'4'},
            {"5",'5'},
            {"6",'6'},
            {"7",'7'},
            {"8",'8'},
            {"9",'9'},
            {"one",'1'},
            {"two",'2'},
            {"three",'3'},
            {"four",'4'},
            {"five",'5'},
            {"six",'6'},
            {"seven",'7'},
            {"eight",'8'},
            {"nine",'9'}
        };
        public string ChallangeName => "Trebuchet?!";

        public bool HasSecondStage => true;

        public string FirstStageInputPath => "Data/2023/01/01.txt";

        public string SecondStageInputPath => "Data/2023/01/02.txt";

        public string GetFirstStageResult(IEnumerable<string> input)
        {
            int result = 0;
            foreach (var singleString in input)
            {
                char firstDigit = GetFirstDigit(singleString);
                char lastDigit = GetFirstDigit(string.Join("", singleString.Reverse()));
                result += int.Parse($"{firstDigit}{lastDigit}");
            }
            return result.ToString();
        }



        public string GetSecondStageResult(IEnumerable<string> input)
        {
            int result = 0;
            foreach (var singleString in input)
            {
                char firstDigit = GetFirstOrLastDigitOrWord(singleString, firstNotLast: true);
                char lastDigit = GetFirstOrLastDigitOrWord(singleString, firstNotLast: false);
                result += int.Parse($"{firstDigit}{lastDigit}");
            }
            return result.ToString();
        }

        private char GetFirstOrLastDigitOrWord(string singleString, bool firstNotLast)
        {
            Debug.Assert(singleString != null, nameof(singleString) + " != null");
            Dictionary<string, int> foundIndexes = new();
            foreach (var kvp in stringToCharMap)
            {

                foundIndexes.Add(kvp.Key, firstNotLast ? singleString.IndexOf(kvp.Key) : singleString.LastIndexOf(kvp.Key));
            }

            var Found = firstNotLast ?
                foundIndexes.Where(kvp => kvp.Value > -1).MinBy(kvp => kvp.Value) :
                foundIndexes.Where(kvp => kvp.Value > -1).MaxBy(kvp => kvp.Value);

            return stringToCharMap[Found.Key];
        }

        private char GetFirstDigit(string singleString)
        {
            return singleString.First(c => c is >= '0' and <= '9');
        }
    }
}
