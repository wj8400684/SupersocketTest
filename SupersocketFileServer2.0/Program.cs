using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SuperSocketFileServer;
using SupersocketServer;
using System.Reflection;

await SuperSocketHostBuilder.Create<FilePackageInfo, FirstFilePipelineFilter>()
    .UseCommand(options =>
    {
        options.AddCommandAssembly(typeof(Data).GetTypeInfo().Assembly);
    })
    .UseSession<FileAppSession>()
    .UseInProcSessionContainer()
    .ConfigureServices((ctx, cfg) =>
    {
        cfg.AddSingleton<IPackageEncoder<FilePackageInfo>, FilePackageEncode>();
    })
    .Build()
    .RunAsync();