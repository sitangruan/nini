using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nini.core.V10;

namespace nini.V10.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly ILoginManager _loginManager;

        public LogoutController(ILoginManager manager)
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