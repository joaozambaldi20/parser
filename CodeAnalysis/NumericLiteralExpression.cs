namespace CodeAnalysis;

public sealed class NumericLiteralExpression : ExpressionSyntax
{
    public override ESyntaxType Type { get => ESyntaxType.NumericLiteralExpression; }
    public Token Token { get; }

    public NumericLiteralExpression(Token token)
    {
        token.EnsurESyntaxType(ESyntaxType.NumericLiteral);
        Token = token;
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Token;
    }
}
