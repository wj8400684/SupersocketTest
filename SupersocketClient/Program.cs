using System.Net;
using System.Text;
using SuperSocket.Client;
using SuperSocket.ProtoBase;
using SupersocketClient;

var superClient =
    new EasyClient<StringPackageInfo>(new CommandLinePipelineFilter
    {
        Decoder = new DefaultStringPackageDecoder(Encoding.UTF8)
    }).AsClient();

superClient.PackageHandler += OnPackageHandler;

ValueTask OnPackageHandler(EasyClient<StringPackageInfo> sender, StringPackageInfo package)
{
    //Console.WriteLine($"收到消息{package.Key} {package.Body}");
    return ValueTask.CompletedTask;
}

await superClient.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2023));

superClient.StartReceive();

while (true)
{
    await superClient.SendAsync(Encoding.UTF8.GetBytes($"Login test\r\n"));
}