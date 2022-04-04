namespace CodeAnalysis;

public static class Ensure
{
    public static void EnsurESyntaxType(this Token tokentype, ESyntaxType tokenType)
    {
        if (tokentype.Type != tokenType)
        {
            throw new ArgumentException($"Expected token to be {tokenType}");
        }
    }
}
