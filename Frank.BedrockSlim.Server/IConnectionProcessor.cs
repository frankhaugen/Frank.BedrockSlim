namespace Frank.BedrockSlim.Server;

public interface IConnectionProcessor
{
    Task<ReadOnlyMemory<byte>> ProcessAsync(ReadOnlyMemory<byte> input);
}