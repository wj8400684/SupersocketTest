using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace SupersocketServer.Commands
{
    public sealed class Login : CommandBase<MyAppSession, StringRequestInfo>
    {
        public override void ExecuteCommand(MyAppSession session, StringRequestInfo requestInfo)
        {
            session.TrySend($"{requestInfo.Key} {requestInfo.Body}\r\n");
            //Implement your business logic
        }
    }
}