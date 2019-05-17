using Microsoft.AspNetCore.Http;
using nini.foundation.Logging;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nini.core.Common.WebSockets
{
    public class MarvelWebSocketManager: IMarvelWebSocketManager
    {
        protected ConcurrentDictionary<Guid, WebSocket> mWebSockets = new ConcurrentDictionary<Guid, WebSocket>();
        private readonly IMarvelLogger mLogger = MarvelLoggerFactory.Generate(typeof(MarvelWebSocketManager));

        public ConcurrentDictionary<Guid, WebSocket> GetWebSocketDictionary()
        {
            return mWebSockets;
        }

        public void AddSocket(WebSocket socket)
        {
            var newId = Guid.NewGuid();
            mWebSockets.TryAdd(newId, socket);
        }
        public Guid GetSocketId(WebSocket socket)
        {
            return mWebSockets.FirstOrDefault(p => p.Value == socket).Key;
        }

        public async Task RemoveSocket(Guid id)
        {
            if (mWebSockets.TryRemove(id, out WebSocket socket))
            {
                await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure,
                    "Closed by the MarvelWebSocketManager", CancellationToken.None);
            }
        }

        public async Task AcceptIncomingSocket(WebSocket socket, HttpContext context)
        {
            AddSocket(socket);
            var socketId = GetSocketId(socket);
            var connectedConfirmation =
                $"Your web socket is connected successfully! Your unique Id is '{socketId}'.";
            Send(socket, connectedConfirmation);
            await OnDataReceived(socket, context);
        }

        public async Task OnDataReceived(WebSocket socket, HttpContext context)
        {
            var buffer = new byte[1024 * 4];

            try
            {
                while (socket.State == WebSocketState.Open && !context.RequestAborted.IsCancellationRequested)
                {
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None)
                        .ConfigureAwait(false);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var socketId = GetSocketId(socket);
                        var received = Encoding.UTF8.GetString(buffer, 0, result.Count);

                        var response =
                            $"Web socket (Id: '{socketId}'): In {DateTime.Now} we receive this message from you: {received}";

                        Send(socket, response);
                    }
                }
            }
            catch (Exception ex)
            {
                mLogger.LogError("Exception happened in OnDataReceived in web socket. Detail: " + ex.Message);
            }

            await OnDisconnected(socket);
        }

        public async Task OnDisconnected(WebSocket socket)
        {
            var socketId = GetSocketId(socket);
            await RemoveSocket(socketId);
        }

        public void Send(WebSocket socket, string data)
        {
            if (socket != null && socket.State == WebSocketState.Open)
            {
                var bytes = Encoding.UTF8.GetBytes(data);

                socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true,
                    CancellationToken.None);
            }
        }
    }
}
