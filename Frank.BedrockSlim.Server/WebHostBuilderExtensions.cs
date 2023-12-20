using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.BedrockSlim.Server;


public static class WebHostBuilderExtensions
{
    public static IWebHostBuilder UseTcpConnectionHandler<TProcessor>(this IWebHostBuilder builder, int port)
        where TProcessor : class, IConnectionProcessor
    {
        builder.ConfigureServices(services =>
        {
            services.AddTransient<IConnectionProcessor, TProcessor>();
        });

        builder.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(port, listenOptions =>
            {
                listenOptions.UseConnectionHandler<TcpConnectionHandler>();
            });
        });

        return builder;
    }
}