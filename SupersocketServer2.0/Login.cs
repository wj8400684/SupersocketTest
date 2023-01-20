using System.Text;
using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;

namespace SupersocketServer;

public sealed class Login : IAsyncCommand<StringPackageInfo>
{
    public ValueTask ExecuteAsync(IAppSession session, StringPackageInfo package)
    {
        return session.SendAsync(Encoding.UTF8.GetBytes($"{package.Key} {package.Body}\r\n"));
    }
}