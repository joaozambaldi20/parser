namespace CodeAnalysis;

internal sealed class Parser
{

    private SyntaxToken[] _tokenArray;
    private int _position;
    private List<string> _diagnostics = new();

    public Parser(string text)
    {
        var lexer = new Lexer(text);
        var tokens = new List<SyntaxToken>();
        SyntaxToken? token;

        do
        {
            token = lexer.NextToken();

            if (token.Type != ESyntaxType.WhiteSpaceToken)
                tokens.Add(token);

        } while(token.Type != ESyntaxType.EOFToken);

        _diagnostics.AddRange(lexer.Diagnostics);
        _tokenArray = tokens.ToArray();
    }
    
    private SyntaxToken Current => Peek(0);
    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;
        return index < _tokenArray.Length ?_tokenArray[index] : _tokenArray.Last();
    }
    private SyntaxToken NextToken()
    {
        var curr = Current;
        _position++;
        return curr;
    }


    private SyntaxToken MatchToken(ESyntaxType type)
    {
        if (Current.Type == type)
            return NextToken();

        _diagnostics.Add($"ERROR: Unexpected token <{Current.Type}>, expected <{type}>");
        return new SyntaxToken(type, Current.Position, string.Empty, new ManufacturedTokenValue());
    }

    private ExpressionSyntax ParsePrimaryExpression()
    {

        if (Current.Type is ESyntaxType.OpeningParenthesisToken)
        {
            var left = NextToken();
            var expression = ParseTerm();
            var right = MatchToken(ESyntaxType.ClosingParenthesisToken);

            return new ParenthesizedExpression(left, right, expression);
        }


        var numberToken = MatchToken(ESyntaxType.NumberToken);
        return new LiteralExpressionSyntax(numberToken);
    }


    public IEnumerable<string> Diagnostics => _diagnostics;



    private ExpressionSyntax ParseFactor()
    {
        var left = ParsePrimaryExpression();

        while (Current.Type is ESyntaxType.StarToken or ESyntaxType.ForwardSlashToken)
        {
            var operatorToken = NextToken();
            var right = ParsePrimaryExpression();
            left = new BinaryExpressionSyntax(left, right, operatorToken);
        }

        return left;
    }

    public ExpressionSyntax ParseTerm()
    {
        var left = ParseFactor();

        while (Current.Type is ESyntaxType.PlusToken or ESyntaxType.MinusToken)
        {
            var operatorToken = NextToken();
            var right = ParseFactor();
            left = new BinaryExpressionSyntax(left, right, operatorToken);
        }

        return left;
    }


    public SyntaxTree Parse()
    {
        var expression = ParseTerm();
        var eofToken = MatchToken(ESyntaxType.EOFToken);
        return new SyntaxTree(_diagnostics, expression, eofToken);
    }

}
