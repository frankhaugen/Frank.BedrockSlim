using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;

namespace Frank.BedrockSlim.Server;

internal class TcpConnectionHandler : ConnectionHandler
{
	private readonly ILogger<TcpConnectionHandler> _logger;
	private readonly IConnectionHandler _connectionHandler;

	public TcpConnectionHandler(ILogger<TcpConnectionHandler> logger, IConnectionHandler connectionHandler)
	{
		_logger = logger;
		_connectionHandler = connectionHandler;
	}

	public override async Task OnConnectedAsync(ConnectionContext connection)
	{
		_logger.LogDebug("Connected: {ConnectionId}", connection.ConnectionId);

		while (true)
		{
			var result = await connection.Transport.Input.ReadAsync();
			var buffer = result.Buffer;

			foreach (var segment in buffer)
			{
				if (segment.IsEmpty) continue;
				var responseBytes = await _connectionHandler.HandleAsync(segment);
				await connection.Transport.Output.WriteAsync(responseBytes);
			}

			if (result.IsCompleted)
				break;

			connection.Transport.Input.AdvanceTo(buffer.End);
		}

		_logger.LogDebug("Disconnected: {ConnectionId}", connection.ConnectionId);
	}
}