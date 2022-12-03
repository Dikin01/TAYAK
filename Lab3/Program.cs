using Lab2;

namespace Lab3;

public static class Program
{
    private static readonly string Directory = Path.GetFullPath(System.IO.Directory.GetCurrentDirectory() + @"../../../../");
    private static readonly List<string> Alphabet = new() { 
        "a", "+", "*", ")", "(",
        "x", "y", "b", 
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
    
    static void Main()
    {
        var fileManager = new FileManager(Path.Combine(Directory, "test2.txt"));
        fileManager.ReadFileByLines();
        var stack = new List<string> { "h0", "E"};

        var input = Console.ReadLine();
        
        while (input is not null or "")
        {
            PushDownAutomate automate = new(fileManager.FileLines, Alphabet);
            Console.WriteLine(automate.IsPAExecutable(input, stack)
                ? "Pushdown automate is executable!"
                : "Pushdown automate is NOT executable!");

            input = Console.ReadLine();
        }    
            
    }
}