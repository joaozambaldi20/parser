namespace CodeAnalysis;

public sealed class BinaryExpressionSyntax : ExpressionSyntax
{
    public override ESyntaxType Type { get => ESyntaxType.BinaryExpression; }
    public ExpressionSyntax Left { get; }
    public ExpressionSyntax Right { get; }
    public Token Operator { get; }


    public BinaryExpressionSyntax(ExpressionSyntax left, ExpressionSyntax right, Token @operator)
    {
        Left = left;
        Right = right;
        Operator = @operator;
    }
    
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Left;
        yield return Operator;
        yield return Right;
    }
}