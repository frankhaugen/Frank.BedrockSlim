using Frank.BedrockSlim.Client;
using Frank.BedrockSlim.Client.Sample;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTcpClient(options =>
{
    options.Timeout = TimeSpan.FromSeconds(5);
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();