using SuperSocket.Command;
using SupersocketServer;

namespace SuperSocketFileServer;

[Command(Key = Commands.Data)]
public sealed class Data : IAsyncCommand<FileAppSession, FilePackageInfo>
{
    public ValueTask ExecuteAsync(FileAppSession session, FilePackageInfo package)
    {
        //foreach (var buffer in package.Body)
        //{
        //    session.FileStream.Write(buffer.Span);
        //}

        return ValueTask.CompletedTask;
    }
}
