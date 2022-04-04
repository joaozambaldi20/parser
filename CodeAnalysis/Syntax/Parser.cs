namespace CodeAnalysis.Syntax;

internal sealed class Parser
{

    private SyntaxToken[] _tokenArray;
    private int _position;
    private List<string> _diagnostics = new();
    
    
    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;
        return index < _tokenArray.Length ?_tokenArray[index] : _tokenArray.Last();
    }

    private SyntaxToken Current => Peek(0);
    
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
            var expression = ParseExpression();
            var right = MatchToken(ESyntaxType.ClosingParenthesisToken);

            return new ParenthesizedExpression(left, right, expression);
        }

        var numberToken = MatchToken(ESyntaxType.NumberToken);
        return new LiteralExpressionSyntax(numberToken);
    }

    private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
    {
        ExpressionSyntax left;
        var unaryOperatorPrecedence = Current.Type.GetUnaryOperatorPrecedence();

        if (unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
        {
            var operatorToken = NextToken();
            var operand = ParseExpression(unaryOperatorPrecedence);
            left = new UnaryExpressionSyntax(operatorToken, operand);
        }
        else
        {
            left = ParsePrimaryExpression();
        }

        while (true)
        {
            var precedence = Current.Type.GetBinaryOperatorPrecedence();
            if (precedence == 0 || precedence <= parentPrecedence)
                break;
            
            var operatorToken = NextToken();
            var right = ParseExpression(precedence);
            left = new BinaryExpressionSyntax(left, right, operatorToken);
        }

        return left;
    }

    public Parser(string text)
    {
        var lexer = new Lexer(text);
        var tokens = new List<SyntaxToken>();
        SyntaxToken? token;

        do
        {
            token = lexer.NextToken();
            if (token.Type != ESyntaxType.WhiteSpaceToken)
            {
                tokens.Add(token);
            }

        }
        while(token.Type != ESyntaxType.EOFToken);

        _diagnostics.AddRange(lexer.Diagnostics);
        _tokenArray = tokens.ToArray();
    }
    
    public SyntaxTree Parse()
    {
        var expression = ParseExpression();
        var eofToken = MatchToken(ESyntaxType.EOFToken);
        return new SyntaxTree(_diagnostics, expression, eofToken);
    }
    public IEnumerable<string> Diagnostics => _diagnostics;
}
