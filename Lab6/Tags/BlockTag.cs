namespace Lab6.Tags;

public class BlockTag : TagBase
{
    public int Columns { get; set; }
    public int Rows { get; set; }

    public BlockTag() : base(null)
    { }

    public BlockTag(int? width = null, int? height = null) : base(width, height)
    {
    }
}