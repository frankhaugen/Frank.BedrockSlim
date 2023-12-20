using System.Text;

namespace Frank.BedrockSlim.Server.Sample;

public class MyCustomProcessor : IConnectionProcessor
{
    public async Task<ReadOnlyMemory<byte>> ProcessAsync(ReadOnlyMemory<byte> input)
    {
        var stringInput = Encoding.UTF8.GetString(input.ToArray());
        Console.WriteLine($"Received: {stringInput}");
        return new ReadOnlyMemory<byte>("OK"u8.ToArray());
    }
}