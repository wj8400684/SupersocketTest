using SuperSocket.ProtoBase;
using System.Buffers;
using System.Reflection.Metadata;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SupersocketServer;

internal sealed class FilePipelineFilter : FixedHeaderPipelineFilter<FilePackageInfo>
{
    private const int HeadSize = sizeof(int);
    private static string ReturnWrap = "\r\n";
    private static string EqualSign = "=";
    private static string Command = "Command";

    public FilePipelineFilter()
        : base(HeadSize)
    {
    }

    protected override FilePackageInfo DecodePackage(ref ReadOnlySequence<byte> buffer)
    {
        var reader = new SequenceReader<byte>(buffer.Slice(HeadSize));

        if (!reader.TryReadLittleEndian(out int bodyLength))
            throw new ProtocolException("读取包内容长度失败");

        if (bodyLength > buffer.Length)
            throw new ArgumentOutOfRangeException(nameof(bodyLength), "包内容长度不正确");

        //获取body内容
        var body = reader.Sequence.Slice(reader.Consumed, bodyLength);

        //转换到字符串
        var str = body.GetString(Encoding.UTF8);

        //获取换行符位置
        int speIndex = str.IndexOf(ReturnWrap);

        if (speIndex < 0)
            throw new ProtocolException("协议错误");

        var parts = str.Split(ReturnWrap, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 2)
            throw new ProtocolException("每次命令至少包括两行");

        FilePackageInfo packageInfo = null;

        for (int i = 0; i < parts.Length; i++)
        {
            var paramArray = parts[i].Split(EqualSign);

            //存在等号
            if (paramArray.Length <= 1)
                continue;

            //超过两个等号，返回失败
            if (paramArray.Length > 2)
                throw new ProtocolException("超过两个等号");

            if (paramArray[0].Equals(Command, StringComparison.CurrentCultureIgnoreCase))
            {
                packageInfo  = new FilePackageInfo
                {
                    Key = paramArray[1],
                    Paramter = new Dictionary<string, string>()
                };
            }
            else
            {
                packageInfo.Paramter.Add(paramArray[0].ToLower(), paramArray[1]);
            }
        }

        if (packageInfo.Key.Equals("Data", StringComparison.InvariantCultureIgnoreCase))
            packageInfo.Body = reader.UnreadSequence;

        return packageInfo;
    }

    protected override int GetBodyLengthFromHeader(ref ReadOnlySequence<byte> buffer)
    {
        var reader = new SequenceReader<byte>(buffer);

        reader.TryReadLittleEndian(out int totalLength);

        return totalLength;
    }
}
