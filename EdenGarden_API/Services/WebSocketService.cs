using EdenGarden_API.Services.Interfaces;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace EdenGarden_API.Services
{
    public class WebSocketService
    {
        private readonly List<WebSocket> _sockets = new List<WebSocket>();

        public async Task HandleWebSocketAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                _sockets.Add(webSocket);
                await ReceiveMessages(webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        public async Task ReceiveMessages(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            _sockets.Remove(webSocket);
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        public async Task SendMessageAsync(string message)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);
            var tasks = new List<Task>();

            foreach (var socket in _sockets)
            {
                if (socket.State == WebSocketState.Open)
                {
                    tasks.Add(socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None));
                }
            }

            await Task.WhenAll(tasks);  
        }
    }
}
