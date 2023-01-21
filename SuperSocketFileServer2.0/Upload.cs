using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;

namespace SuperSocketFileServer;

[Command(Commands.)]
public sealed class Upload : IAsyncCommand<StringPackageInfo>
{
    public ValueTask ExecuteAsync(IAppSession session, StringPackageInfo package)
    {
        throw new NotImplementedException();
    }
}
