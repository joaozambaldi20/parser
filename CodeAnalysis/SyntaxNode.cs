namespace CodeAnalysis;

public abstract class SyntaxNode
{
    public abstract ESyntaxType Type { get; }
    public abstract IEnumerable<SyntaxNode> GetChildren();
}
