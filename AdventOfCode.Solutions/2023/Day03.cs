using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions._2023;

public class Day03 : IChallangeSolution
{
    private readonly char[] _symbols =  {'*', '-', '$', '=', '+', '&', '@', '#', '/', '%'};
    private readonly char[] _symbols_withDot =  {'*', '-', '$', '=', '+', '&', '@', '#', '/', '%', '.'};
    private readonly char[] _digits = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
    public string ChallangeName => "2023, Day 3: Gear Ratios";
    public bool HasSecondStage => true;

    public string FirstStageInputPath => "Data/2023/03/01.txt";

    public string SecondStageInputPath => "Data/2023/03/02.txt";
    public string GetFirstStageResult(IEnumerable<string> input)
    {
        int sumOfPartNumbers = 0;
        if (input == null)
        {
            throw new ArgumentException("puzzle input cannot be null");
        }
        var stringArray = input.ToArray();

        var lineStructures = GetLinesWithNeighborhoods(stringArray);
        foreach (var line in lineStructures)
        {
            int[] numbersWithAdjacentSymbol = GetNumbersWithAdjacentSymbols(line);
            sumOfPartNumbers += numbersWithAdjacentSymbol.Sum();
        }

        return sumOfPartNumbers.ToString();
    }

    


    public string GetSecondStageResult(IEnumerable<string> input)
    {
        int sumOfGearRatios = 0;
        if (input == null)
        {
            throw new ArgumentException("puzzle input cannot be null");
        }
        var stringArray = input.ToArray();

        var lineStructures = GetLinesWithNeighborhoods(stringArray);
        foreach (var line in lineStructures)
        {
            Gear[] gears = GetGears(line);
            sumOfGearRatios += gears.Select(g => g.FirstValue*g.SecondValue).Sum();
        }

        return sumOfGearRatios.ToString();
    }

    private Gear[] GetGears(LineAndNeighborhood line)
    {
        var resultGears = new List<Gear>();
        List<GearSymbolWithSmallNeighborhood> potentialGears = GetPotentialGears(line);
        foreach (var potentialGear in potentialGears)
        {
            if (TryGetConfirmedGear(line, potentialGear, out Gear? result) && result.HasValue)
            {
                resultGears.Add(result.Value);
            }
        }

        return resultGears.ToArray();
    }

    private bool TryGetConfirmedGear(LineAndNeighborhood line, GearSymbolWithSmallNeighborhood potentialGear, out Gear? gear)
    {
        if (IsAGear(line, potentialGear))
        {
            gear = GetGear(line, potentialGear.GearIndex);
            return true;
        }
        else
        {
            gear = null;
            return false;
        }
    }

    private Gear GetGear(LineAndNeighborhood line, int gearIndex)
    {
        List<int> foundValues = new();
        foundValues.AddRange(GetNumbersFromIndex(line.currentLine, gearIndex));
        if (line.previousLine != null)
        {
            foundValues.AddRange(GetNumbersFromIndex(line.previousLine, gearIndex));
        }
        if (line.nextLine != null)
        {
            foundValues.AddRange(GetNumbersFromIndex(line.nextLine, gearIndex));
        }

        if (foundValues.Count != 2)
        {
            throw new NotSupportedException(
                $"gear: \r\n{line.previousLine}\r\n{line.currentLine}\r\n{line.nextLine} \r\n has more than 2 parts attached");
        }
        else
        {
            return new Gear()
            {
                FirstValue = foundValues[0],
                SecondValue = foundValues[1]
            };
        }

        
    }

    private IEnumerable<int> GetNumbersFromIndex(string currentLine, int startingIndex)
    {
        List<int> resultList = new();
        if (currentLine == null)
            return resultList;
        if (_symbols_withDot.Contains(currentLine[startingIndex]))
        {
            if (startingIndex > 0 && _digits.Contains(currentLine[startingIndex-1]))
            {
                resultList.Add(FindStartAndReadWholeNumber(currentLine, startingIndex-1));
            }
            if (startingIndex <= currentLine.Length-2 && _digits.Contains(currentLine[startingIndex+1]))
            {
                resultList.Add(FindStartAndReadWholeNumber(currentLine, startingIndex+1));
            }

        }
        else
        {
            //central char is digit
            //   *
            //...123
            //1234..
            //..123.
            //...1..
            resultList.Add(FindStartAndReadWholeNumber(currentLine, startingIndex));
        }

        return resultList;
    }

    private int FindStartAndReadWholeNumber(string currentLine, int startingIndex)
    {
        int currIdx = startingIndex;
        while (currIdx > 0 && _digits.Contains(currentLine[currIdx]))
        {
            currIdx--;
        }

        if(!_digits.Contains(currentLine[currIdx]))
        {
            currIdx++;
        }
        var sb = new StringBuilder();
        while (currIdx < currentLine.Length && _digits.Contains(currentLine[currIdx]))
        {
            sb.Append(currentLine[currIdx++]);
        }

        var resultInt = int.Parse(sb.ToString());
        return resultInt;
    }

    private bool IsAGear(LineAndNeighborhood line, GearSymbolWithSmallNeighborhood potentialGear)
    {
        int NumberCount = 0;
        
        
        List<string> NeighLines = new List<string>();
        
        if(line.previousLine!=null)
            NeighLines.Add(line.previousLine.Substring(potentialGear.NeighborhoodStartIndex, potentialGear.NeighborhoodLength));
        
        NeighLines.Add(line.currentLine.Substring(potentialGear.NeighborhoodStartIndex, potentialGear.NeighborhoodLength));
        
        if(line.nextLine!=null)
            NeighLines.Add(line.nextLine.Substring(potentialGear.NeighborhoodStartIndex, potentialGear.NeighborhoodLength));

        var smallLines = GetLinesWithNeighborhoods(NeighLines.ToArray());
        foreach (var smallLine in smallLines)
        {
            var result = GetNumbersToCheck(smallLine);
            NumberCount += result.Count();
        }

        return NumberCount == 2;

    }

    private List<GearSymbolWithSmallNeighborhood> GetPotentialGears(LineAndNeighborhood line)
    {
        var resultList = new List<GearSymbolWithSmallNeighborhood>();
        var iterator = new PotentialGearIterator(line);
        while (iterator.HasNext)
        {
            var gear = iterator.GetNext();
            if(gear.HasValue)
                resultList.Add(gear.Value);
        }
        return resultList;
    }


    private int[] GetNumbersWithAdjacentSymbols(LineAndNeighborhood line)
    {
        List<int> numbersWithAdjacentSymbols = new();
        var numbersToCheck = GetNumbersToCheck(line);
        foreach (var number in numbersToCheck)
        {
            if (HasAdjecentSymbol(number, line))
            {
                numbersWithAdjacentSymbols.Add(number.Value);
            }
        }
        return numbersWithAdjacentSymbols.ToArray();
    }
    private static List<LineAndNeighborhood> GetLinesWithNeighborhoods(string[] stringArray)
    {
        List<LineAndNeighborhood> lineStructures = new();
        for (int i = 0; i < stringArray.Count(); i++)
        {
            lineStructures.Add(new LineAndNeighborhood(stringArray, i));
        }

        return lineStructures;
    }
    /// <summary>
    /// Used to find the first number, starting from the startIndex
    /// </summary>
    /// <param name="line">line with neighborhood we look in</param>
    /// <param name="startIndex">index to start the search from</param>
    /// <param name="foundNumber">if found, the number and it's start and end index is set</param>
    /// <returns>returns true if there is more string to look thorugh, false if it reached the end of the string</returns>
    /// <exception cref="NotImplementedException"></exception>
    private bool GetNextNumber(LineAndNeighborhood line, int startIndex, out NumberAndIndexes? foundNumber)
    {
        foundNumber = null;
        string currline = line.currentLine;
        int lineLength = currline.Length;
        if (startIndex >= lineLength)
            return false;
        int numberStartIndex = currline.IndexOfAny(_digits, startIndex);
        if (numberStartIndex < 0)
        {
            return false;
        }
        var createdNumber = new NumberAndIndexes();
        createdNumber.StartIndex= numberStartIndex;
        StringBuilder digits = new StringBuilder();
        do
        {
            digits.Append(currline[numberStartIndex]);
            createdNumber.EndIndex = numberStartIndex;
            numberStartIndex++;
        } while (numberStartIndex < lineLength && _digits.Contains(currline[numberStartIndex]));

        createdNumber.Value=int.Parse(digits.ToString());
        foundNumber = createdNumber;
        return numberStartIndex < lineLength;
    }

    private IEnumerable<NumberAndIndexes> GetNumbersToCheck(LineAndNeighborhood line)
    {
        List<NumberAndIndexes> numbersToCheck = new List<NumberAndIndexes>();
        int startIndex = 0;
        NumberAndIndexes? foundNumber = null;
        while (GetNextNumber(line,startIndex, out foundNumber))
        {
            if (foundNumber.HasValue)
            {
                numbersToCheck.Add(foundNumber.Value);
                startIndex = foundNumber.Value.EndIndex+1;
            }
        }
        if (foundNumber.HasValue)
        {
            numbersToCheck.Add(foundNumber.Value);
        }
        return numbersToCheck;
    }

    private bool HasAdjecentSymbol(NumberAndIndexes number, LineAndNeighborhood line)
    {
        int lineLenght = line.currentLine.Length;
        int neighborhoodStartIndex = number.StartIndex == 0 ? 0 : number.StartIndex - 1;
        int neighborhoodLength = number.EndIndex - number.StartIndex + (number.EndIndex == lineLenght - 1 ? 2 : 3);
        var currentNeighborhood = line.currentLine.Substring(neighborhoodStartIndex,neighborhoodLength);
        if (StringContainsSymbols(currentNeighborhood)) return true;

        if (line.previousLine != null)
        {
            var previousNeighborhood = line.previousLine.Substring(neighborhoodStartIndex, neighborhoodLength);
            if (StringContainsSymbols(previousNeighborhood)) return true;
        }

        if (line.nextLine != null)
        {
            var previousNeighborhood = line.nextLine.Substring(neighborhoodStartIndex, neighborhoodLength);
            if (StringContainsSymbols(previousNeighborhood)) return true;
        }

        return false;

    }

    private bool StringContainsSymbols(string @string, char[]?symbols=null)
    {
        if (symbols == null)
        {
            symbols = _symbols;
        }
        return symbols.Any(c => @string.Contains(c));
    }
    private struct LineAndNeighborhood
    {
        public string? previousLine;
        public string currentLine;
        public string? nextLine;
        public LineAndNeighborhood(string[] allStrings, int currentIndex)
        {
            var count = allStrings.Length;
            previousLine = currentIndex == 0 ? null : allStrings[currentIndex - 1];
            currentLine = allStrings[currentIndex];
            nextLine = currentIndex == count - 1 ? null : allStrings[currentIndex + 1];
        }
    }
    private struct Gear
    {
        public int FirstValue;
        public int SecondValue;
    }
    
    private struct NumberAndIndexes
    {
        public int Value;
        public int StartIndex;
        public int EndIndex;
    }
    private struct GearSymbolWithSmallNeighborhood
    {
        public int GearIndex;
        public int NeighborhoodStartIndex;
        public int NeighborhoodLength;
    }

    private class PotentialGearIterator
    {
        private int _lineLength = 0;
        private LineAndNeighborhood _line;
        private int _position =0;
        private readonly char _gearChar;
        private bool CurrentPositionInBounds => _position >= 0 && _position < _lineLength;

        public bool HasNext
        {
            get
            {
                if (CurrentPositionInBounds)
                {
                    return _line.currentLine.IndexOf(_gearChar, _position) > 0;
                }
                return false;
            }
        }

        

        public PotentialGearIterator(LineAndNeighborhood line, char gearChar = '*')
        {
            _line = line;
            _lineLength = line.currentLine.Length;
            _gearChar = gearChar;
        }

        public GearSymbolWithSmallNeighborhood? GetNext()
        {
            if (CurrentPositionInBounds)
            {
                var potentialGearIndex=  _line.currentLine.IndexOf(_gearChar, _position);
                _position = potentialGearIndex + 1;
                if (potentialGearIndex > -1)
                {
                    var potentialGear = new GearSymbolWithSmallNeighborhood();
                    potentialGear.GearIndex = potentialGearIndex;
                    potentialGear.NeighborhoodStartIndex = potentialGear.GearIndex == 0 ? 0 : potentialGear.GearIndex - 1;
                    potentialGear.NeighborhoodLength = 1 + 
                                                       (potentialGear.GearIndex-potentialGear.NeighborhoodStartIndex) +
                                                       (potentialGear.GearIndex+1 < _lineLength?1:0);
                    return potentialGear;
                }
            }
            return null;
        }
    }
}

