using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;

namespace SuperSocketFileServer;

public sealed class Login : IAsyncCommand<StringPackageInfo>
{
    public ValueTask ExecuteAsync(IAppSession session, StringPackageInfo package)
    {
        throw new NotImplementedException();
    }
}
