using Microsoft.AspNetCore.Mvc;
using nini.core.V10;
using System;

namespace nini.V10.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class KeepLiveController : ControllerBase
    {
        private readonly ILoginManager _loginManager;

        public KeepLiveController(ILoginManager manager)
        {
            _loginManager = manager;
        }

        [HttpPost]
        public NoContentResult Logout(Guid sessionId)
        {
            _loginManager.Logout(sessionId);

            return NoContent();
        }
    }
}