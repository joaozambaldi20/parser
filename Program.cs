using CodeAnalysis;
using CodeAnalysis.Syntax;

var repl = new Repl();
var showTree = false;

repl.Run((input, write, withColor) =>
{

    if (input == "&showtree")
    {
        showTree = !showTree;
        return;
    }

    var parser = new Parser(input);
    var syntaxTree = parser.Parse();

    foreach (var diagnostic in parser.Diagnostics)
    {
        withColor(ConsoleColor.Red, () =>
        {
            write(diagnostic, "\n");
        });
    }    

    if (showTree)
    {
        withColor(ConsoleColor.DarkGray, () => write(syntaxTree.PrettyPrint(), string.Empty));
    }
  
    if (!parser.Diagnostics.Any())
    {
        var evaluator = new Evaluator(syntaxTree.Root);
        write($"{evaluator.Evaluate()}", "\n");
    }

});
