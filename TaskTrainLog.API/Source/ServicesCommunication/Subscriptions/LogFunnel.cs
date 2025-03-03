using TT.Core;

namespace TT.Log;

public static class LogFunnel
{
    public static void OnLogMessageRecived(byte[] message) 
    {
        var incoming = LogMessage.Parser.ParseFrom(message);
    }
}
