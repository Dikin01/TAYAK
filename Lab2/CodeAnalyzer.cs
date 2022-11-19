namespace Lab2;

public class CodeAnalyzer : IAnalyzer
{
    private readonly List<char> _alphabet;

    public CodeAnalyzer(List<char> alphabet)
    {
        _alphabet = alphabet;
    }

    public bool IsAutomateDescriptionCorrect(List<string> inputLines)
    {
        return inputLines.All(IsDescriptionLineCorrect);
    }

    public bool IsDescriptionLineCorrect(string analyzingLine)
    {
        var i = 0;

        ReadState(analyzingLine, ref i);

        if (analyzingLine.Length == i)
        {
            Console.WriteLine("Incorrect syntax. Enter ',' and symbol of transmission and output state. ");
            return false;
        }

        if (analyzingLine[i] != ',')
        {
            Console.WriteLine($"Incorrect symbol - {analyzingLine[i]}");
            return false;
        }

        i++;

        if (analyzingLine.Length == i)
        {
            Console.WriteLine("Incorrect syntax. Enter symbol of transmission and output state.");
            return false;
        }

        if (!_alphabet.Contains(analyzingLine[i]))
        {
            Console.WriteLine($"Incorrect symbol - {analyzingLine[i]}");
            return false;
        }

        i++;

        if (analyzingLine.Length == i)
        {
            Console.WriteLine("Incorrect syntax. Enter '=state'");
            return false;
        }


        if (analyzingLine[i] != '=')
        {
            Console.WriteLine($"Incorrect symbol - {analyzingLine[i]}");
            return false;
        }

        i++;

        if (analyzingLine.Length == i)
        {
            Console.WriteLine("Incorrect syntax. Enter output state");
            return false;
        }

        ReadState(analyzingLine, ref i);

        if (i >= analyzingLine.Length)
            return true;

        Console.WriteLine($"Incorrect symbol - {analyzingLine[i]}");
        return false;
    }

    public string ReadState(string analyzingLine, ref int index)
    {
        var state = "";
        while (char.IsDigit(analyzingLine[index]) || char.IsLetter(analyzingLine[index]))
        {
            state += analyzingLine[index];
            index++;
            if (analyzingLine.Length == index) break;
        }

        return state;
    }
}