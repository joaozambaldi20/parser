namespace CodeAnalysis;

public enum ESyntaxType
{
    BadToken,
    WhiteSpace,
    OpeningParenthesis,
    ClosingParenthesis,
    OpeningCurlyBrace,
    ClosingCurlyBrace,
    OpeningSquareBracket,
    ClosingSquareBracket,
    PlusSign,
    MinusSign,
    LogicalOr,
    LogicalAnd,
    Asterisk,
    ForwardSlash,
    NumericLiteral,
    EOF,
    
    NumericLiteralExpression,
    BinaryExpression,
    ParethesizedExpression
}
