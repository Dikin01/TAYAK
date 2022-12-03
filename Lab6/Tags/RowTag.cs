namespace Lab6.Tags;

public class RowTag : TagBase
{
    public Valign Valign { get; set; } = Valign.Top;
    public Halign Halign { get; set; } = Halign.Left;
    public int TextColor { get; set; } = 15;
    public int BgColor { get; set; } = 0;

    public RowTag(int? width = null, int? height = null): base(width, height)
    {
    }
}