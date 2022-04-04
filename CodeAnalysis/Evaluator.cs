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
                case ESyntaxType.PlusSign:
                    return left + right;
                case ESyntaxType.MinusSign:
                    return left - right;
                case ESyntaxType.Asterisk:
                    return left * right;
                case ESyntaxType.ForwardSlash:
                    if (right == 0) throw new DivideByZeroException();
                    return left / right;
                default:
                    throw new Exception($"Unexpected binary operator to be {b.Operator.Type}");
            }
        }

        // Numeric Literal Expression
        if (root is NumericLiteralExpression n)
        {
            return (double) n.Token.Value;
        }

        // Parenthersized Expressions
        if (root is ParenthesizedExpression parenthesized)
        {
            return EvaluateExpression(parenthesized.Expression);
        }


        throw new Exception($"Unexpected token {root}");
    }
}
