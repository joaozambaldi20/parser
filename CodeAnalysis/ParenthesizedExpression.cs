namespace CodeAnalysis;

public sealed class ParenthesizedExpression : ExpressionSyntax
{
    public Token OpeningParenthesisToken { get; }
    public Token ClosingParenthesisToken { get; }
    public ExpressionSyntax Expression { get; }


    public ParenthesizedExpression(
        Token openingParenthesisToken, Token closingParenthesisToken, ExpressionSyntax expression)
    {
        OpeningParenthesisToken = openingParenthesisToken;
        ClosingParenthesisToken = closingParenthesisToken;
        Expression = expression;
    }

    public override ESyntaxType Type => ESyntaxType.ParethesizedExpression;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return OpeningParenthesisToken;
        yield return Expression;
        yield return ClosingParenthesisToken;
    }
}
