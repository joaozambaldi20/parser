namespace CodeAnalysis.Syntax;

internal static class SyntaxFacts
{
    public static int GetUnaryOperatorPrecedence(this ESyntaxKind kind)
    {
        switch (kind)
        {
            case ESyntaxKind.PlusToken:
            case ESyntaxKind.MinusToken:
                return 3;
            default:
                return 0;
        }
    }

    public static int GetBinaryOperatorPrecedence(this ESyntaxKind kind)
    {
        switch (kind)
        {
            case ESyntaxKind.PlusToken:
            case ESyntaxKind.MinusToken:
                return 1;
            case ESyntaxKind.StarToken:
            case ESyntaxKind.ForwardSlashToken:
                return 2;
            default:
                return 0;
        }
    }
}
