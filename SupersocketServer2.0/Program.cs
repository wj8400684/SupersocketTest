using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SupersocketServer;

await SuperSocketHostBuilder.Create<StringPackageInfo, CommandLinePipelineFilter>()
    .UseCommand(options => options.AddCommand<Login>())
    .UseInProcSessionContainer()
    .Build()
    .RunAsync();