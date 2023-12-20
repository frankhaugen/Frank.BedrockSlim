namespace Frank.BedrockSlim.Client;

public class TcpClientOptions
{
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(5);
}