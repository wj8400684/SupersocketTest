using SuperSocket.Channel;
using SuperSocket.ProtoBase;
using SuperSocket.Server;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SupersocketServer;

public sealed class FileAppSession : AppSession
{
    private readonly IPackageEncoder<FilePackageInfo> _packageEncoder;

    public FileAppSession(IPackageEncoder<FilePackageInfo> packageEncoder)
    {
        _packageEncoder = packageEncoder;
    }

    public FileStream FileStream { get; set; }

    internal ValueTask SendPackageAsync(ReadOnlyMemory<byte> response)
    {
        if (Channel.IsClosed)
            return ValueTask.CompletedTask;

        return Channel.SendAsync((writer) =>
        {
            var totalLength = sizeof(int) + response.Length;
            var bodyLength = response.Length;

            //写入总大小
            var buffer = writer.GetSpan(sizeof(int));
            MemoryMarshal.Write(buffer, ref totalLength);
            writer.Advance(4);

            //写入命令大小
            buffer = writer.GetSpan(sizeof(int));
            MemoryMarshal.Write(buffer, ref bodyLength);
            writer.Advance(4);

            //写入命令内容
            writer.Write(response.Span);
        });
    }

    internal ValueTask SendPackageAsync(FilePackageInfo packageInfo)
    {
        if (Channel.IsClosed)
            return ValueTask.CompletedTask;

        return Channel.SendAsync(_packageEncoder, packageInfo);
    }

    protected override ValueTask OnSessionClosedAsync(CloseEventArgs e)
    {
        FileStream?.Close();
        FileStream?.Dispose();

        return ValueTask.CompletedTask;
    }
}