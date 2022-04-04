namespace CodeAnalysis.Syntax;

public sealed class SyntaxToken : SyntaxNode
{
    public override ESyntaxType Type { get; }
    public int Position { get; }
    public string Text { get; }
    public object Value { get; }


    public SyntaxToken(ESyntaxType type, int position, string text, object value)
    {
        Type = type;
        Position = position;
        Text = text;
        Value = value;
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        return Enumerable.Empty<SyntaxNode>();
    }
}
