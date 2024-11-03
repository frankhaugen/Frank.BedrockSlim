namespace Frank.BedrockSlim.Server;

public interface IConnectionHandler
{
    Task<ReadOnlyMemory<byte>> HandleAsync(ReadOnlyMemory<byte> input);
}