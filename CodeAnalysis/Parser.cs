namespace CodeAnalysis;

public class Parser
{

    private Token[] _tokenArray;
    private int _position;
    private List<string> _diagnostics = new();

    public Parser(string text)
    {
        var lexer = new Lexer(text);
        var tokens = new List<Token>();
        Token? token;

        do
        {
            token = lexer.NextToken();

            if (token.Type != ESyntaxType.WhiteSpace)
                tokens.Add(token);

        } while(token.Type != ESyntaxType.EOF);

        _diagnostics.AddRange(lexer.Diagnostics);
        _tokenArray = tokens.ToArray();
    }
    
    private Token Current => Peek(0);
    private Token Peek(int offset)
    {
        var index = _position + offset;
        return index < _tokenArray.Length ?_tokenArray[index] : _tokenArray.Last();
    }
    private Token NextToken()
    {
        var curr = Current;
        _position++;
        return curr;
    }


    private Token MatchSyntax(ESyntaxType type)
    {
        if (Current.Type == type)
            return NextToken();

        _diagnostics.Add($"ERROR: Unexpected token <{Current.Type}>, expected <{type}>");
        return new Token(type, Current.Position, string.Empty, new ManufacturedTokenValue());
    }

    private ExpressionSyntax ParsePrimaryExpression()
    {

        if (Current.Type is ESyntaxType.OpeningParenthesis)
        {
            var left = NextToken();
            var expression = ParseTerm();
            var right = MatchSyntax(ESyntaxType.ClosingParenthesis);

            return new ParenthesizedExpression(left, right, expression);
        }


        var numberToken = MatchSyntax(ESyntaxType.NumericLiteral);
        return new NumericLiteralExpression(numberToken);
    }


    public IEnumerable<string> Diagnostics => _diagnostics;



    private ExpressionSyntax ParseFactor()
    {
        var left = ParsePrimaryExpression();

        while (Current.Type is ESyntaxType.Asterisk or ESyntaxType.ForwardSlash)
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

        while (Current.Type is ESyntaxType.PlusSign or ESyntaxType.MinusSign)
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
        var eofToken = MatchSyntax(ESyntaxType.EOF);
        return new SyntaxTree(_diagnostics, expression, eofToken);
    }

}
