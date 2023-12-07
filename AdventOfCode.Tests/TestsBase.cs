namespace _2023;

public abstract class TestsBase
{
    public static List<string> GenerateInputList(IEnumerable<object[]> objectArrays)
    {
        var input = new List<string>();
        foreach (var objects in objectArrays)
        {
            input.Add((string) objects[0]);
        }

        return input;
    }
}