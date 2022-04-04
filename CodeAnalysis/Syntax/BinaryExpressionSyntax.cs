namespace CodeAnalysis.Syntax;

public sealed class BinaryExpressionSyntax : ExpressionSyntax
{
    public override ESyntaxType Type => ESyntaxType.BinaryExpression;
    public ExpressionSyntax Left { get; }
    public ExpressionSyntax Right { get; }
    public SyntaxToken OperatorToken { get; }


    public BinaryExpressionSyntax(ExpressionSyntax left, ExpressionSyntax right, SyntaxToken operatorToken)
    {
        Left = left;
        Right = right;
        OperatorToken = operatorToken;
    }
    
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Left;
        yield return OperatorToken;
        yield return Right;
    }
}
