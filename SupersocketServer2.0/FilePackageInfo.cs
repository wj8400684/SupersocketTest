using SuperSocket.ProtoBase;
using System.Buffers;

namespace SupersocketServer;

public sealed class FilePackageInfo : IKeyedPackageInfo<string>
{
    public required string Key { get; set; }

    public Dictionary<string, string> Paramter { get; set; }

    public ReadOnlySequence<byte> Body { get; set; }

    public string ReadValue(string name)
    {
        if (Paramter.TryGetValue(name, out var value))
            return value;

        return null;
    }

    public string ReadFileName()
    {
        const string FileName = "filename";

        if (Paramter.TryGetValue(FileName, out var value))
            return value;

        return null;
    }

    public long ReadFileSize()
    {
        const string FileName = "filesize";

        if (Paramter.TryGetValue(FileName, out var value))
        {
            if (long.TryParse(value, out var length))
            {
                return length;
            }
        }

        return 0;
    }
}
