﻿using Microsoft.AspNetCore.Mvc;
using nini.core.V10;
using System;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using nini.Common;

namespace nini.V10.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Login/Logout/KeepLive")]
    public class LogoutController : BaseController<LogoutController>
    {
        private readonly ILoginManager _loginManager;

        public LogoutController(ILogger<LogoutController> logger, ILoginManager manager) : base(logger)
        {
            _loginManager = manager;
        }

        [HttpPost]
        [Description("Log out")]
        public NoContentResult Logout(Guid sessionId)
        {
            Logger.LogDebug($"Session Id '{sessionId}' calls Logout.");
            _loginManager.Logout(sessionId);

            return NoContent();
        }
    }
}