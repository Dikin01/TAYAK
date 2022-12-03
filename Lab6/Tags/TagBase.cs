namespace Lab6.Tags;

public abstract class TagBase
{
    public List<TagBase> Children { get; } = new();
    public int Width { get; } = 80;
    public int Height { get; } = 24;

    public string Text { get; set; } = "";

    public TagBase(int? width = null, int? height = null)
    {
        if (width != null)
        {
            Width = width.Value;
        }
        if (height != null)
        {
            Height = height.Value;
        }
    }
}