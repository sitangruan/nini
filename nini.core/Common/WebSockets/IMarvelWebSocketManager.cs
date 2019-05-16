using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace nini.core.Common.WebSockets
{
    public interface IMarvelWebSocketManager
    {
        Task AcceptIncomingSocket(WebSocket socket, HttpContext context);
    }
}
