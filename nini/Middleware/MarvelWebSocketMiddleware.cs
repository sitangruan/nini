using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using nini.core.Common.WebSockets;

namespace nini.Middleware
{
    public class MarvelWebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMarvelWebSocketManager _mWebSocketManager;

        public MarvelWebSocketMiddleware(RequestDelegate next, IMarvelWebSocketManager mWebSocketManager)
        {
            _next = next;
            _mWebSocketManager = mWebSocketManager;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var socket = await context.WebSockets.AcceptWebSocketAsync();
                await _mWebSocketManager.AcceptIncomingSocket(socket, context);
            }
        }
    }
}
