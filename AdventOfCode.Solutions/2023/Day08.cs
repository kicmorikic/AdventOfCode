namespace AdventOfCode.Solutions._2023;

public class Day08 : IChallangeSolution
{
    private readonly string startingPoint = "AAA";
    private readonly string goal = "ZZZ";

    public string ChallangeName => "Day 8: Haunted Wasteland";
    public bool HasSecondStage => true;
    
    public string FirstStageInputPath => "Data/2023/08/01.txt";

    public string SecondStageInputPath => FirstStageInputPath;
    public string GetFirstStageResult(IEnumerable<string> input)
    {
        var queue = ParseStringToQueue(input.First());
        var map = ParseInputMap(input.Skip(2));
        int stepCount = 0;
        string currentLocation = startingPoint;
        while (queue.TryDequeue(out char currentDirection) && !currentLocation.Equals(goal))
        {
            stepCount++;
            queue.Enqueue(currentDirection);
            var possibilities = map[currentLocation];
            currentLocation = currentDirection == 'L' ? possibilities.Left : possibilities.Right;
        }

        return stepCount.ToString();
    }

    

    public string GetSecondStageResult(IEnumerable<string> input)
    {
        var queue = ParseStringToQueue(input.First());
        var map = ParseInputMap(input.Skip(2));
        int stepCount = 0;
        var currentLocations = map.Where(kvp => kvp.Key.EndsWith("A")).Select(kvp => kvp.Key).ToArray();
        var cycles = currentLocations.Select(s => new Cycle()).ToArray();
        while (queue.TryDequeue(out char currentDirection) && cycles.Any(c => !c.CycleFound))
        {
            stepCount++;
            queue.Enqueue(currentDirection);
            for (var i = 0; i < currentLocations.Length; i++)
            {
                if (cycles[i].CycleFound)
                {
                    continue;
                }
                var possibility = map[currentLocations[i]];
                currentLocations[i] = currentDirection == 'L' ? possibility.Left : possibility.Right;
                if (currentLocations[i].EndsWith("Z"))
                {
                    if (cycles[i].firstZ == null)
                    {
                        cycles[i].firstZ = currentLocations[i];
                        cycles[i].numberOfStepsToGetToFirstZ = stepCount;
                    }
                    else
                    {
                        if (cycles[i].firstZ.Equals(currentLocations[i]))
                        {
                            cycles[i].numberOfStepsToGetToFirstZAgain =
                                stepCount - cycles[i].numberOfStepsToGetToFirstZ;
                        }
                    }
                }
            }
            
            
        }

        long leastCommonMultiplierOfCycles =
            cycles.Aggregate(1L, (acc, cycle) => (acc * cycle.numberOfStepsToGetToFirstZAgain)/Gcd(acc,cycle.numberOfStepsToGetToFirstZAgain));
        return leastCommonMultiplierOfCycles.ToString();
    }

    static long Gcd(long a, long b)
    {
        while (a != b)
        {
            if (a > b)
                a -= b;
            else
                b -= a;
        }
        return a;
    }


    static Dictionary<string, Possibility> ParseInputMap(IEnumerable<string> inputLines)
    {
        Dictionary<string, Possibility> dictionary = new Dictionary<string, Possibility>();

        foreach (string line in inputLines)
        {
            string[] parts = line.Split('=');

            if (parts.Length == 2)
            {
                string key = parts[0].Trim();
                string value = parts[1].Trim();

                if (value.StartsWith("(") && value.EndsWith(")"))
                {
                    value = value.Trim('(', ')');
                    string[] options = value.Split(',').Select(option => option.Trim()).ToArray();

                    if (options.Length == 2)
                    {
                        Possibility possibility = new Possibility(options[0], options[1]);
                        dictionary[key] = possibility;
                    }
                }
            }
        }

        return dictionary;
    }

    internal class Cycle
    {
        public string? firstZ = null;
        public long numberOfStepsToGetToFirstZ = 0;
        public long numberOfStepsToGetToFirstZAgain = 0;

        public bool CycleFound =>
            firstZ != null && numberOfStepsToGetToFirstZ != 0 && numberOfStepsToGetToFirstZAgain != 0;
    }

    static Queue<char> ParseStringToQueue(string input)
    {
        Queue<char> queue = new Queue<char>();

        foreach (char character in input)
        {
            if (character == 'R' || character == 'L')
            {
                queue.Enqueue(character);
            }
        }

        return queue;
    }
    internal class Possibility
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public Possibility(string left, string right)
        {
            Left = left;
            Right = right;
        }
    }
}

