using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using nini.core.V10;

namespace nini.V20.Controllers
{
    [ApiVersion("2.0")]
    public class ValuesController : V10.Controllers.ValuesController
    {
        private readonly IValuesManager _valuesManager;

        public ValuesController(ILogger<V10.Controllers.ValuesController> logger, IValuesManager manager) : base(logger, manager)
        {
            _valuesManager = manager;
        }
    }
}
