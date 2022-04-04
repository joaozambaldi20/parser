namespace CodeAnalysis;

static class Ensure
{
    public static void EnsurESyntaxType(this SyntaxToken tokentype, ESyntaxType tokenType)
    {
        if (tokentype.Type != tokenType)
        {
            throw new ArgumentException($"Expected token to be {tokenType}");
        }
    }
}
