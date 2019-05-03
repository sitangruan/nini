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
    public class LoginController : BaseController<LoginController>
    {
        private readonly ILoginManager _loginManager;

        public LoginController(ILogger<LoginController> logger, ILoginManager manager) : base(logger)
        {
            _loginManager = manager;
        }

        [HttpPost]
        public Guid DoLogin([FromBody] UserCredential credential)
        {
            Logger.LogDebug($"User '{credential.userName}' tries to login.");
            Guid sessionId = _loginManager.DoLogin(credential.userName, credential.password);

            return sessionId;
        }
    }

    public class UserCredential
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}