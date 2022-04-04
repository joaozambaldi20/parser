namespace CodeAnalysis.Syntax;

public abstract class SyntaxNode
{
    public abstract ESyntaxKind Kind { get; }
    public abstract IEnumerable<SyntaxNode> GetChildren();
}
