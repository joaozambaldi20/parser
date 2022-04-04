namespace CodeAnalysis.Syntax;

public sealed class SyntaxToken : SyntaxNode
{
    public override ESyntaxKind Kind { get; }
    public int Position { get; }
    public string Text { get; }
    public object Value { get; }


    public SyntaxToken(ESyntaxKind kind, int position, string text, object value)
    {
        Kind = kind;
        Position = position;
        Text = text;
        Value = value;
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        return Enumerable.Empty<SyntaxNode>();
    }
}
