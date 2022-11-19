namespace Lab2;
public class FileManager
{
    private readonly FileInfo _fileInfo;
    public List<string>? FileLines { get; private set; }

    public FileManager(string path) => _fileInfo = new FileInfo(path);

    public void ReadFileByLines()
    {
        var stringArray = File.ReadAllLines(_fileInfo.FullName);
        FileLines = new List<string>(stringArray);
    }
}

