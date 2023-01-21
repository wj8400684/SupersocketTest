using Microsoft.Extensions.Logging;
using SuperSocket.Command;
using SupersocketServer;
using System.IO;
using System.Text;

namespace SuperSocketFileServer;

[Command(Key = Commands.Upload)]
public sealed class Upload : IAsyncCommand<FileAppSession, FilePackageInfo>
{
    private static readonly string FileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Files");
    private static readonly string Response = $"[Response]\r\nCommand=Upload\r\nCode=0\r\nFileSize=";

    public Upload()
    {
        if (!Directory.Exists(FileDirectory))
            Directory.CreateDirectory(FileDirectory);
    }

    public ValueTask ExecuteAsync(FileAppSession session, FilePackageInfo package)
    {
        var fileName = package.ReadFileName();
        var fileSize = package.ReadFileSize();

        session.LogInformation($"开始传输文件：{fileName} 大小：{fileSize}");

        fileName = Path.Combine(FileDirectory, fileName);

        session.FileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        session.FileStream.Position =  session.FileStream.Length; //文件移到末尾

        return session.SendPackageAsync(Encoding.UTF8.GetBytes(Response + session.FileStream.Length.ToString()));
    }
}
