using CodeAnalysis;

var repl = new Repl();

repl.Run((input, write, withColor) =>
{

    var parser = new Parser(input);
    var syntaxTree = parser.Parse();

    foreach (var diagnostic in parser.Diagnostics)
    {
        withColor(ConsoleColor.Red, () =>
        {
            write(diagnostic, "\n");
        });
    }    

    withColor(ConsoleColor.DarkGray, () => write(syntaxTree.PrettyPrint(), "\n"));


    if (!parser.Diagnostics.Any())
    {
        var evaluator = new Evaluator(syntaxTree.Root);
        write($"R: {evaluator.Evaluate()}", "\n");
    }

});
