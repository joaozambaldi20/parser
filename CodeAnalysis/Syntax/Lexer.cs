namespace CodeAnalysis.Syntax;

internal sealed class Lexer
{
    private readonly string _text;
    private int _position;
    private List<string> _diagnostics = new();


    public Lexer(string text)
    {
        _text = text;
    }


    private char Current
    {
        get => _position < _text.Length ? _text[_position] : '\0';
    }

    private char SpyNext(int n = 1)
    {
        if (_position + n >= _text.Length)
            return '\0';

        return _text[_position + n];
    }

    private void Next()
    {
        _position++;
    }


    public IEnumerable<string> Diagnostics => _diagnostics;
    public SyntaxToken NextToken()
    {
        
        if (_position >= _text.Length)
        {
            return new SyntaxToken(ESyntaxKind.EOFToken, _position, "", '\0');
        }

        // White Space mode
        if (char.IsWhiteSpace(Current))
        {
            var startPosition = _position;

            while (char.IsWhiteSpace(Current))
                Next();

            var length = _position - startPosition;
            var text = _text.Substring(startPosition, length);
            
            return new SyntaxToken(ESyntaxKind.WhiteSpaceToken, startPosition, text, text);
        }

        // Number mode
        if (char.IsDigit(Current))
        {
            var startPosition = _position;

            var tokenText = "";
            var dotCount = 0;

            while (char.IsDigit(Current) || Current == '.' && dotCount == 0)
            {
                if (Current == '.')
                    dotCount++;

                tokenText += Current;
                Next();
            }

            var length = _position - startPosition;
            var text = _text.Substring(startPosition, length);
            var canParse = double.TryParse(text, out double value);

            if (!canParse || value == double.PositiveInfinity || value == double.NegativeInfinity)
            {
                _diagnostics.Add($"The number {text} is not a valid double");
            }

            return new SyntaxToken(ESyntaxKind.NumberToken, startPosition, text, value);
        }

        if (Current == '+')
        {
            var token = new SyntaxToken(ESyntaxKind.PlusToken, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '-')
        {
            var token = new SyntaxToken(ESyntaxKind.MinusToken, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '*')
        {
            var token = new SyntaxToken(ESyntaxKind.StarToken, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '/')
        {
            var token = new SyntaxToken(ESyntaxKind.ForwardSlashToken, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '(')
        {
            var token = new SyntaxToken(ESyntaxKind.OpeningParenthesisToken, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == ')')
        {
            var token = new SyntaxToken(ESyntaxKind.ClosingParenthesisToken, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '[')
        {
            var token = new SyntaxToken(ESyntaxKind.OpeningSquareBracketToken, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == ']')
        {
            var token = new SyntaxToken(ESyntaxKind.ClosingSquareBracketToken, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '{')
        {
            var token = new SyntaxToken(ESyntaxKind.OpeningCurlyBraceToken, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '}')
        {
            var token = new SyntaxToken(ESyntaxKind.ClosingCurlyBraceToken, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == 'o' && SpyNext() == 'r')
        {
            var token = new SyntaxToken(ESyntaxKind.OrToken, _position, "or", "OR");
            _position += 2;
            return token;
        }

        if (Current == 'a' && SpyNext() == 'n' && SpyNext(2) == 'd')
        {
            var token = new SyntaxToken(ESyntaxKind.AndToken, _position, "and", "AND");
            _position += 3;
            return token;
        }

        _diagnostics.Add($"ERROR: bad character in input {Current} at position {_position}");
        return new SyntaxToken(ESyntaxKind.BadToken, ++_position, _text.Substring(_position - 1, 1), new ManufacturedTokenValue());
    }

}
