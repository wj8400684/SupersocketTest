using SuperSocket.Command;
using SupersocketServer;
using System.Text;

namespace SuperSocketFileServer;

[Command(Key = Commands.Active)]
public sealed class Active : IAsyncCommand<FileAppSession, FilePackageInfo>
{
    private static readonly ReadOnlyMemory<byte> Response = Encoding.UTF8.GetBytes($"[Response]\r\nCommand=Active\r\nCode=0");

    public ValueTask ExecuteAsync(FileAppSession session, FilePackageInfo package)
    {
        return session.SendPackageAsync(Response);
    }
}
