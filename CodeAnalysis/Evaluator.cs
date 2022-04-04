namespace CodeAnalysis;

public sealed class Evaluator
{
    private ExpressionSyntax _root;
    public Evaluator(ExpressionSyntax root)
    {
        _root = root;
    }

    public double Evaluate()
    {
        return EvaluateExpression(_root);
    }

    private double EvaluateExpression(ExpressionSyntax root)
    {
        // Binnary Expressions
        if (root is BinaryExpressionSyntax b)
        {
            var right = EvaluateExpression(b.Right);
            var left = EvaluateExpression(b.Left);

            switch (b.Operator.Type)
            {
                case ESyntaxType.PlusToken:
                    return left + right;
                case ESyntaxType.MinusToken:
                    return left - right;
                case ESyntaxType.StarToken:
                    return left * right;
                case ESyntaxType.ForwardSlashToken:
                    if (right == 0) throw new DivideByZeroException();
                    return left / right;
                default:
                    throw new Exception($"Unexpected binary operator to be {b.Operator.Type}");
            }
        }

        // Numeric Literal Expression
        if (root is LiteralExpressionSyntax n)
        {
            return (double) n.LiteralToken.Value;
        }

        // Parenthersized Expressions
        if (root is ParenthesizedExpression parenthesized)
        {
            return EvaluateExpression(parenthesized.Expression);
        }


        throw new Exception($"Unexpected token {root}");
    }
}
