namespace PO_102_Zadanie5;

public static class ConsoleHelpers
{
    public static ConsoleColor ScannerColor = ConsoleColor.Yellow;
    public static ConsoleColor PrinterColor = ConsoleColor.Green;
    public static ConsoleColor DeviceColor = ConsoleColor.Cyan;
    
    public static void WriteLine(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}