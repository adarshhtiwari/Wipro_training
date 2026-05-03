namespace Eval1;

public class Loggers
{
    static string LogFile = "log.txt";

    // Log normal message
    public static void Log(string message)
    {
        try
        {
            string log = "[LOG] " + DateTime.Now + " - " + message;
            Console.WriteLine(log);
            File.AppendAllText(LogFile, log + "\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Logging failed: " + ex.Message);
        }
    }

    // Log error message
    public static void LogError(string message)
    {
        try
        {
            string log = "[ERROR] " + DateTime.Now + " - " + message;
            Console.WriteLine(log);
            File.AppendAllText(LogFile, log + "\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Logging failed: " + ex.Message);
        }
    }
}