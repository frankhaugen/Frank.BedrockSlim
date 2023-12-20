using System.Net;

namespace Frank.BedrockSlim.Client;

public interface ITcpClient
{
    /// <summary>
    /// Sends data to the specified server and returns the response.
    /// </summary>
    /// <param name="serverIp"></param>
    /// <param name="serverPort"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<ReadOnlyMemory<byte>> SendAsync(IPAddress serverIp, int serverPort, ReadOnlyMemory<byte> data);
}