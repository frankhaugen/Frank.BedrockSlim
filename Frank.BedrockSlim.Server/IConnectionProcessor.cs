namespace Frank.BedrockSlim.Server;

public interface IConnectionProcessor
{
    Task<byte[]> ProcessAsync(ReadOnlyMemory<byte> input);
}