namespace AdventOfCode.Solutions._2023;

public class Day09 : IChallangeSolution
{
    public string ChallangeName => "2023, Day 09: Mirage Maintenance";

    public bool HasSecondStage => true;

    public string FirstStageInputPath => "Data/2023/09/01.txt";

    public string SecondStageInputPath => FirstStageInputPath;
    public string GetFirstStageResult(IEnumerable<string> input)
    {
        long predictedValuesSum = 0;
        List<List<long>> histories = new();
        foreach (var line in input)
        {
            histories.Add(line.Trim().Split(" ",StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList());
        }
        foreach (var history in histories)
        {
            predictedValuesSum += PredictNextValue(history);
        }
        return predictedValuesSum.ToString();
    }

    private long PredictNextValue(List<long> history)
    {
        Stack<List<long>> predictionSteps = new Stack<List<long>>();
        List<long> currentStep = new List<long>();
        currentStep.AddRange(history);
        while (currentStep.Any(i => i != 0))
        {
            var newStep = new List<long>();
            for (var i = 0; i < currentStep.Count-1; i++)
            {
                newStep.Add(currentStep[i+1]-currentStep[i]);
            }
            currentStep.Add(0);
            predictionSteps.Push(currentStep);
            currentStep = newStep;
        }

        while (predictionSteps.TryPop(out var step))
        {
            step[step.Count - 1] = step[step.Count - 2] + currentStep[currentStep.Count - 1];
            currentStep = step;
        }

        return currentStep[currentStep.Count-1];
    }

    public string GetSecondStageResult(IEnumerable<string> input)
    {
        long predictedValuesSum = 0;
        List<List<long>> histories = new();
        foreach (var line in input)
        {
            histories.Add(line.Trim().Split(" ",StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList());
        }
        foreach (var history in histories)
        {
            predictedValuesSum += PredictPreviousValue(history);
        }
        return predictedValuesSum.ToString();
    }

    private long PredictPreviousValue(List<long> history)
    {
        Stack<List<long>> predictionSteps = new Stack<List<long>>();
        List<long> currentStep = new List<long>();
        currentStep.AddRange(history);
        while (currentStep.Any(i => i != 0))
        {
            var newStep = new List<long>();
            for (var i = 0; i < currentStep.Count-1; i++)
            {
                newStep.Add(currentStep[i+1]-currentStep[i]);
            }
            var addLeading0 = (new List<long>() {0});
            addLeading0.AddRange(currentStep);
            currentStep = addLeading0;
            predictionSteps.Push(currentStep);
            currentStep = newStep;
        }

        while (predictionSteps.TryPop(out var step))
        {
            step[0] = step[1] - currentStep[0];
            currentStep = step;
        }

        return currentStep[0];
    }
}