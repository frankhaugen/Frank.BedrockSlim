using System.Text;

namespace Frank.BedrockSlim.Server.Sample;

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
        return await Task.FromResult(Encoding.UTF8.GetBytes($"Echo: {stringInput}"));
    }
}