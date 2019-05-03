using Microsoft.AspNetCore.Mvc;
using nini.core.V10;
using System;
using Microsoft.Extensions.Logging;
using nini.Common;

namespace nini.V10.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class KeepLiveController : BaseController<KeepLiveController>
    {
        private readonly ILoginManager _loginManager;

        public KeepLiveController(ILogger<KeepLiveController> logger, ILoginManager manager) : base(logger)
        {
            _loginManager = manager;
        }

        [HttpPost]
        public NoContentResult KeepLive(Guid sessionId)
        {
            Logger.LogDebug($"Session Id '{sessionId}' calls KeepLive.");
            _loginManager.KeepLive(sessionId);

            return NoContent();
        }
    }
}