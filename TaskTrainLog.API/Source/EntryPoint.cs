using TT.Core;

namespace TT.Log;

internal static class EntryPoint
{
    private static ITTApp _logApp = new TaskTrainLog();

    public static void Main(string[] args)
    {
        _logApp.Build(args);
        _logApp.Run();
    }
}