namespace CodeAnalysis.Syntax;

public sealed class ParenthesizedExpression : ExpressionSyntax
{
    public SyntaxToken OpeningParenthesisToken { get; }
    public SyntaxToken ClosingParenthesisToken { get; }
    public ExpressionSyntax Expression { get; }


    public ParenthesizedExpression(
        SyntaxToken openingParenthesisToken, SyntaxToken closingParenthesisToken, ExpressionSyntax expression)
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
