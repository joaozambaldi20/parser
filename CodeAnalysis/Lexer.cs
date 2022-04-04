namespace CodeAnalysis;

public class Lexer
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
    public Token NextToken()
    {
        
        if (_position >= _text.Length)
        {
            return new Token(ESyntaxType.EOF, _position, "", '\0');
        }

        // White Space mode
        if (char.IsWhiteSpace(Current))
        {
            var startPosition = _position;

            while (char.IsWhiteSpace(Current))
                Next();

            var length = _position - startPosition;
            var text = _text.Substring(startPosition, length);
            
            return new Token(ESyntaxType.WhiteSpace, startPosition, text, text);
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

            return new Token(ESyntaxType.NumericLiteral, startPosition, text, value);
        }

        if (Current == '+')
        {
            var token = new Token(ESyntaxType.PlusSign, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '-')
        {
            var token = new Token(ESyntaxType.MinusSign, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '*')
        {
            var token = new Token(ESyntaxType.Asterisk, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '/')
        {
            var token = new Token(ESyntaxType.ForwardSlash, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '(')
        {
            var token = new Token(ESyntaxType.OpeningParenthesis, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == ')')
        {
            var token = new Token(ESyntaxType.ClosingParenthesis, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '[')
        {
            var token = new Token(ESyntaxType.OpeningSquareBracket, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == ']')
        {
            var token = new Token(ESyntaxType.ClosingSquareBracket, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '{')
        {
            var token = new Token(ESyntaxType.OpeningCurlyBrace, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == '}')
        {
            var token = new Token(ESyntaxType.ClosingCurlyBrace, _position, Current.ToString(), Current);
            Next();
            return token;
        }

        if (Current == 'o' && SpyNext() == 'r')
        {
            var token = new Token(ESyntaxType.LogicalOr, _position, "or", "OR");
            _position += 2;
            return token;
        }

        if (Current == 'a' && SpyNext() == 'n' && SpyNext(2) == 'd')
        {
            var token = new Token(ESyntaxType.LogicalAnd, _position, "and", "AND");
            _position += 3;
            return token;
        }

        _diagnostics.Add($"ERROR: bad character in input {Current} at {_position}");
        return new Token(ESyntaxType.BadToken, ++_position, _text.Substring(_position - 1, 1), new ManufacturedTokenValue());
    }

}
