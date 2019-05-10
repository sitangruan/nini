using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nini.core.V10;

namespace nini.V20.Controllers
{
    [ApiVersion("2.0")]
    public class LogoutController : V10.Controllers.LogoutController
    {
        private readonly ILoginManager _loginManager;

        public LogoutController(ILogger<V10.Controllers.LogoutController> logger, ILoginManager manager) : base(logger, manager)
        {
            _loginManager = manager;
        }
    }
}