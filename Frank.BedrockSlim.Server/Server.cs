using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Frank.BedrockSlim.Server;

public static class Server
{
    public static IHost CreateTcpServer<THandler>(int port, Action<HostBuilderContext, IServiceCollection>? configureServices = null)
        where THandler : class, IConnectionHandler
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(configureServices ?? ((context, services) => { }))
            .UseTcpConnectionHandler<THandler>(port)
            .Build();
    }
}