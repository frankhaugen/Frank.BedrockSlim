# Frank.BedrockSlim
___
The purpose of this project is to provide a starting point for building a web application using the Bedrock Framework. It is intended to be used as a starting point for building servers and clients, using minimal dependencies, and providing a simple, consistent, and easy to use API.

ASP.NET Core is the core of this library, but very little of the actual "basics" are added, so its only providing "server infrastructure" and "client infrastructure" for the most part. The rest is up to you.

___

![Nuget](https://img.shields.io/nuget/v/Frank.BedrockSlim.Server?label=Server&style=for-the-badge)
![Nuget](https://img.shields.io/nuget/v/Frank.BedrockSlim.Client?label=Client&style=for-the-badge)

![Nuget](https://img.shields.io/nuget/dt/Frank.BedrockSlim.Server?label=Server&style=for-the-badge)
![Nuget](https://img.shields.io/nuget/dt/Frank.BedrockSlim.Client?label=Client&style=for-the-badge)



___
## Table of Contents

- [Getting Started](#getting-started)
  - [Installing](#installing)
  - [Sample Projects](#sample-projects)
  - [Server](#server)
  - [Client](#client)
  - [Lack of Tests](#lack-of-tests)

___

## Getting Started

### Installing

To install the latest version of this library, run one or both of the following commands:

```bash
dotnet add package Frank.BedrockSlim.Server
dotnet add package Frank.BedrockSlim.Client
```

### Sample Projects

There are two sample projects included in this repository, one for the server, and one for the client. They are both console applications, and can be used to test the library.

To run the sample projects, just run the following commands run the 'run-samples.ps1' script:

```bash
pwsh ./run-samples.ps1
```

The script will build the library, and then run the server saple project and wait 5 seconds before running the client sample project, then close them after 15 seconds.

### Server

The server is a simple console application that hosts a web server. It is configured using the `Host.CreateDefaultBuilder` method, which provides a default configuration, logging, and dependency injection setup. The `ConfigureWebHostDefaults` method configures the web host using the `Startup` class, which is where you can configure the server.

#### Program.cs

```csharp
var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions( ));
builder.UseTcpConnectionHandler<MyCustomProcessor>(6667);
builder.Logging.AddConsole();
var app = builder.Build();
await app.RunAsync();
```

#### MyCustomProcessor.cs

```csharp
public class MyCustomProcessor : IConnectionProcessor
{
    private readonly ILogger<MyCustomProcessor> _logger;

    public MyCustomProcessor(ILogger<MyCustomProcessor> logger)
    {
        _logger = logger;
    }

    public async Task<ReadOnlyMemory<byte>> ProcessAsync(ReadOnlyMemory<byte> input)
    {
        var stringInput = Encoding.UTF8.GetString(input.ToArray());
        _logger.LogInformation("Received: {Input}", stringInput);
        return new ReadOnlyMemory<byte>("OK"u8.ToArray());
    }
}
```

### Client

The client is a simple interface called `ITcpClient` that provides a simple API for connecting to a server and sending and receiving data. It has an extension method called `AddTcpClient` that can be used to configure the client in your `Startup` class or `Program` class.

#### Program.cs

```csharp
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTcpClient(options =>
{
    options.Timeout = TimeSpan.FromSeconds(5);
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

#### Worker.cs

```csharp
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ITcpClient _tcpClient;

    public Worker(ILogger<Worker> logger, ITcpClient tcpClient)
    {
        _logger = logger;
        _tcpClient = tcpClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                var response = await _tcpClient.SendAsync(IPAddress.Loopback, 6667, "Hello World"u8.ToArray());
                
                if (response.Length > 0)
                {
                    _logger.LogInformation("Received: {Response}", Encoding.UTF8.GetString(response.ToArray()));
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
```
___

## Lack of Tests

Except for the sample projects, there are no tests. This is because theres very little to test. The XUnit project is there to make it easy to start testing, but the actual test cases are not very clear at this time.
___
## Contributing

Not all contributions are code! We welcome contributions from everyone, but please see our [contributing guide](CONTRIBUTING.md) for more information.