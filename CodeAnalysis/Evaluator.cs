using CodeAnalysis.Syntax;

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
        // Unary Expressions
        if (root is UnaryExpressionSyntax u)
        {
            var operand = EvaluateExpression(u.Operand);
            
            if (u.OperatorToken.Kind is ESyntaxKind.PlusToken)
                return operand;

            if (u.OperatorToken.Kind is ESyntaxKind.MinusToken)
                return -operand;

            throw new Exception($"Unexpected unary operator to be {u.OperatorToken.Kind}");
        }

        // Binnary Expressions
        if (root is BinaryExpressionSyntax b)
        {
            var right = EvaluateExpression(b.Right);
            var left = EvaluateExpression(b.Left);

            switch (b.OperatorToken.Kind)
            {
                case ESyntaxKind.PlusToken:
                    return left + right;
                case ESyntaxKind.MinusToken:
                    return left - right;
                case ESyntaxKind.StarToken:
                    return left * right;
                case ESyntaxKind.ForwardSlashToken:
                    if (right == 0) throw new DivideByZeroException();
                    return left / right;
                default:
                    throw new Exception($"Unexpected binary operator to be {b.OperatorToken.Kind}");
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
