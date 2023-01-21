
using SuperSocket.ProtoBase;
using System.Buffers;

namespace SupersocketServer;

internal sealed class FirstFilePipelineFilter : FixedSizePipelineFilter<FilePackageInfo>
{
    public FirstFilePipelineFilter() 
        : base(1)
    {
    }

    protected override FilePackageInfo DecodePackage(ref ReadOnlySequence<byte> buffer)
    {
        NextFilter = new FilePipelineFilter();
        return null;
    }
}
