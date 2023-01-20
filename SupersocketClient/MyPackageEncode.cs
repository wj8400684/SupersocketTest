using System.Buffers;
using System.Text;
using SuperSocket.ProtoBase;

namespace SupersocketClient;

internal sealed class MyPackageEncode : IPackageEncoder<StringPackageInfo>
{
    private static readonly ReadOnlyMemory<byte> Crlf = Encoding.UTF8.GetBytes("\r\n");
    private static readonly ReadOnlyMemory<byte> Span = Encoding.UTF8.GetBytes(" ");

    public int Encode(IBufferWriter<byte> writer, StringPackageInfo pack)
    {
        var totalCount = writer.Write(pack.Key, Encoding.UTF8);

        writer.Write(Span.Span);

        totalCount += Span.Length;
        
        foreach (var parameter in pack.Parameters)
        {
            totalCount += writer.Write(parameter, Encoding.UTF8);
            writer.Write(Span.Span);
            totalCount += Span.Length;
        }

        writer.Write(Crlf.Span);

        return totalCount += Crlf.Length;
    }
}