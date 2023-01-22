using SuperSocket.ProtoBase;
using System.Buffers;

namespace SupersocketServer;

public sealed class FilePackageEncode : IPackageEncoder<FilePackageInfo>
{
    public int Encode(IBufferWriter<byte> writer, FilePackageInfo pack)
    {
        throw new NotImplementedException();
    }
}
