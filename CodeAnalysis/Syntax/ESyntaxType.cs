namespace CodeAnalysis.Syntax;

public enum ESyntaxKind
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
