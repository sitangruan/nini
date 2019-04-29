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
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager _loginManager;

        public LoginController(ILoginManager manager)
        {
            _loginManager = manager;
        }

        [HttpPost]
        public Guid DoLogin([FromBody] UserCredential credential)
        {
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