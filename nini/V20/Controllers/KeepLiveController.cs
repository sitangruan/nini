using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nini.core.V10;

namespace nini.V20.Controllers
{
    [ApiVersion("2.0")]
    public class KeepLiveController : V10.Controllers.KeepLiveController
    {
        private readonly ILoginManager _loginManager;

        public KeepLiveController(ILogger<V10.Controllers.KeepLiveController> logger, ILoginManager manager) : base(logger, manager)
        {
            _loginManager = manager;
        }
    }
}