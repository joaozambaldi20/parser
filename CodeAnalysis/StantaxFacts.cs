namespace CodeAnalysis;

internal static class SyntaxFacts
{
    public static int GetBinaryOperatorPrecedence(this ESyntaxType type)
    {
        switch (type)
        {
            case ESyntaxType.PlusToken:
            case ESyntaxType.MinusToken:
                return 1;
            case ESyntaxType.StarToken:
            case ESyntaxType.ForwardSlashToken:
                return 2;
            default:
                return 0;
        }
    }
}
