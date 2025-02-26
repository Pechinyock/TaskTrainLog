using System.Text;

namespace TT.Log;

public static class LogFunnel
{
    public static void OnLogMessageRecived(byte[] message) 
    {
        var text = Encoding.UTF8.GetString(message);
    }
}
