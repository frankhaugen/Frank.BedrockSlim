using Frank.BedrockSlim.Server;
using Frank.BedrockSlim.Server.Sample;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions( ));
builder.UseTcpConnectionHandler<MyCustomProcessor>(6667);
builder.Logging.AddConsole();
var app = builder.Build();
await app.RunAsync();