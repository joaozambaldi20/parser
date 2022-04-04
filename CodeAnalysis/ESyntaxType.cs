namespace CodeAnalysis;

public enum ESyntaxType
{
    BadToken,
    EOFToken,
    WhiteSpaceToken,
    NumberToken,

    // mathematical opoerators
    PlusToken,
    MinusToken,
    StarToken,
    ForwardSlashToken,

    // logical operators
    AndToken,
    OrToken,

    OpeningParenthesisToken,
    ClosingParenthesisToken,
    OpeningCurlyBraceToken,
    ClosingCurlyBraceToken,
    OpeningSquareBracketToken,
    ClosingSquareBracketToken,

    
    LiteralExpression,
    UnaryExpression,
    BinaryExpression,
    ParethesizedExpression
}
