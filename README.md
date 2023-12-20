# Frank.BedrockSlim
This starter project is a fork of [Bedrock.Framework]() and is intended to be a starting point for building a web application using the Bedrock Framework. It is intended to be used as a starting point for building a web application using the Bedrock Framework. Then it ended up a bit different, and somewhat higher level.

The purpose of this project is to provide a starting point for building a web application using the Bedrock Framework. It is intended to be used as a starting point for building servers and clients, using minimal dependencies, and providing a simple, consistent, and easy to use API.

ASP.NET Core is the core of this library, but very little of the actual "basics" are added, so its only providing "server infrastructure" and "client infrastructure" for the most part. The rest is up to you.

## Getting Started

### Installing

To install the latest version of this library, run one or both of the following commands:

```bash
dotnet add package Frank.BedrockSlim.Server
dotnet add package Frank.BedrockSlim.Client
```

### Server

The server is a simple console application that hosts a web server. It is configured using the `Host.CreateDefaultBuilder` method, which provides a default configuration, logging, and dependency injection setup. The `ConfigureWebHostDefaults` method configures the web host using the `Startup` class, which is where you can configure the server.

### Client

The client is a simple console application that makes a request to a web server. It is configured using the `Host.CreateDefaultBuilder` method, which provides a default configuration, logging, and dependency injection setup. The `ConfigureServices` method configures the client using the `Startup` class, which is where you can configure the client.

## Contributing

Not all contributions are code! We welcome contributions from everyone, but please see our [contributing guide](CONTRIBUTING.md) for more information.