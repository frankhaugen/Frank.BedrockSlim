using System.Text;

namespace Frank.BedrockSlim.Server.Sample;

public class MyCustomProcessor : IConnectionProcessor
{
    public async Task<byte[]> ProcessAsync(ReadOnlyMemory<byte> input)
    {
        var stringInput = Encoding.UTF8.GetString(input.ToArray());
        Console.WriteLine($"Received: {stringInput}");
        return "OK"u8.ToArray();
    }
}