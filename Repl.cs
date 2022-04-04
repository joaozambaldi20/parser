public class Repl
{
    private static void WithColor(ConsoleColor color, Action f)
    {
        var initialColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        f();
        Console.ForegroundColor = initialColor;
    }

    private static void Write(string line, string end = "\n")
    {
        Console.WriteLine(line + end);
    }

    public int Run(Action<string, Action<string, string>, Action<ConsoleColor, Action>> f)
    {
        var quit = false;

        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
            quit = true;
        };

        while (true)
        {
            Console.Write("> ");
            string? input;

            try
            {
                input = Console.ReadLine();
            }
            catch (IOException)
            {
                return -1;
            }


            if (quit == true)
                return 0;

            switch (input)
            {
                case null:
                case "&exit":
                    return 0;
                case "&clear":
                    Console.Clear();
                    break;
                default:
                    f(input, Write, WithColor);
                    break;
            }
        }
    }
}
