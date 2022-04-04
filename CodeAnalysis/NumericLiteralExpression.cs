namespace CodeAnalysis;

public sealed class LiteralExpressionSyntax : ExpressionSyntax
{
    public override ESyntaxType Type => ESyntaxType.NumberExpression;
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
