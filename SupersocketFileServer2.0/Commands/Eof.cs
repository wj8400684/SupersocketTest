using Microsoft.Extensions.Logging;
using SuperSocket.Command;
using SupersocketServer;
using System.Text;

namespace SuperSocketFileServer;

[Command(Key = Commands.Eof)]
public sealed class Eof : IAsyncCommand<FileAppSession, FilePackageInfo>
{
    private static readonly ReadOnlyMemory<byte> Response = Encoding.UTF8.GetBytes($"[Response]\r\nCommand=Eof");

    public ValueTask ExecuteAsync(FileAppSession session, FilePackageInfo package)
    {
        session.LogInformation("传输完成");

        session.FileStream.Close();
        session.FileStream = null;

        return session.SendPackageAsync(Response);
    }
}
