using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nini.core.V10;

namespace nini.V20.Controllers
{
    [ApiVersion("2.0")]
    public class LoginController : V10.Controllers.LoginController
    {
        private readonly ILoginManager _loginManager;

        public LoginController(ILogger<V10.Controllers.LoginController> logger, ILoginManager manager) : base(logger, manager)
        {
            _loginManager = manager;
        }
    }
}