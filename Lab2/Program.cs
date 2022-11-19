namespace Lab2;

public static class Program
{
    private static readonly string Directory =
        Path.GetFullPath(System.IO.Directory.GetCurrentDirectory() + @"../../../../");

    private static readonly List<char> Alphabet = new()
        { '\\', '/', 'a', '+', 'd', '\"', 'c', 'e', 'f', 'g', 'b', '8', '*', '0', '1', 'h', 'm', 'z' };

    private static void Main()
    {
        var fileManager = new FileManager(Path.Combine(Directory, "var2.txt"));

        fileManager.ReadFileByLines();

        CodeAnalyzer codeAnalyzer = new(Alphabet);
        if (!codeAnalyzer.IsAutomateDescriptionCorrect(fileManager.FileLines!))
            return;

        var automate = new Automate(fileManager.FileLines!);

        Console.WriteLine(automate.IsAutomateDeterministic()
            ? "Automate is Deterministic.\n"
            : "Automate is not Deterministic.\n");

        automate.PrintTransitionFunctions();
        automate.Determination();
        automate.PrintTransitionFunctions();

        while (true)
        {
            var input = Console.ReadLine();
            if (input is null or "")
                break;

            Console.WriteLine(automate.IsExecutableForInputLine(input!) ? "Is executable" : "Is NOT executable");
        }
    }
}