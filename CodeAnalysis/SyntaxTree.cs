namespace CodeAnalysis;

public sealed class SyntaxTree
{
    public ExpressionSyntax Root { get; }
    public Token EndOfFileToken { get; }
    public IReadOnlyList<string> Diagnostics { get; }

    public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, Token eof)
    {
        Root = root;
        EndOfFileToken = eof;
        Diagnostics = diagnostics.ToArray();
    }

    public string PrettyPrint()
    {
        var result = "";
        PrettyPrint((str) => result += str, (str) => result += $"{str}\n", Root);
        return result;
    }

    private void PrettyPrint(Action<string> write, Action<string> writeLine, SyntaxNode node, string indent = "", bool last = true)
    {
        var marker = last ? "└──" : "├──";

        write($"{indent}{marker} {node.Type}");

        if (node is Token t && t.Value is not null)
        {
            write($" {t.Value}");
        }

        writeLine(string.Empty);

        indent += (last ? "    " : "|   ");

        var lc = node.GetChildren().LastOrDefault();

        foreach (var child in node.GetChildren())
        {
            PrettyPrint(write, writeLine, child, indent, child == lc);
        }
    }

}
