public class Visuel
{
    public Visuel()
    {

    }

    public static void PrintCentered(string text)
    {
        int windowWidth = Console.WindowWidth;
        int padding = (windowWidth - text.Length) / 2;
        Console.WriteLine(new string(' ', Math.Max(padding, 0)) + text);
    }

    public static void PrintCenteredColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        PrintCentered(text);
        Console.ResetColor();
    }

    public static void TypewriterCentered(string text, int delay = 40)
    {
        int windowWidth = Console.WindowWidth;
        int padding = (windowWidth - text.Length) / 2;
        Console.Write(new string(' ', Math.Max(padding, 0)));

        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.WriteLine();
    }
}