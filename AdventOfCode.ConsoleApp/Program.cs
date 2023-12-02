// See https://aka.ms/new-console-template for more information
using AdventOfCode.Solutions;
using AdventOfCode.Solutions._2023;
using System.Text.RegularExpressions;

List<IChallangeSolution> challanges = new();
challanges.Add(new Day01());
challanges.Add(new Day02());

int i = 0;
foreach (var challange in challanges)
{
    Console.WriteLine($"{i}) {challange.ChallangeName}");
    if (challange.HasSecondStage)
    {
        Console.WriteLine($"{i}a) {challange.ChallangeName}");
    }
    i++;
}
Console.WriteLine("Last is Default");
var def = $"{challanges.Count - 1}";
def += challanges.Last().HasSecondStage ? "a" : "";
var inputRegex = new Regex("([0-9]+)([aA]{0,1})", RegexOptions.Compiled);
Console.WriteLine("Select a challange to run: ");
string chosenResult = Console.ReadLine();
if (string.IsNullOrWhiteSpace(chosenResult))
{
    chosenResult = def;
}
var regexResult = inputRegex.Match(chosenResult);
if (regexResult.Success)
{
    if (int.TryParse(regexResult.Groups[1].Value, out int number))
    {
        try
        {
            var choice = challanges[number];
            if (string.IsNullOrWhiteSpace(regexResult.Groups[2].Value))
            {
                Console.WriteLine($"{choice.ChallangeName} first challange result is: ");
                Console.WriteLine(choice.GetFirstStageResult(GetFileContents($"../../../{choice.FirstStageInputPath}")));
            }
            else
            {
                Console.WriteLine($"{choice.ChallangeName} second challange result is: ");
                Console.WriteLine(choice.GetSecondStageResult(GetFileContents($"../../../{choice.SecondStageInputPath}")));
            }
        }
        catch
        {
            Console.WriteLine($"No challange registered with \"{chosenResult}\"");
            return;
        }


    }
}

IEnumerable<string> GetFileContents(string path)
{
    return File.ReadAllLines(path);
}