using SuperSocket.Command;
using SupersocketServer;
using System.Text;

namespace SuperSocketFileServer;

[Command(Key = Commands.Login)]
public sealed class Login : IAsyncCommand<FileAppSession, FilePackageInfo>
{
    private static readonly ReadOnlyMemory<byte> Response = Encoding.UTF8.GetBytes($"[Response]\r\nCommand=Login\r\nCode=0");

    public ValueTask ExecuteAsync(FileAppSession session, FilePackageInfo package)
    {
        return session.SendPackageAsync(Response);
    }
}
