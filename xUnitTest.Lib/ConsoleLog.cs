using System;
public class ConsoleLog : ILog
{
    public void Write(string msg)
    {
        Console.WriteLine(msg);
    }
}