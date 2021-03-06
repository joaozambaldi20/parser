namespace CodeAnalysis.Syntax;

public sealed class UnaryExpressionSyntax : ExpressionSyntax
{
    public override ESyntaxKind Kind => ESyntaxKind.UnaryExpression;
    public SyntaxToken OperatorToken { get; }
    public ExpressionSyntax Operand { get; }


    public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand)
    {
        Operand = operand;
        OperatorToken = operatorToken;
    }
    
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return OperatorToken;
        yield return Operand;
    }
}
