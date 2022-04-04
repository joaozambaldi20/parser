namespace CodeAnalysis.Syntax;

public sealed class LiteralExpressionSyntax : ExpressionSyntax
{
    public override ESyntaxKind Kind => ESyntaxKind.LiteralExpression;
    public SyntaxToken LiteralToken { get; }

    public LiteralExpressionSyntax(SyntaxToken token)
    {
        LiteralToken = token;
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LiteralToken;
    }
}
